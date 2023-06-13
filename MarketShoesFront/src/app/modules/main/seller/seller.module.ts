import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from "@angular/router";

const route:Routes=[
  {path:"",redirectTo:"create",pathMatch:"full"},
  {path:"",loadChildren:()=>import("./shoes-create/shoes-create.component").then(x=>x.ShoesCreateModule)}
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(route),
    CommonModule
  ]
})
export class SellerModule { }
