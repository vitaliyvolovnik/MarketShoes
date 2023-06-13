import {Characteristic} from "./Characteristic";

export interface SubCharacteristic{
  id:number;
  name:string;
  characteristic?:Characteristic;
  characteristicId:number;
}
