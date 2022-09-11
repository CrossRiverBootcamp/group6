import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AddTransactionDTO } from "../models/addTransactionDTO.models";
import { getOperationDTO } from "../models/getOperationDTO.models";
import { OperationsHistoryDTO } from "../models/OperationsHistoryDTO.models";




@Injectable({
  providedIn: 'root'
})
export class OperationsService {
private  OperationUrl='https://localhost:7120/api/Account/GetOperations';  
      getOperation(getOperation: getOperationDTO): Observable<OperationsHistoryDTO[]> {
        debugger;
        return this._http.post<OperationsHistoryDTO[]>(this.OperationUrl, getOperation);
      }
    constructor(private _http : HttpClient){}
}