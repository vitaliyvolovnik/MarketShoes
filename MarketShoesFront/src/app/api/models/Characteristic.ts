import {SubCharacteristic} from "./SubCharacteristic";

export interface Characteristic{
  id:number;
  name:string;
  subCharacteristics:SubCharacteristic[]
}
