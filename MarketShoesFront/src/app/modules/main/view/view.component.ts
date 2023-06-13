import {Component, NgModule} from '@angular/core';
import {CommonModule} from "@angular/common";
import {RouterModule} from "@angular/router";
import {CheckboxModule} from "primeng/checkbox";
import {Characteristic} from "../../../api/models/Characteristic";
import {SubCharacteristicHttpService} from "../../../api/services/subCharacteristic-http.service";
import {first} from "rxjs";
import {ProductHttpService} from "../../../api/services/product-http.service";
import {Product} from "../../../api/models/Product";
import {ProductView} from "../../../api/models/ProductView";
import {CartHttpService} from "../../../api/services/cart-http.service";
import {MessageService} from "primeng/api";

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.scss']
})
export class ViewComponent {
  characteristics:Characteristic[]= []
  shoes:ProductView[] = []
  isHide: boolean[] = []

  constructor(private subCharacteristicService:SubCharacteristicHttpService,
              private productHttpService:ProductHttpService,
              private cartHttpService:CartHttpService,
              private messageService:MessageService) {

    this.subCharacteristicService.getAll()
      .pipe(first())
      .subscribe({
        next:(characteristics)=>{
          this.characteristics = characteristics as Characteristic[];
          console.log(characteristics)
          this.isHide = Array(this.characteristics.length).fill(true)
        },
        error:(err)=>{console.log(err.message())}
      });
    this.productHttpService.getAll()
      .pipe(first())
      .subscribe({
        next:(shoes)=>{
          this.shoes = shoes as ProductView[];
          console.log(this.shoes)
        },
        error:(err) =>{
          console.log(err.message)
        }
      })


  }

  ToggleHide(id:number){
    this.isHide[id] = !this.isHide[id];
  }

  addToCart(id: number) {
    this.cartHttpService.add(id)
      .pipe(first())
      .subscribe({
        next:()=>{
          this.messageService.add({severity: 'success', summary: 'Корзина', detail: 'товар добавлено в корзину'});
        },
        error:(err)=>{
          this.messageService.add({severity: 'error', summary: 'Корзина', detail: 'не вдалося добавити товар у корзину'});
        }
      })
  }



}

@NgModule({
  declarations: [ViewComponent],
  imports: [
    RouterModule.forChild([{path: "", component: ViewComponent}]),
    CommonModule,
    CheckboxModule
  ]
})
export class ViewModule { }
