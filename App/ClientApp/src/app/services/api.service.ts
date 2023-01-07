import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs'
import { environment } from '../../environments/environment';
import { Login } from './../models/login.module'
import { FilterParameters } from './../models/filter.module'
import { Product } from './../models/product.module'

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  readonly ApiUrl: string = environment.baseApiUrl;
  readonly ImagesUrl: string = environment.baseImagesUrl;

  constructor(private http: HttpClient) { }

  getProducts(val: FilterParameters) {
    return this.http.post(this.ApiUrl + "/products/getProducts", val);
  }

  getProductsById(val: number) : Observable<Product> {
    return this.http.get<Product>(this.ApiUrl + "/products/getProductsById/" + val);
  }

  loginCurrentUser(val: Login) {
    return this.http.post<any>(this.ApiUrl + "/users/login", val);
  }
}
