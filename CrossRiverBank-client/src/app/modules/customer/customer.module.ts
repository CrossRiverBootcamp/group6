import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogInComponent } from './log-in/log-in.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';
import { SignUPComponent } from './sign-up/sign-up.component';
import { CustomerMenuComponent } from './customer-menu/customer-menu.component';
import { AccountInfoComponent } from './account-info/account-info.component';
import { MainCustomerComponent } from './main-customer/main-customer.component';

const _routes: Routes = [
  {path:"",component:LogInComponent}, 
  {path:"login",component:LogInComponent},
  {path:"signUp",component:SignUPComponent},
  {path:"menu",component:CustomerMenuComponent},
  {path:"accountInfo",component:AccountInfoComponent},
  {path:"main",component:MainCustomerComponent},
 
  ]; 

@NgModule({
  declarations: [
    LogInComponent,
    SignUPComponent,
    CustomerMenuComponent,
    AccountInfoComponent,
    MainCustomerComponent
  ],
  imports: [
   CommonModule, 
    RouterModule.forChild(_routes),
    ReactiveFormsModule,
    FormsModule,
    MaterialModule
  ],
  exports:[CustomerMenuComponent]
})
export class CustomerModule { }
