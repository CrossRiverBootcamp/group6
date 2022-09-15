import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { RegisterDTO } from 'src/app/models/registerDTO.models';
import { CustomerService } from 'src/app/services/customer.service';
import { emailConfirmationService } from 'src/app/services/emailVerification.services';
import { EmailVerificationComponent } from '../email-verification/email-verification.component';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUPComponent implements OnInit {

  constructor(private _router: Router,private _customerService: CustomerService,private _dialog:MatDialog,private _emailVerification: emailConfirmationService) {
   }
   openDialog(registerForm:RegisterDTO) {
    debugger;
  const dialogRef=  this._dialog.open(EmailVerificationComponent, {
      data: {
        registerForm:registerForm
      },
    });
  }
  ngOnInit(): void {
  }
  signUpForm: FormGroup = new FormGroup(
    {
      "email": new FormControl("", [Validators.required, Validators.email]),
      'password': new FormControl("", [Validators.required, Validators.minLength(8)]),
      'firstName': new FormControl("", Validators.required),
      'lastName': new FormControl("", Validators.required),
      'VerifiCode': new FormControl("",),
    }
  );
  register(){
    var newRegistration: RegisterDTO = this.signUpForm.value;
    this.sendEmail(newRegistration.email);
    this.openDialog(newRegistration);
  }
  home(){
    this._router.navigate(['/login']);
  }

sendEmail(email:string){
  this._emailVerification.SendEmailCode(email).subscribe((res) => 
  {
    alert("send you a new code");
  }, (err) => { 
    alert("not confirmed your email")
   
  });
}
}