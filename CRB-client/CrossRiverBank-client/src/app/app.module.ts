import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoginService } from './services/login.service';
import { InterceptService } from './services/intercept.service';
import { CustomerModule } from './modules/customer/customer.module';



@NgModule({
  declarations: [
    AppComponent,
   
  ],
  imports: [
    CustomerModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule
  ],
  providers: [
    LoginService,{
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptService,
      multi: true
     }],
  
  
  bootstrap: [AppComponent]
})
export class AppModule { }
