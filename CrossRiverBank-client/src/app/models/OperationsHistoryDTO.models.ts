export interface OperationsHistoryDTO{
    credit:boolean;
    accountId:number;
    amount:number;
    balance:number;
    date: Date;
    rowId?:number;
}