import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './modules/exceptions/not-found/not-found.component';


const routes: Routes = [
  {path:"customer",loadChildren:()=>import("./modules/customer/customer.module").then((m:any)=>m.CustomerModule)},
  { path: '**', component: NotFoundComponent }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
