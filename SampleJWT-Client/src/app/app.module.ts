import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CustomerComponent } from './customer/customer.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoginComponent } from './login/login.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms'


import { AuthService } from './auth.service';
import { AuthGuard } from './auth.guard';
import { CustomerService } from './customer.service';
import { CustomHttpInterceptor } from './CustomHttpInterceptor';

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'customer', component: CustomerComponent , canActivate: [AuthGuard]},
  { path: 'customer/:id', component: CustomerComponent , canActivate: [AuthGuard]},
  {    
    path: 'home',    
    component: HomeComponent,
  },
  {    
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'    
  },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    CustomerComponent,
    PageNotFoundComponent,
    HomeComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    NgbModule.forRoot(),
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- debugging purposes only
    ),
    FormsModule, 
    ReactiveFormsModule
  ],
  providers: [AuthService,AuthGuard,CustomerService,
    {  provide: HTTP_INTERCEPTORS,
      useClass: CustomHttpInterceptor,
      multi: true
   }],
  bootstrap: [AppComponent]
})
export class AppModule {  
}
