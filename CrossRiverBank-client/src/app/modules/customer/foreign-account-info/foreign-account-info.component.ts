import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ForeignAccountDTO } from 'src/app/models/foreignAccountDetailsDTO.models';

@Component({
  selector: 'app-foreign-account-info',
  templateUrl: './foreign-account-info.component.html',
  styleUrls: ['./foreign-account-info.component.css']
})
export class ForeignAccountInfoComponent implements OnInit {
    accountDetails! : ForeignAccountDTO;
  constructor(public _dialog: MatDialog,@Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
    debugger
    this.accountDetails = this.data.details;
  }
  CloseDialog()
  {
    this._dialog.closeAll();
  }

}
