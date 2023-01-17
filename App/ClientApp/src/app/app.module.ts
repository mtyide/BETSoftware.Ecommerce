import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatPaginatorModule } from '@angular/material/paginator';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ProductsComponent } from './products/products.component';
import { ErrorComponent } from './error/error.component';
import { OrderCreatedComponent } from './order.created/order.created.component';
import { CreateComponent } from './login/create/create.component';

import { ApiService } from './services/api.service';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    ProductsComponent,
    ErrorComponent,
    OrderCreatedComponent,
    CreateComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MatPaginatorModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'products/:id', component: ProductsComponent },
      { path: 'login', component: LoginComponent },
      { path: 'error', component: ErrorComponent },
      { path: 'created/:id', component: OrderCreatedComponent },
      { path: 'login/create', component: CreateComponent },
      { path: 'login/:id', component: LoginComponent }
    ]),
    AppRoutingModule
  ],
  providers: [ApiService, { provide: Storage, useValue: localStorage }],
  bootstrap: [AppComponent]
})
export class AppModule { }
