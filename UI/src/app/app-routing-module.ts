import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login';
import { MainPageComponent } from './Components/main-page-component/main-page-component';
import { PatchDetailsComponent } from './Components/patch-details-component/patch-details-component';
import { CustomerDetailsComponent } from './Components/customer-details-component/customer-details-component';
import { SignupComponent } from './Components/signup-component/signup-component';
import { NotificationComponent } from './Components/notification/notification';
import { ProductDetailsComponent } from './Components/product-details/product-details';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path : 'signup', component: SignupComponent},
  {
    path: 'home', component: MainPageComponent,
    children: [
      { path: 'patch-details', component: PatchDetailsComponent },
      { path: 'customer-details', component: CustomerDetailsComponent },
      { path: 'notifications', component: NotificationComponent},
      { path: 'product-details/:name', component: ProductDetailsComponent }
    ]
  },
  { path: '**', redirectTo: 'login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
