import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ForeignAccountDTO } from 'src/app/models/foreignAccountDetailsDTO.models';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-foreign-account-info',
  templateUrl: './foreign-account-info.component.html',
  styleUrls: ['./foreign-account-info.component.css']
})
export class ForeignAccountInfoComponent implements OnInit {
  accountID!: number;
  foreignAccountDetails!: ForeignAccountDTO;
  constructor(public _dialog: MatDialog, @Inject(MAT_DIALOG_DATA) public data: any, private _loginService: LoginService) { }

  ngOnInit(): void {
    this.accountID = this.data.accountID;
    this.GetForeignAccountDetails(this.data.accountID);
  }
  CloseDialog() {
    this._dialog.closeAll();
  }
  GetForeignAccountDetails(accountID: number) {
    this._loginService.GetForeignAccountDetails(accountID).subscribe((res) => {
      this.foreignAccountDetails = {
        accountId: accountID,
        firstName: res.firstName,
        lastName: res.lastName,
        email: res.email,
      };
    },
      (err) => {
        alert("faild to get foregin account details");
        console.log(err);
      });
  }
}
