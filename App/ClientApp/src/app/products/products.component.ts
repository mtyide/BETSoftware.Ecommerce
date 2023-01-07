import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';
import { FilterParameters } from './../models/filter.module'

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {
  constructor(private service: ApiService, private route: ActivatedRoute) { }

  public checkoutDisabled: string = "disabled";
  subTotal: any = [];
  public grandTotal: number = 0;
  public totalAmount: number = 0;
  public token: string = "";
  public expires: string = "";
  public id: number = 0;
  public ProductsList: any = [];
  public filterRequest: FilterParameters = {
    Size: 0,
    Page: 0
  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => this.id = params['id']);
    this.getProductsList();
  }

  getProductsList() {
    this.service.getProducts(this.filterRequest).subscribe({
      next: (orders) => {
        this.ProductsList = orders;
      },
      error: (response) => {
        console.log(response);
      }
    });
  }

  calcTotal(price: number, val: string, index: number) {
    this.totalAmount = (price * Number.parseInt(val));
    this.subTotal[(index - 1)] = this.totalAmount;
    this.getGrandTotal();
    return this.totalAmount.toFixed(2);
  }

  getGrandTotal() {
    this.grandTotal = 0;
    for (var i = 0; i <= (this.subTotal.length - 1); i++) {
      this.grandTotal += this.subTotal[i];
    }
    (this.grandTotal === 0) ? this.checkoutDisabled = "disabled" : this.checkoutDisabled = "";
  }
}
