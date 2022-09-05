import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { AccountInfoDTO } from 'src/app/models/accountInfoDTO.models';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit {
  accountInfoDTO!:AccountInfoDTO;
  accountInfoID!:number;
  constructor(private _loginService: LoginService) {
   
   }

  ngOnInit(): void {
    this.accountInfoID=this._loginService.getAccountID();
    if(this.accountInfoID=0){
      alert("faild to get your details");
    }
    this._loginService.GetAccountInfo(this.accountInfoID).subscribe(
      (res)=>{
      this.accountInfoDTO=res;
    },
    (err)=>{
   alert("faild to get your details");

    });
  }
  
}



