import { Component, OnInit } from '@angular/core';
import { CustomerDto } from '../model/CustomerDto';
import { CustomerService } from '../customer.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {

  public customers:CustomerDto[];
  
  constructor(private customerService:CustomerService) { }

  ngOnInit() {
    this.customerService.PublicCustomers().then(data=>{
      this.customers = data;
    });
  }

}
