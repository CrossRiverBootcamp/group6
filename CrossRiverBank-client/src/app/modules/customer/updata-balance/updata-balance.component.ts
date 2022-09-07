import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AddTransactionDTO } from 'src/app/models/addTransactionDTO.models';
import { LoginService } from 'src/app/services/login.service';
import { TransactionService } from 'src/app/services/transaction.service';

@Component({
  selector: 'app-updata-balance',
  templateUrl: './updata-balance.component.html',
  styleUrls: ['./updata-balance.component.css']
})
export class UpdataBalanceComponent implements OnInit {
 accountID:number=0;
 addTransaction!:AddTransactionDTO;
  constructor(private _router: Router,private _loginService: LoginService,private _transactionService: TransactionService) { }

  ngOnInit(): void {
    this.accountID=this._loginService.getAccountID()
  }
  updateBalanceForm: FormGroup = new FormGroup(
    {
      'ToAccountID': new FormControl("", Validators.required),
      'Amount': new FormControl("", [Validators.required]),
    }
  );
  updateBalance(){
    this.addTransaction=this.updateBalanceForm.value;
    this.addTransaction.fromAccountID=this.accountID;
    this._transactionService.addTransaction(this.addTransaction).subscribe(
      (res)=>{
        this._router.navigate(['/main']);
        alert("your transaction past sucssefuly");
      },
      (err)=>{
        alert("sorry yor transaction faild check your details and try again");

      }
    )
  }
  

}
