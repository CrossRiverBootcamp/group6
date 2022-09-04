import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  isCustomer: boolean = false;
    private card: BehaviorSubject<boolean> = new BehaviorSubject(this.isCustomer);

    getCard() {
        return this.card.asObservable();
      } 
      setCard(_authorized: boolean) {
        this.card.next(_authorized);
      }
      // logIn(loginuser: LoginDTO): Observable<number> {
      //   debugger
      //   return 1;
      //   //return this._http.post<number>("api/Subscriber/login", loginuser);
      // }
    constructor(private _http : HttpClient){}
}