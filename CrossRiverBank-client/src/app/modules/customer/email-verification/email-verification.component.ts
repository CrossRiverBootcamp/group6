import { _fixedSizeVirtualScrollStrategyFactory } from '@angular/cdk/scrolling';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { LoginDTO } from 'src/app/models/loginDTO.models';
import { RegisterDTO } from 'src/app/models/registerDTO.models';
import { CustomerService } from 'src/app/services/customer.service';
import { emailConfirmationService } from 'src/app/services/emailVerification.services';
import { LoginService } from 'src/app/services/login.service';


@Component({
  selector: 'app-email-verification',
  templateUrl: './email-verification.component.html',
  styleUrls: ['./email-verification.component.css']
})
export class EmailVerificationComponent implements OnInit {
  registerFormDTO!:RegisterDTO;
  loginUser!:LoginDTO;


  constructor(public _dialog: MatDialog,private _router: Router,private _emailVerification: emailConfirmationService,private _customerService:CustomerService,@Inject(MAT_DIALOG_DATA) public data: any,private _loginService:LoginService) {
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
    this.logIn();   
    this._dialog.closeAll();
    },(err)=>{
      alert("not allowed go to sign up check your deatield and try again");
      });
  }
  goToRegister(){
    this._dialog.closeAll();
  }

  logIn() {
    this.loginUser.email=this.registerFormDTO.email;
      this.loginUser.password=this.registerFormDTO.password;
    debugger;
    this._loginService.logIn(this.loginUser).subscribe(
          (res) => {
            debugger;
            if(res.accountID<=0){
              alert("check your details somthing there is ronge");
            }
        else{
        this._loginService.setAccountID(res.accountID);
        this._loginService.setCard(true);
        sessionStorage.setItem('token', res.token)
        this._router.navigate(['/main']);
      }
    },
   (err) => {
    debugger;
             alert("faild to login try again");
             console.log(err);
             });
      }
}
