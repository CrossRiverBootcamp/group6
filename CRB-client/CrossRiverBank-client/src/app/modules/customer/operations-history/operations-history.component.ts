import { animate, state, style, transition, trigger } from '@angular/animations';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ForeignAccountDTO } from 'src/app/models/foreignAccountDetailsDTO.models';
import { getOperationDTO } from 'src/app/models/getOperationDTO.models';
import { OperationsHistoryDTO } from 'src/app/models/OperationsHistoryDTO.models';
import { LoginService } from 'src/app/services/login.service';
import { OperationsService } from 'src/app/services/operation.service';
import { ForeignAccountInfoComponent } from '../foreign-account-info/foreign-account-info.component';


@Component({
  selector: 'app-operations-history',
  templateUrl: './operations-history.component.html',
  styleUrls: ['./operations-history.component.css'],
})
export class OperationsHistoryComponent implements OnInit {
  dataSource = new MatTableDataSource<OperationsHistoryDTO>();
  displayedColumns: string[] = ['credit', 'accountId', 'amount', 'balance', 'date'];
  numOfOperaitons: number = 0;
  pageSizeOptions = [2, 4, 6];
  currentAccountID = 0;
  pageNumber = 0;
  numberOfRecords = 4;
  // };
  foreignAccountDetails: ForeignAccountDTO = {
    accountId: 0,
    firstName: " ",
    lastName: " ",
    email: " ",
  };
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  constructor(private _loginService: LoginService, private _operationsService: OperationsService, private _dialog: MatDialog) {

  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  ngOnInit(): void {
    this.currentAccountID = this._loginService.getAccountID();
    this.getNumOfOperations();
    this.getOperationsFromDB();
  }

  getNumOfOperations() {
    this._operationsService.setnumberOperation(this.currentAccountID).subscribe((res) => {
      //intialize total number of operations-history
      this.numOfOperaitons = res;
    }, (err) => { console.log(err); });
  }
  getOperationsFromDB() {
    const getOperationDT0: getOperationDTO = {
      currentAccountID: this.currentAccountID,
      pageNumber: this.pageNumber,
      numberOfRecords: this.numberOfRecords,
    }
    this._operationsService.getOperation(getOperationDT0).subscribe(
      (data) => {
        if (data.length > 0) {
          this.dataSource.data = data;
          this.paginator.pageIndex = this.pageNumber;
          setTimeout(() => {
            this.paginator.pageIndex = this.pageNumber;
            this.paginator.length = this.numOfOperaitons;
          })
        }
        else {
          alert("dont have any more operations");
        }
      }, (err) => {
        alert("faild to get your operations");
        console.log(err);
      }
    );
  }
  //each page event get operations
  getNextPage(pageEvent: PageEvent) {
    this.pageNumber = pageEvent.pageIndex;
    this.numberOfRecords = pageEvent.pageSize;
    this.getOperationsFromDB();
  }
  //get foreign account details 
  getforeignAccountDetails(accountID: number) {
    this.openDialog(accountID);
  }

  openDialog(accountID: number) {
    const dialogRef = this._dialog.open(ForeignAccountInfoComponent, {
      data: {
        accountID: accountID,
      },
    });
  }
}
