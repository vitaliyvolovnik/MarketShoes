import {API_URL} from "../../config/Constants";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {SecurityService} from "../../services/security.service";
import {Product} from "../models/Product";
import {Injectable} from "@angular/core";


@Injectable({providedIn:'root'})
export class SubCharacteristicHttpService{
  private readonly URL =`${API_URL}/SubCharacteristic`;

  constructor(private httpClient:HttpClient,
              private securityService:SecurityService) {
  }


  getAll(){
    return this.httpClient.get(`${this.URL}`);
  }
}
