import {Component, NgModule} from '@angular/core';
import {CommonModule} from "@angular/common";
import {ActivatedRoute, RouterModule} from "@angular/router";
import {ButtonModule} from "primeng/button";
import {RippleModule} from "primeng/ripple";
import {ProductHttpService} from "../../../api/services/product-http.service";
import {ProductView} from "../../../api/models/ProductView";
import {first} from "rxjs";

@Component({
  selector: 'app-shoes-item',
  templateUrl: './shoes-item.component.html',
  styleUrls: ['./shoes-item.component.scss']
})
export class ShoesItemComponent {
  product!: ProductView;

  constructor(private activatedRoute: ActivatedRoute,
              private productHttpService:ProductHttpService) {
    const shoesId = activatedRoute.snapshot.params['offerId'];
    if (shoesId) {
      this.getShoes(shoesId);
    }

  }
  getShoes(id:number){
    this.productHttpService.get(id)
      .pipe(first())
      .subscribe({
        next:(product)=>{this.product = product as ProductView}
      })
  }
}





@NgModule({
  declarations: [ShoesItemComponent],
  imports: [
    RouterModule.forChild([{path: "", component: ShoesItemComponent}]),
    CommonModule,
    ButtonModule,
    RippleModule,

  ]
})
export class ShoesItemModule { }
