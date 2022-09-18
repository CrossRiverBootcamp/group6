import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AccountInfoDTO } from '../models/accountInfoDTO.models';
import { ForeignAccountDTO } from '../models/foreignAccountDetailsDTO.models';
import { LoginDTO } from '../models/loginDTO.models';
import { LoginResultDTO } from '../models/loginResultDTO.models';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private customerAcountUrl = 'https://localhost:7120/api/Account';
  
  isCustomer: boolean = false;
  ID: number = 0;
  token: string = '';
  private card: BehaviorSubject<boolean> = new BehaviorSubject(this.isCustomer);
  private acountId: BehaviorSubject<number> = new BehaviorSubject(this.ID);
  private tokenJWT: BehaviorSubject<string> = new BehaviorSubject(this.token);

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
  getToken() {
    return this.tokenJWT.value;
  }
  setToken(_token: string) {
    this.tokenJWT.next(_token);
  }
  logIn(loginUser: LoginDTO): Observable<LoginResultDTO> {
    return this._http.post<LoginResultDTO>(this.customerAcountUrl + '/login', loginUser);
  }
  GetAccountInfo(cardID: number): Observable<AccountInfoDTO> {
    return this._http.get<AccountInfoDTO>(this.customerAcountUrl + `/${cardID}`);
  }
  //get name and email of other accountID
  GetForeignAccountDetails(accountID: number): Observable<ForeignAccountDTO> {
    return this._http.get<ForeignAccountDTO>(this.customerAcountUrl + `?foreignAccountID=${accountID}`);
  }
  constructor(private _http: HttpClient) { }
}
