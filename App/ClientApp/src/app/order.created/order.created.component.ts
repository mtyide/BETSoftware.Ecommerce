import { Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-order.created',
  templateUrl: './order.created.component.html',
  styleUrls: ['./order.created.component.css']
})
export class OrderCreatedComponent {
  constructor(private route: ActivatedRoute) { }

  public id: number = 0;

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => this.id = params['id']);
  }
}
