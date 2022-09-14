import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { RegisterDTO } from 'src/app/models/registerDTO.models';
import { CustomerService } from 'src/app/services/customer.service';
import { EmailVerificationComponent } from '../email-verification/email-verification.component';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUPComponent implements OnInit {

  constructor(private _router: Router,private _customerService: CustomerService,private _dialog:MatDialog) {
   }
   openDialog(registerDTO:RegisterDTO) {
    debugger;
  const dialogRef=  this._dialog.open(EmailVerificationComponent, {
      data: {
        registerDTO:registerDTO
      },
    });
    dialogRef.afterClosed()
    .subscribe((x:any)=>{this.home();} );
  }
  ngOnInit(): void {
  }
  signUpForm: FormGroup = new FormGroup(
    {
      "email": new FormControl("", [Validators.required, Validators.email]),
      'password': new FormControl("", [Validators.required, Validators.minLength(8)]),
      'firstName': new FormControl("", Validators.required),
      'lastName': new FormControl("", Validators.required),
    }
  );
  register(){
    var newRegistration: RegisterDTO = this.signUpForm.value;
    this.openDialog(newRegistration);
  }
  home(){
    this._router.navigate(['/login']);
  }
}
