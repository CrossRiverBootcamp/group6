import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AccountInfoDTO } from 'src/app/models/accountInfoDTO.models';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit {
  accountInfoDTO!:AccountInfoDTO;
  constructor() {
   
   }

  ngOnInit(): void {
    // this.accountInfoDTO.firstName="miriam";
    // this.accountInfoDTO.lastName="kaplan";
    // this.accountInfoDTO.balance=1000;
  }
  

}
