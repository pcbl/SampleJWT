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
  public isPaged: boolean = false;
  public customers: CustomerDto[];
  public loading: boolean;
  public errorMessage: string = "";
  constructor(private customerService: CustomerService) { }
  public page: number = 0;
  public size: number = 100;
  public pageSize: number = 5;

  ngOnInit() {
    this.loading = true;
    var call: Promise<CustomerDto[]>;
    if (this.type == "special") {
      call = this.customerService.SpecialCustomers();
    }
    else if (this.type == "all") {
      this.isPaged = true;

      //Counting number of pages!
      //And then we set the size as needed...
      var self = this;
      this.customerService.CountAllCustomers().then(data => {
        self.size = data;
      });

      call = this.customerService.AllCustomers(this.page,this.pageSize);
    }
    else {
      call = this.customerService.PublicCustomers();
    }
    call.then(data => {
      this.loading = false;
      this.customers = data;
    }).catch(ex => {
      this.loading = false;
      if (typeof ex.error === 'string') {
        this.errorMessage = ex.error;
      }
      else {
        this.errorMessage = ex.message;
      }
    }
    );
  }

  //Once page changes we make a new service call...
  private onPageChange = (pageNumber) => {
    var call = this.customerService.AllCustomers(this.page-1,this.pageSize);
    call.then(data => {
      this.loading = false;
      this.customers = data;
    }).catch(ex => {
      this.loading = false;
      if (typeof ex.error === 'string') {
        this.errorMessage = ex.error;
      }
      else {
        this.errorMessage = ex.message;
      }
    });
  }

  public Title(): string {
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
