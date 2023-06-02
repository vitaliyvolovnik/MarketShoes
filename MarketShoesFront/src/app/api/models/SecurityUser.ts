import {Role} from "./enums/Role";

export interface SecurityUser{
  id:number;
  email:string;
  role:Role;
  active:boolean;
  jwtBearer:string;
}
