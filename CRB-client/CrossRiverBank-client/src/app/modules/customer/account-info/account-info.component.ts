import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountInfoDTO } from 'src/app/models/accountInfoDTO.models';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit {
  accountInfoDTO!: AccountInfoDTO;
  accountInfoID!: number;

  constructor(private _loginService: LoginService, private _router: Router) { }

  ngOnInit(): void {
    this.accountInfoID = this._loginService.getAccountID();
    if (this.accountInfoID == 0) {
      alert("faild to get your details , try to login again!");
      this._router.navigate(['/login']);
    }
    this._loginService.GetAccountInfo(this.accountInfoID).subscribe(
      (res) => {
        debugger;
        this.accountInfoDTO = res;
      },
      (err) => {
        console.log(err);
        alert("faild to get your details");
      });
  }
}



