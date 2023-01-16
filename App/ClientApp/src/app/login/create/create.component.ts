import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from '../../models/login.module';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent {

  disabled: string = '';
  errorMessage: string = '';
  confirmPassword: string = '';
  checkCanProceed: boolean = false;
  loginRequest: Login = {
    Password: '',
    Username: '',
    EmailAddress: ''
  }

  constructor(private service: ApiService, private router: Router) { }

  createUser() {
    this.checkCanProceed = true;
    this.errorMessage = '';

    if (this.confirmPassword === undefined
      || this.confirmPassword === null
      || this.confirmPassword === '') { this.checkCanProceed = false; }
    if (this.loginRequest.EmailAddress === undefined
      || this.loginRequest.EmailAddress === null
      || this.loginRequest.EmailAddress === '') { this.checkCanProceed = false; }
    if (this.loginRequest.Password === undefined
      || this.loginRequest.Password === null
      || this.loginRequest.Password === '') { this.checkCanProceed = false; }
    if (this.loginRequest.Username === undefined
      || this.loginRequest.Username === null
      || this.loginRequest.Username === '') { this.checkCanProceed = false; }
    if (this.confirmPassword !== this.loginRequest.Password) { this.checkCanProceed = false; }

    if (this.checkCanProceed === false) {
      this.errorMessage = 'Please enter all required fields to continue';
    } else {
      this.disabled = 'disabled';
      this.service.createUser(this.loginRequest).subscribe({
        next: (data) => {
          data ? this.router.navigate(['login', data.username]) : this.router.navigate(['error']);
        },
        error: () => {
          this.checkCanProceed = false;
          this.disabled = '';
          this.errorMessage = "Please specify all required details.";
          document.getElementById('username')?.focus();
        }
      });
    }
  }
}
