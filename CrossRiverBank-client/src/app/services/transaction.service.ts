import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AddTransactionDTO } from "../models/addTransactionDTO.models";




@Injectable({
  providedIn: 'root'
})
export class TransactionService {
private  transactionUrl='https://localhost:7120/api/Customer';   
      addTransaction(addTransaction: AddTransactionDTO): Observable<boolean> {
        debugger;
        return this._http.post<boolean>(this.transactionUrl + '/CreateAccount', addTransaction);
      }
    constructor(private _http : HttpClient){}
}