import { Component } from '@angular/core';
import { LoginService } from './services/login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  card:boolean=false;
  constructor(private _loginService: LoginService) {
    this._loginService.getCard().subscribe((data:any) => {
  
      this.card = data;
  

  })
 
  }
  title = 'CrossRiverBank';
}
