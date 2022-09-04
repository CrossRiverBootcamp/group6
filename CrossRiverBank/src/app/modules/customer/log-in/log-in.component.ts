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
 }
 logInForm: FormGroup = new FormGroup(
   {
     "email": new FormControl("", [Validators.required, Validators.email]),
     'password': new FormControl("", Validators.required)
   }
 );
 logIn(){
   this.loginUser=this.logInForm.value
  // this._router.navigate(['/menu']);
  //  this._loginService.logIn(this.loginUser).subscribe(id=>{
  //    debugger;
  //    if(id==null){
  //      alert("הפרטים שהכנסת שגויים")
  //    }
  //    alert(id);
         this._loginService.setCard(true);
         this._router.navigate(['/main']);
  //  })
  
 }
 register(){
  this._loginService.setCard(false);
   this._router.navigate(['/signUp']);
 }
}



