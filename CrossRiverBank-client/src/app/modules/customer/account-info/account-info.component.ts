import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Route } from '@angular/router';
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
  constructor(private _loginService: LoginService)//,private _router: Route) 
  {
   
   }

  ngOnInit(): void {
    //what can we do about the 0???
    this.accountInfoID=this._loginService.getAccountID();
    if(this.accountInfoID==0){
      alert("faild to get your details");
      // this._router.navigate(['/login']);
    }
    this._loginService.GetAccountInfo(this.accountInfoID).subscribe(
      (res)=>{
      this.accountInfoDTO=res;
      console.log(res)
    debugger;
    },
    (err)=>{
   alert("faild to get your details");
      
    });
  }
  
}



