import { OrderLine } from "./orderlines.module";

export class Line implements OrderLine {
  Id: number = 0;
  ProductId: number = 0;
  OrderId: number = 0;
  Qty: number = 0;
}
