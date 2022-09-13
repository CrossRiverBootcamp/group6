import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AccountInfoDTO } from '../models/accountInfoDTO.models';
import { LoginDTO } from '../models/loginDTO.models';
import { LoginResultDTO } from '../models/loginResultDTO.models';
import { HttpHeaders } from '@angular/common/http';
import { ForeignAccountDTO } from '../models/foreignAccountDetailsDTO.models';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private customerAcountUrl = 'https://localhost:7120/api/Account';
  isCustomer: boolean = false;
  ID: number = 0;
  // token: string = '';
  private card: BehaviorSubject<boolean> = new BehaviorSubject(this.isCustomer);
  private acountId: BehaviorSubject<number> = new BehaviorSubject(this.ID);

  getCard() {
    return this.card.asObservable();
  }
  setCard(_authorized: boolean) {
    this.card.next(_authorized);
  }
  getAccountID() {
    return this.acountId.value;
  }
  setAccountID(_id: number) {
    this.acountId.next(_id);
  }
  logIn(loginUser: LoginDTO): Observable<LoginResultDTO> {
    return this._http.post<LoginResultDTO>(this.customerAcountUrl + '/login', loginUser,);
  }


  GetAccountInfo(cardID: number): Observable<AccountInfoDTO> {
    // this.token = sessionStorage.getItem('token') || '';
    // const httpOptions = {
    //   Headers: new HttpHeaders({
    //     'Content-Type': 'application/json',
    //     'Authorization': 'Bearer ' + this.token,
    //   })
    // }
    return this._http.get<AccountInfoDTO>(this.customerAcountUrl + `/${cardID}`);
  }
  //get name and email of other accountID
  GetForeignAccountDetails(accountID: number): Observable<ForeignAccountDTO> {
    return this._http.get<ForeignAccountDTO>(this.customerAcountUrl + `?foreignAccountID=${accountID}`);
  }

  constructor(private _http: HttpClient) { }
}
