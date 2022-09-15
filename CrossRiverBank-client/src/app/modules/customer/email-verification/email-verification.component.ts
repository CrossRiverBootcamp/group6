import { _fixedSizeVirtualScrollStrategyFactory } from '@angular/cdk/scrolling';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { RegisterDTO } from 'src/app/models/registerDTO.models';
import { CustomerService } from 'src/app/services/customer.service';
import { emailConfirmationService } from 'src/app/services/emailVerification.services';


@Component({
  selector: 'app-email-verification',
  templateUrl: './email-verification.component.html',
  styleUrls: ['./email-verification.component.css']
})
export class EmailVerificationComponent implements OnInit {
  registerFormDTO!:RegisterDTO;


  constructor(public _dialog: MatDialog,private _router: Router,private _emailVerification: emailConfirmationService,private _customerService:CustomerService,@Inject(MAT_DIALOG_DATA) public data: any) {
  }

  ngOnInit(): void {
    debugger;
   this.registerFormDTO=this.data.registerForm;
  }
  SendCodeAgain() {
    this._emailVerification.SendEmailCode(this.registerFormDTO.VerifiCode).subscribe((res) => 
    {
      alert("send you a new code");
    }, (err) => { 
      alert("not confirmed your email")
     
    });
  }
  ConfirmCode() {
    debugger;
    this.registerFormDTO.VerifiCode=this.data.VerifiCode;
    this._customerService.register(this.registerFormDTO).subscribe((res)=>{
      alert("your registeration past sucsessfuly your redy to login");
      this._router.navigate(['/login']);
      this._dialog.closeAll();
    },(err)=>{
      alert("not allowed go to sign up check your deatield and try again");
      });
  }
  goToRegister(){
    this._dialog.closeAll();
  }
}
