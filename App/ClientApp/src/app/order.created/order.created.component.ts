import { Component } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

@Component({
  selector: 'app-order.created',
  templateUrl: './order.created.component.html',
  styleUrls: ['./order.created.component.css']
})
export class OrderCreatedComponent {
  constructor(private route: ActivatedRoute, private storage: Storage, private router: Router) { }

  loginId: any;
  id: number = 0;
  token: any;

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => this.id = params['id']);
    this.token = this.storage.getItem('token');
    this.loginId = this.storage.getItem('id');

    if (this.token === null || this.token === undefined
      || this.id === 0 || this.id === undefined) {
      this.router.navigate(['login']);
    }
  }

  redirectToProducts() {
    this.router.navigate(['products', this.loginId]);
  }
}
