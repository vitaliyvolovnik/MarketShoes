import {Component, EventEmitter, NgModule, Output} from '@angular/core';
import {CommonModule} from "@angular/common";
import {CartHttpService} from "../../../api/services/cart-http.service";
import {VirtualScrollerModule} from "primeng/virtualscroller";
import {ProductView} from "../../../api/models/ProductView";
import {LazyLoadEvent} from "primeng/api";
import {first} from "rxjs";
import {BasketItem} from "../../../api/models/basketItem";
import {ButtonModule} from "primeng/button";
import {RippleModule} from "primeng/ripple";

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {
  @Output() closeClick = new EventEmitter<string>();
  items: BasketItem[] = [];

  constructor(private cartHttpService:CartHttpService) {
    this.cartHttpService.get()
      .pipe(first())
      .subscribe({
        next:(items)=>{this.items = items}
      })

  }



  close() {
    this.closeClick.emit("")
  }

  removeFromBusket(id:number) {
    this.cartHttpService.removeFromBasket(id)
      .pipe(first())
      .subscribe({
        next:()=>{
          this.cartHttpService.get()
            .pipe(first())
            .subscribe({
              next:(items)=>{this.items = items}
            })
        }
      })
  }
}


@NgModule({
  declarations: [BasketComponent],
  exports: [
    BasketComponent
  ],
  imports: [
    CommonModule,
    VirtualScrollerModule,
    ButtonModule,
    RippleModule
  ]
})
export class BasketModule { }
