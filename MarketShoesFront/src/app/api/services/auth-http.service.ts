import {Injectable} from "@angular/core";
import {API_URL} from "../../config/Constants";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {SecurityUser} from "../models/SecurityUser";
import {Credentials} from "../models/Credentials";
import {RegisterModel} from "../models/RegisterModel";


@Injectable({providedIn:'root'})
export class AuthHttpService {
  private readonly URL =`${API_URL}/Auth`;
   httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Cache-Control': 'no-cache',
      'Access-Control-Allow-Origin': '*'
    })
  };
  constructor(private httpClient:HttpClient) {
  }


  login(credentials:Credentials){
    return this.httpClient.post<SecurityUser>(`${this.URL}/login`,credentials,this.httpOptions);
  }
  register(register:RegisterModel){
    return this.httpClient.post(`${this.URL}/register`,register,this.httpOptions)
  }

  confirm(token:string){
    return this.httpClient.head(`${this.URL}/confirmEmail/${token}`);
  }


}
