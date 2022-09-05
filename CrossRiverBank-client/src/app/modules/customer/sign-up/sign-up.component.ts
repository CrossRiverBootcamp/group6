import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomerService } from 'src/app/services/customer.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUPComponent implements OnInit {

  constructor(private _router: Router,private _customerService: CustomerService) { }

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
    
  }
  home(){
    this._router.navigate(['/login']);
  }
}
