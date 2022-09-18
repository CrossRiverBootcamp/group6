import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { LoginService } from '../services/login.service';

@Injectable({
  providedIn: 'root'
})
export class CustomerIsActiveGuard implements CanActivate {
  constructor(private _loginService:LoginService){}
  active:boolean=false;
  accountId:number=0;
  canActivate()
  {
    this.accountId=this._loginService.getAccountID()
    if(this.accountId==0){
      alert("login- do not touch url");
      return false;
    }
    return true;
  }   
  
}
