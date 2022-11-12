import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { MainMessageComponent } from './components/main-message/main-message.component';

const routes: Routes = [
  {path:"login", component:LoginComponent},
  {path:"",component:HomeComponent},
  {path:"message", component:MainMessageComponent}
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
