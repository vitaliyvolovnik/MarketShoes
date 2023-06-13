import {API_URL} from "../../config/Constants";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Product} from "../models/Product";
import {SecurityService} from "../../services/security.service";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {BasketItem} from "../models/basketItem";


@Injectable({providedIn:'root'})
export class CartHttpService{
  private readonly URL =`${API_URL}/Basket`;

  constructor(private httpClient:HttpClient,
              private securityService:SecurityService) {
  }
  get(){
    const token = this.securityService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.httpClient.get<BasketItem[]>(`${this.URL}`, { headers }) ;
  }

  create(item:BasketItem) {
    const token = this.securityService.getToken();

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.httpClient.post(`${this.URL}`, item, { headers });
  }
  add(id:number){
    const token = this.securityService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.httpClient.post(`${this.URL}/${id}`,null,{ headers })
  }

  removeFromBasket(id:number){
    const token = this.securityService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.httpClient.delete(`${this.URL}/${id}`,{ headers })
  }
}

