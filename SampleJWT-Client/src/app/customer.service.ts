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

  public SpecialCustomers():Promise<CustomerDto[]>
  {
    return this.http.get<CustomerDto[]>(this.baseUrl+"GetSpecialCustomers").toPromise();    
  }

  public AllCustomers(page:number,pageSize:number):Promise<CustomerDto[]>
  {
    return this.http.get<CustomerDto[]>(this.baseUrl+"GetAllCustomers?page="+page+"&pageSize="+pageSize).toPromise();    
  }

  public CountAllCustomers():Promise<number>
  {
    return this.http.get<number>(this.baseUrl+"CountAllCustomers").toPromise();    
  }
}
