import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginDTO } from 'src/app/models/loginDTO.models';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {

  constructor(private _router: Router, private _loginService: LoginService) { }
  loginUser!:LoginDTO;
 ngOnInit(): void {
   this._loginService.setCard(false);
   this._loginService.setAccountID(0);
 }
 logInForm: FormGroup = new FormGroup(
   {
     "email": new FormControl("", [Validators.required, Validators.email]),
     'password': new FormControl("", Validators.required)
   }
 );
 logIn(){
   this.loginUser=this.logInForm.value
   debugger;
    this._loginService.logIn(this.loginUser).subscribe(
          (res) => {
            if(res<10){
              alert("הפרטים שהכנסת שגויים");
            }
        else{
        this._loginService.setAccountID(res);
        this._loginService.setCard(true);
        this._loginService.setAccountID(res);
        this._router.navigate(['/main']);
      }
    },
   (err) => {
             alert("faild to login try again");
             console.log(err);
             });
      }
 register(){
  this._router.navigate(['/signUp']);
 }
}



