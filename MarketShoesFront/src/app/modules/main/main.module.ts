import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {ShoesItemComponent, ShoesItemModule} from './shoes-item/shoes-item.component';



@NgModule({
  declarations: [

  ],
  imports: [
    CommonModule,
    ShoesItemModule
  ]
})
export class MainModule { }
