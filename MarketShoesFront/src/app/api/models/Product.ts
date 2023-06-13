import {SubCharacteristic} from "./SubCharacteristic";

export interface Product{
  Id:number;
  Model:string;
  Description:string;
  Manufacturer:string;
  Price:number;
  Code:string;
  Count:number;
  IsAvailable:boolean;
  SellerId:number;
  Photos:FormData;
  Characteristics:SubCharacteristic[]
}




