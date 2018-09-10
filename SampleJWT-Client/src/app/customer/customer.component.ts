import { Component, OnInit, Input } from '@angular/core';
import { CustomerDto } from '../model/CustomerDto';
import { CustomerService } from '../customer.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {

  @Input() type:string;
  
  public customers:CustomerDto[];
  
  constructor(private customerService:CustomerService) { }

  ngOnInit() {
    var call:Promise<CustomerDto[]>;
    if(this.type=="special")
    {
      call = this.customerService.SpecialCustomers();
    }
    else if(this.type=="all")
    {
      call = this.customerService.AllCustomers();
    }
    else
    {
      call = this.customerService.PublicCustomers();
    }
    call.then(data=>{
      this.customers = data;
  });
  }

}
