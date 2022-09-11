import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { getOperationDTO } from 'src/app/models/getOperationDTO.models';
import { OperationsHistoryDTO } from 'src/app/models/OperationsHistoryDTO.models';
import { LoginService } from 'src/app/services/login.service';
import { OperationsService } from 'src/app/services/operation.service';


@Component({
  selector: 'app-operations-history',
  templateUrl: './operations-history.component.html',
  styleUrls: ['./operations-history.component.css']
})
export class OperationsHistoryComponent implements OnInit {
  displayedColumns: string[] = ['credit', 'accountID', 'amount', 'balance','date'];
  dataSource = new MatTableDataSource<OperationsHistoryDTO>();
  currentAccountID: number = 0;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  constructor( private _loginService: LoginService,private  _operationsService: OperationsService) { }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  ngOnInit(): void {
    this.currentAccountID = this._loginService.getAccountID();
    if(this.currentAccountID==0){
      alert("faild to get your details");
    }
    this.getOperations();
  }
  getOperations(){
  const getOperationDT0: getOperationDTO = {
    currentAccountID: this.currentAccountID,
    pageNumber: 1,
    numberOfRecords: 5
  }
    this._operationsService.getOperation(getOperationDT0).subscribe(
      (res)=>{
        this.dataSource.data = res;
        console.log(res);
      },(err)=>{
        alert("faild to get your operations")
      }
    );
  }
  

}
