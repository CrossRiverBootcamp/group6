import { Injectable, OnDestroy } from '@angular/core';
import { HttpInterceptor } from '@angular/common/http';
import { HttpRequest } from '@angular/common/http';
import { Observable} from 'rxjs';
import { HttpHandler } from '@angular/common/http';
import { HttpEvent } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class InterceptService implements HttpInterceptor, OnDestroy {

  constructor() { }
    ngOnDestroy(): void {
        throw new Error('Method not implemented.');
    }

  token: string="";


  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
      const tokenreq = req.clone({ headers: req.headers.set('Authorization', 'Bearer '  ) });
      return next.handle(tokenreq);
    }

  

  
}