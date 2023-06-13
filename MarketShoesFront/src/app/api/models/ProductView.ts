import {SubCharacteristic} from "./SubCharacteristic";
import {Photo} from "./photo";

export interface ProductView{
  id:number;
  model:string;
  description:string;
  manufacturer:string;
  price:number;
  code:string;
  count:number;
  isAvailable:boolean;
  sellerId:number;
  photos:Photo[];
  characteristics:SubCharacteristic[]
}



