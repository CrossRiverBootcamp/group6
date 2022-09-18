import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { RegisterDTO } from "../models/registerDTO.models";

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
private  customerAcountUrl='https://localhost:7120/api/Customer/CreateAccount';  

      register(customer: RegisterDTO): Observable<boolean> {
        return this._http.post<boolean>(this.customerAcountUrl, customer);
      }
    constructor(private _http : HttpClient){}
}