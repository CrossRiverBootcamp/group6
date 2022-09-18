import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { getOperationDTO } from "../models/getOperationDTO.models";
import { OperationsHistoryDTO } from "../models/OperationsHistoryDTO.models";

@Injectable({
  providedIn: 'root'
})
export class OperationsService {

private  OperationUrl='https://localhost:7120/api/OperationsHistory'; 
      
      //get operation by page index & pageSizeOption
      getOperation(getOperation: getOperationDTO): Observable<OperationsHistoryDTO[]> {
        return this._http.get<OperationsHistoryDTO[]>(this.OperationUrl+`/OperationsHistory?accountID=${getOperation.currentAccountID}&page=${getOperation.pageNumber}&records=${getOperation.numberOfRecords}`);
      }

      // get number of total operations:
      setnumberOperation(accountID: number): Observable<number> {
        return this._http.get<number>(this.OperationUrl+`/${accountID}`);
      }
    constructor(private _http : HttpClient){}
}