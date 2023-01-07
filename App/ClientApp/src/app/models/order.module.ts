export interface Order {
  Id: number,
  Date: string,
  CustomerId: number,
  ShippingRequired: boolean,
  ShippingAddress: string,
  Lines: [],
  Active: boolean,
  TotalAmount: number,
  ShippingTax: number
}
