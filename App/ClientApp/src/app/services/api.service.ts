import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'
import { environment } from '../../environments/environment';
import { Login } from './../models/login.module'
import { Product } from './../models/product.module'
import { Order } from '../models/order.module';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  url: string = '';
  readonly ApiUrl: string = environment.baseApiUrl;
  readonly ImagesUrl: string = environment.baseImagesUrl;

  constructor(private http: HttpClient) { }

  getProducts(token: string): Observable<Product[]> {
    const auth = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    this.url = this.ApiUrl + '/products/getProducts';
    return this.http.get<Product[]>(this.url, { headers: auth });
  }

  getProductsById(val: number, token: string): Observable<Product> {
    const auth = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    this.url = this.ApiUrl + "/products/getProductsById" + val;
    return this.http.get<Product>(this.url, { headers: auth });
  }

  loginCurrentUser(val: Login) {
    this.url = this.ApiUrl + "/users/login";
    return this.http.post<any>(this.url, val);
  }

  createUser(val: Login) {
    this.url = this.ApiUrl + "/users/insertUser";
    return this.http.post<any>(this.url, val);
  }

  insertOrder(val: Order, token: string): Observable<any> {
    const auth = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    this.url = this.ApiUrl + "/orders/insertOrder";
    return this.http.post<any>(this.url, val, { headers: auth });
  }

  updateOrder(val: Order, id: number, token: string): Observable<Order> {
    const auth = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    this.url = this.ApiUrl + "/orders/updateOrder/" + id;
    return this.http.put<Order>(this.url, val, { headers: auth });
  }
}
