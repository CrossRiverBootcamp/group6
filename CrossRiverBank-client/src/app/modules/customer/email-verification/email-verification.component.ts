import { _fixedSizeVirtualScrollStrategyFactory } from '@angular/cdk/scrolling';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
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
  confirmEmailCodeForm!: FormGroup;
  registerFormDTO!:RegisterDTO;

  constructor(private _router: Router,private _emailVerification: emailConfirmationService,private _customerService:CustomerService,@Inject(MAT_DIALOG_DATA) public data: any) {
    this.confirmEmailCodeForm = new FormGroup({
      "VerifiCode": new FormControl("", [Validators.required, Validators.minLength(4), Validators.maxLength(4)]),
    });
  }

  ngOnInit(): void {
    debugger;
    this.registerFormDTO=this.data.registerDTO;
  }
  SendCodeAgain() {
    this._emailVerification.SendEmailCode(this.registerFormDTO.email).subscribe((res) => 
    {
      alert("send you a new code");
    }, (err) => { 
      alert("not confirmed your email")
     
    });
  }
  ConfirmCode() {
    this.registerFormDTO.VerifiCode=this.confirmEmailCodeForm.value;
    this._customerService.register(this.registerFormDTO).subscribe((res)=>{
      alert("your registeration past sucsessfuly your redy to login");
      this._router.navigate(['/login']);
    },(err)=>{
      alert("not allowed go to sign up--- check your deatield and try again");
      });
  }
  goToRegister(){
    this._router.navigate(['/signUp']);
  }
}
