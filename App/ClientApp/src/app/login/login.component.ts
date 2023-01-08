import { Component } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ApiService } from '../services/api.service';
import { Login } from './../models/login.module'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginRequest: Login = {
    Password: '',
    Username: '',
    EmailAddress: ''
  }

  constructor(private service: ApiService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => this.loginRequest.Username = params['id']);
  }

  loginUser() {
    this.service.loginCurrentUser(this.loginRequest).subscribe({
      next: (data) => {
        data ? this.router.navigate(['products', data.id]) : this.router.navigate(['error']);
      },
      error: (response) => {
        console.log(response);
        this.router.navigate(['error']);
      }
    });
  }
}
