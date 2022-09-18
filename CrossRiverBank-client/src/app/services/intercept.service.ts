import { Injectable, OnDestroy } from '@angular/core';
import { HttpInterceptor } from '@angular/common/http';
import { HttpRequest } from '@angular/common/http';
import { Observable} from 'rxjs';
import { HttpHandler } from '@angular/common/http';
import { HttpEvent } from '@angular/common/http';
import { LoginService } from './login.service';


@Injectable({
  providedIn: 'root'
})
export class InterceptService implements HttpInterceptor, OnDestroy {

  constructor(private _loginService:LoginService) { }
    ngOnDestroy(): void {
        throw new Error('Method not implemented.');
    }

  token: string="";


  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    debugger;
    this.token=this._loginService.getToken()
    if(this.token!=null){
      const tokenreq = req.clone({ 
        headers: req.headers.set('Authorization', 'Bearer '+this.token) });

    return next.handle(tokenreq);
    }
return next.handle(req);
}  
}