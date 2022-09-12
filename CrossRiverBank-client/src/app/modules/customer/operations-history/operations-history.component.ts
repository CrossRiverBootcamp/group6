import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ForeignAccountDTO } from 'src/app/models/foreignAccountDetailsDTO.models';
import { getOperationDTO } from 'src/app/models/getOperationDTO.models';
import { OperationsHistoryDTO } from 'src/app/models/OperationsHistoryDTO.models';
import { LoginService } from 'src/app/services/login.service';
import { OperationsService } from 'src/app/services/operation.service';


@Component({
  selector: 'app-operations-history',
  templateUrl: './operations-history.component.html',
  styleUrls: ['./operations-history.component.css'],
})
export class OperationsHistoryComponent implements OnInit {
  dataSource = new MatTableDataSource<OperationsHistoryDTO>();
  displayedColumns: string[] = ['credit', 'accountID', 'amount', 'balance', 'date'];
  currentAccountID: number = 0;
  foreignAccountDetails!: ForeignAccountDTO;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  constructor(private _loginService: LoginService, private _operationsService: OperationsService) { }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.currentAccountID = this._loginService.getAccountID();
    if (this.currentAccountID == 0) {
      alert("faild to get your details");
    }
    this.getOperations();
    this.foreignAccountDetails={
      firstName:"ayala",
      lastName:"bloy",
      email:"ayalabloy12@gmail.com",
    }
    //this.getforeignAccountDetails();
  }
  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<OperationsHistoryDTO>();
    
    // this.currentAccountID = this._loginService.getAccountID();
    // if(this.currentAccountID==0){
    //   alert("faild to get your details");
    // }
    // this.getOperations();
  }
  getNumOfOperations() {
    this._operationsService.getnumberOperation(this.currentAccountID).subscribe((res) => {
      this.paginator.length = res;
    }, (err) => { });
  }
  getforeignAccountDetails(accountID: number) {
    this._operationsService.getForeignAccountDetails(accountID).subscribe((res) => {
      this.foreignAccountDetails = res;
      console.log(res);
    }, (err) => { });
  }
 
  getOperationsFromDB(getOperationDT0: getOperationDTO) {
    this._operationsService.getOperation(getOperationDT0).subscribe(
      (res) => {
        this.dataSource.data = res;
        console.log(res);
        debugger;
        this.getNumOfOperations();
      }, (err) => {
        alert("faild to get your operations")
      }
    );
  }

  getOperations() {
    const getOperationDT0: getOperationDTO = {
      currentAccountID: this.currentAccountID,
      pageNumber: this.paginator.pageIndex,
      numberOfRecords: this.paginator.pageSize,
    }
    this.getOperationsFromDB(getOperationDT0);
  }

  getNextPage(pageEvent: PageEvent) {
    const getOperationDT0: getOperationDTO = {
      currentAccountID: this.currentAccountID,
      pageNumber: pageEvent.pageIndex,
      numberOfRecords: pageEvent.pageSize,
    }
    this.getOperationsFromDB(getOperationDT0);
  }



}
