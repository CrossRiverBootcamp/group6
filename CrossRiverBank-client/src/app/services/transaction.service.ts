import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AddTransactionDTO } from "../models/addTransactionDTO.models";

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private transactionUrl = 'https://localhost:7206/api/Transaction';

  addTransaction(addTransaction: AddTransactionDTO): Observable<boolean> {
    return this._http.post<boolean>(this.transactionUrl, addTransaction);
  }
  constructor(private _http: HttpClient) { }
}