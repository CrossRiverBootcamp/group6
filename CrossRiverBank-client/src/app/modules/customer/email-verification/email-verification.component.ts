import { _fixedSizeVirtualScrollStrategyFactory } from '@angular/cdk/scrolling';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { CustomerService } from 'src/app/services/customer.service';
import { emailConfirmationService } from 'src/app/services/emailVerification.services';


@Component({
  selector: 'app-email-verification',
  templateUrl: './email-verification.component.html',
  styleUrls: ['./email-verification.component.css']
})
export class EmailVerificationComponent implements OnInit {
  confirmEmailCodeForm!: FormGroup;

  constructor(private _emailVerification: emailConfirmationService,private _customerService:CustomerService) {
    this.confirmEmailCodeForm = new FormGroup({
      "code": new FormControl("", [Validators.required, Validators.minLength(4), Validators.maxLength(4)]),
    });
  }

  ngOnInit(): void {
    
  }
  confirmCode() {
    this._emailVerification.ConfirmEmailCode(this.confirmEmailCodeForm.value).subscribe((res) => {
      if(res)
      {
        //this._customerService.register(new RegisterDTO())
        //_router.navigate("/main");
        //finish comp///
      }
      else{
        alert("you are not permited! try again...");
        //style="display: inline-block;"
      }
    }, (err) => { 
      alert("not confirmed your email")
      //style="display: inline-block;"
    });
  }
  sendConfirmCode() {
    this._emailVerification.SendEmailCode().subscribe((res)=>{
      alert("send you a new code");
    },(err)=>{
      alert("not allowed go to sign up---");
       //btnSignUp-style="display: inline-block;"
      });
  }
  goToRegister(){
    //_router.nevigate('/signUp');
  }
}
