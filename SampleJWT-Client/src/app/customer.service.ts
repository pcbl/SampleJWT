import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CustomerDto } from './model/CustomerDto';

@Injectable()
export class CustomerService {

  private baseUrl : string = 'http://localhost:51647/api/Customer/';
  constructor(private http: HttpClient) { }

  public PublicCustomers():Promise<CustomerDto[]>
  {
    return this.http.get<CustomerDto[]>(this.baseUrl+"GetPublicCustomers").toPromise();    
  }
}
