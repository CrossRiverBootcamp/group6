import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class emailConfirmationService {
    verificationCodeUrl:string = 'https://localhost:7120/api/EmailVerification';
    ConfirmEmailCode(code: string): Observable<boolean> {
        return this._http.post<boolean>(this.verificationCodeUrl,code);
    }
    SendEmailCode():Observable<boolean>{
        return this._http.get<boolean>(this.verificationCodeUrl);
    }
    constructor(private _http: HttpClient) { }
}