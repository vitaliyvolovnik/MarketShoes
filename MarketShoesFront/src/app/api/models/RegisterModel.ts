import {Role} from "./enums/Role";

export interface RegisterModel{
  Email:string;
  Password:string;
  Firstname:string;
  Lastname:string;
  Number:string;
  Role:Role;
}
