import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {ShoesItemModule} from './shoes-item/shoes-item.component';
import {RouterModule, Routes} from "@angular/router";
import { ConfirmComponent } from './confirm/confirm.component';
import {ViewComponent, ViewModule} from './view/view.component';




const routes:Routes =[
  {path:"",redirectTo:"view",pathMatch:'full'},
  {path:"view",loadChildren:()=>import("./view/view.component").then(x=>x.ViewModule)},
  //{path:"view/shoes/:id", loadChildren:()=>import("./shoes-item/shoes-item.component").then(x=>x.ShoesItemModule)},
  {path:"seller",loadChildren:()=>import("./seller/seller.module").then(x=>x.SellerModule)},

]


@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    ShoesItemModule
  ]
})
export class MainModule { }
