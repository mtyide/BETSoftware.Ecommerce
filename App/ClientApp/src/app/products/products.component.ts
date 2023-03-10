import { Component } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';
import { Order } from '../models/order.module';
import { Line } from '../models/line.module';
import { formatDate } from '@angular/common';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {
  constructor(private service: ApiService, private route: ActivatedRoute, private router: Router, private storage: Storage) { }

  errorMessage: string = '';
  imagesBaseUri: string = this.service.ImagesUrl;
  pageIndex: number = 0;
  pageSize: number = 0;
  length: number = 0;
  pageSizeOptions: number[] = [2, 3, 4, 5];
  date = formatDate(new Date(), 'yyyy/MM/dd', 'en');
  selectedProductIds: any = [];
  selectedProductQty: any = [];
  checkoutDisabled: string = "disabled";
  createOrderDisabled: string = "disabled";
  id: number = 0;
  token: any;
  subTotal: any = [];
  
  order: Order = {
    Active: true,
    CustomerId: 0,
    Id: 0,
    Date: this.date,
    Lines: [],
    ShippingAddress: '',
    ShippingRequired: false,
    ShippingTax: 0,
    TotalAmount: 0
  };
  grandTotal: number = 0;
  totalAmount: number = 0;
  expires: string = '';
  ProductsList: any = [];

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => this.id = params['id']);
    this.token = this.storage.getItem('token');
    if (this.token === null || this.token === undefined
          || this.id === 0 || this.id === undefined) {
      this.router.navigate(['login']);
    }
    else {
      this.getProductsList();
    }
  }

  pageNavigations(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.getProductsList();
  }

  updateOrder(order: Order, id: number) {
    this.errorMessage = '';
    order.Lines = [{ length: this.selectedProductIds.length }];
    for (var i = 0; i <= (this.selectedProductIds.length - 1); i++) {
      const line = new Line();
      line.OrderId = id;
      line.ProductId = this.selectedProductIds[i];
      line.Qty = this.selectedProductQty[i];

      order.Lines.push(line);
    }

    this.service.updateOrder(order, id, this.token).subscribe({
      next: () => {
        this.router.navigate(['created', id]);
      },
      error: (response) => {
        this.errorMessage = "An error occured: " + response.data;
      }
    });
  }

  getProductsList() {
    this.errorMessage = '';
    this.service.getProducts(this.token).subscribe({
      next: (products) => {
        this.ProductsList = products;
        this.length = this.ProductsList.length;
        this.pageIndex = 0;
        this.pageSize = 50;
      },
      error: (response) => {
        if (response.error.status === 404) {
          this.router.navigate(['error'])
        } else {
          this.errorMessage = "An error occured: " + response.message;
        }
      }
    });
  }

  calcTotal(price: number, val: string, index: number) {
    this.totalAmount = (price * Number.parseInt(val));
    this.subTotal[(index - 1)] = this.totalAmount;
    this.selectedProductIds[(index - 1)] = index;
    this.selectedProductQty[(index - 1)] = Number.parseInt(val);
    this.getGrandTotal();
    return this.totalAmount.toFixed(2);
  }

  getGrandTotal() {
    this.grandTotal = 0;
    for (var i = 0; i <= (this.selectedProductQty.length - 1); i++) {
      if (this.subTotal[i] === NaN || this.subTotal[i] === undefined) { continue; }
      this.grandTotal += this.subTotal[i];
    }
    (this.grandTotal === 0) ? this.checkoutDisabled = "disabled" : this.checkoutDisabled = "";
  }

  enableCreateOrder() {
    this.createOrderDisabled = "";
    this.checkoutDisabled = "disabled";
  }

  createOrder() {
    this.errorMessage = '';
    this.order.Active = true;
    this.order.CustomerId = this.id;
    this.order.Date = this.date;
    this.order.TotalAmount = this.grandTotal;
    this.order.ShippingAddress = 'W416 The Factory, 15 Nelson Road, Observatory, Cape Town, 7925';
    this.order.ShippingRequired = true;
    this.order.ShippingTax = 14.5;

    this.service.insertOrder(this.order, this.token).subscribe({
      next: (order) => {
        this.updateOrder(order, order.id);
      },
      error: (response) => {
        console.log(response);
        if (response.error.status === 404) {
          this.router.navigate(['error'])
        } else {
          this.errorMessage = "An error occured: " + response.message;
        }
      }
    });
  }
}
