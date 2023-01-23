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

  disabled: string = '';
  valid: boolean = false;
  errorMessage: string = '';
  loginRequest: Login = {
    Password: '',
    Username: '',
    EmailAddress: ''
  }

  constructor(private service: ApiService, private router: Router, private route: ActivatedRoute, private storage: Storage) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => this.loginRequest.Username = params['id']);
  }

  loginUser() {
    this.errorMessage = '';
    this.valid = true;

    if (this.loginRequest.Username === ''
      || this.loginRequest.Username === null
      || this.loginRequest.Username === undefined) { this.valid = false }
    if (this.loginRequest.Password === ''
      || this.loginRequest.Password === null
      || this.loginRequest.Password === undefined) { this.valid = false }
    if (!this.valid) { this.errorMessage = "Invalid login details specified."; }

    if (this.valid) {
      this.disabled = 'disabled';
      this.service.loginCurrentUser(this.loginRequest).subscribe({
        next: (data) => {
          this.storage.setItem('id', data.id.toString());
          this.storage.setItem('token', data.token);
          data ? this.router.navigate(['products', data.id]) : this.router.navigate(['error']);
        },
        error: (response) => {
          this.valid = false;
          this.disabled = '';
          if (response.error.status === 404) {
            this.router.navigate(['error'])
          } else {
            this.errorMessage = "An error occured: " + response.message;
          }
          document.getElementById('username')?.focus();
        }
      });
    }    
  }
}
