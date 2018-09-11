import { Component, OnInit, Input } from '@angular/core';
import { CustomerDto } from '../model/CustomerDto';
import { CustomerService } from '../customer.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {

  @Input() type: string;

  public customers: CustomerDto[];
  public loading: boolean;
  public errorMessage: string = "";
  constructor(private customerService: CustomerService) { }

  ngOnInit() {
    this.loading = true;
    var call: Promise<CustomerDto[]>;
    if (this.type == "special") {
      call = this.customerService.SpecialCustomers();
    }
    else if (this.type == "all") {
      call = this.customerService.AllCustomers();
    }
    else {
      call = this.customerService.PublicCustomers();
    }
    call.then(data => {
      this.loading = false;
      this.customers = data;
    }).catch(ex => {
      this.loading = false;
      if(typeof ex.error ==='string')
      {
        this.errorMessage = ex.error;
      }
      else
      {
        this.errorMessage = ex.message;
      }
    }
    );
  }
  
  public Title():string
  {
    if (this.type == "special") {
      return "Special Customers";
    }
    else if (this.type == "all") {
      return "All Customers";
    }
    else {
      return "Public Customers";
    }
  }
}
