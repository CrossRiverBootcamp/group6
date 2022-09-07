import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { RegisterDTO } from "../models/registerDTO.models";



@Injectable({
  providedIn: 'root'
})
export class CustomerService {
private  customerAcountUrl='https://localhost:7206/api/Transaction';  

      register(customer: RegisterDTO): Observable<boolean> {
        debugger;
        return this._http.post<boolean>(this.customerAcountUrl, customer);
      }
    constructor(private _http : HttpClient){}
}