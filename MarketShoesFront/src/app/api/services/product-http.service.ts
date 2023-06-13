import {API_URL} from "../../config/Constants";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Product} from "../models/Product";
import {SecurityService} from "../../services/security.service";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";


@Injectable({providedIn:'root'})
export class ProductHttpService{
  private readonly URL =`${API_URL}/Product`;

  constructor(private httpClient:HttpClient,
              private securityService:SecurityService) {
  }
  getAll(){
    const token = this.securityService.getToken();

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);



    return this.httpClient.get(`${this.URL}`, { headers }) ;
  }
  get(id:number){
    const token = this.securityService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.httpClient.get(`${this.URL}/${id}`, { headers }) ;
  }


  create(product: Product, files: File[]) {
    const token = this.securityService.getToken();

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    const mergedData = new FormData();
    mergedData.append('Entity', JSON.stringify(product));

    for (const element of files) {
      mergedData.append("Photos", element);
    }

    return this.httpClient.post(`${this.URL}`, mergedData, { headers });
  }
}

