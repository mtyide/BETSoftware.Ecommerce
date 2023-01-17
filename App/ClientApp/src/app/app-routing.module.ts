import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { ProductsComponent } from './products/products.component';
import { LoginComponent } from './login/login.component';
import { ErrorComponent } from './error/error.component';
import { OrderCreatedComponent } from './order.created/order.created.component';
import { CreateComponent } from './login/create/create.component';

const routes: Routes = [
  { path: 'products/:id', component: ProductsComponent },
  { path: 'login', component: LoginComponent },
  { path: 'error', component: ErrorComponent },
  { path: 'created/:id', component: OrderCreatedComponent },
  { path: 'login/create', component: CreateComponent },
  { path: 'login/:id', component: LoginComponent }
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(routes),
  ],
  exports: [RouterModule],
  declarations: []
})
export class AppRoutingModule { }
