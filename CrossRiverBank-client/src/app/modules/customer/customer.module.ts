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
import { EmailVerificationComponent } from './email-verification/email-verification.component';
import { ForeignAccountInfoComponent } from './foreign-account-info/foreign-account-info.component';
import { CustomerIsActiveGuard } from 'src/app/guards/customer-is-active.guard';

const _routes: Routes = [
  {path:"",component:LogInComponent}, 
  {path:"login",component:LogInComponent},
  {path:"signUp",component:SignUPComponent},
  {path:"menu",component:CustomerMenuComponent,canActivate:[CustomerIsActiveGuard]},
  {path:"accountInfo",component:AccountInfoComponent,canActivate:[CustomerIsActiveGuard]},
  {path:"main",component:MainCustomerComponent,canActivate:[CustomerIsActiveGuard]},
  {path:"transaction",component:UpdataBalanceComponent,canActivate:[CustomerIsActiveGuard]},
  {path:"operationsHistory",component:OperationsHistoryComponent,canActivate:[CustomerIsActiveGuard]},
  {path:"EmailVerification",component:EmailVerificationComponent,canActivate:[CustomerIsActiveGuard]},
 
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
    EmailVerificationComponent,
    ForeignAccountInfoComponent
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
