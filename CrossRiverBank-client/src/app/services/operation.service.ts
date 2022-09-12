import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AddTransactionDTO } from "../models/addTransactionDTO.models";
import { ForeignAccountDTO } from "../models/foreignAccountDetailsDTO.models";
import { getOperationDTO } from "../models/getOperationDTO.models";
import { OperationsHistoryDTO } from "../models/OperationsHistoryDTO.models";




@Injectable({
  providedIn: 'root'
})
export class OperationsService {
private  OperationUrl='https://localhost:7120/api/OperationsHistory';  
      getOperation(getOperation: getOperationDTO): Observable<OperationsHistoryDTO[]> {
        debugger;
        return this._http.get<OperationsHistoryDTO[]>(this.OperationUrl+`/OperationsHistory?id=${getOperation.currentAccountID}&page=${getOperation.pageNumber}&records=${getOperation.numberOfRecords}`);
      }
      getnumberOperation(accountID: number): Observable<number> {
        debugger;
        return this._http.get<number>(this.OperationUrl+`/${accountID}`);
      }
      getForeignAccountDetails(otherAccountID: number): Observable<ForeignAccountDTO> {
        debugger;
        return this._http.get<ForeignAccountDTO>(this.OperationUrl+`/foreignDetails/${otherAccountID}`);
      }
    constructor(private _http : HttpClient){}
}