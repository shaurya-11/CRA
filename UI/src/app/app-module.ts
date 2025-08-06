import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { LoginComponent } from './Components/login/login';
import { MainPageComponent } from './Components/main-page-component/main-page-component';
import { PatchDetailsComponent } from './Components/patch-details-component/patch-details-component';
import { CustomerDetailsComponent } from './Components/customer-details-component/customer-details-component';
import { SignupComponent } from './Components/signup-component/signup-component';
import { NotificationComponent } from './Components/notification/notification';
import { ProductDetailsComponent } from './Components/product-details/product-details';
import { AuthInterceptor } from './Interceptors/auth-interceptor';

@NgModule({
  declarations: [
    App,
    LoginComponent,
    MainPageComponent,
    SignupComponent,
    PatchDetailsComponent,
    CustomerDetailsComponent,
    NotificationComponent,
    ProductDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [App]
})
export class AppModule { }
