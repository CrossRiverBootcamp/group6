import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AccountInfoDTO } from '../models/accountInfoDTO.models';
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
    debugger;
    return this._http.post<LoginResultDTO>(this.customerAcountUrl + '/login', loginUser);
  }


  GetAccountInfo(cardID: number): Observable<AccountInfoDTO> {
    debugger;
    // this.token = sessionStorage.getItem('token') || '';
    // const httpOptions = {
    //   Headers: new HttpHeaders({
    //     'Content-Type': 'application/json',
    //     'Authorization': 'Bearer ' + this.token,
    //   })
    // }
    return this._http.get<AccountInfoDTO>(this.customerAcountUrl + `/${cardID}`);
  }

  constructor(private _http: HttpClient) { }
}

