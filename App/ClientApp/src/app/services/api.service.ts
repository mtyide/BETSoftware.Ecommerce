import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs'
import { environment } from '../../environments/environment';
import { Login } from './../models/login.module'
import { Product } from './../models/product.module'
import { Order } from '../models/order.module';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  readonly ApiUrl: string = environment.baseApiUrl;
  readonly ImagesUrl: string = environment.baseImagesUrl;

  constructor(private http: HttpClient) { }

  getProducts() : Observable<Product[]> {
    return this.http.get<Product[]>(this.ApiUrl + "/products/getProducts");
  }

  getProductsById(val: number) : Observable<Product> {
    return this.http.get<Product>(this.ApiUrl + "/products/getProductsById" + val);
  }

  loginCurrentUser(val: Login) {
    return this.http.post<any>(this.ApiUrl + "/users/login", val);
  }

  createUser(val: Login) {
    return this.http.post<any>(this.ApiUrl + "/users/insertUser", val);
  }

  insertOrder(val: Order): Observable<any> {
    return this.http.post<any>(this.ApiUrl + "/orders/insertOrder", val);
  }

  updateOrder(val: Order, id: number): Observable<Order> {
    return this.http.put<Order>(this.ApiUrl + "/orders/updateOrder/" + id, val);
  }
}
