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
import { UpdataBalanceComponent } from './updata-balance/updata-balance.component';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';
import { TransactionAccountDetailsComponent } from './transaction-account-details/transaction-account-details.component';
import { EmailVerificationComponent } from './email-verification/email-verification.component';

const _routes: Routes = [
  {path:"",component:LogInComponent}, 
  {path:"login",component:LogInComponent},
  {path:"signUp",component:SignUPComponent},
  {path:"menu",component:CustomerMenuComponent},
  {path:"accountInfo",component:AccountInfoComponent},
  {path:"main",component:MainCustomerComponent},
  {path:"transaction",component:UpdataBalanceComponent},
  {path:"operationsHistory",component:OperationsHistoryComponent},
 
  ]; 

@NgModule({
  declarations: [
    LogInComponent,
    SignUPComponent,
    CustomerMenuComponent,
    AccountInfoComponent,
    MainCustomerComponent,
    UpdataBalanceComponent,
    OperationsHistoryComponent,
    TransactionAccountDetailsComponent,
    EmailVerificationComponent
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
