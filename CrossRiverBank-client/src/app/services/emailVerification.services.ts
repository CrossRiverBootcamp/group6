import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class emailConfirmationService {
    verificationCodeUrl:string = 'https://localhost:7120/api/EmailVerification';
    SendEmailCode(email: string): Observable<boolean> {
        debugger;
        return this._http.post<boolean>(this.verificationCodeUrl,JSON.stringify(email),{headers: {'Content-Type': 'application/json'}});
    }
    constructor(private _http: HttpClient) { }
}