import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {HeaderModule} from './modules/main/market-header/market-header.component';
import {MarketFooterComponent } from './modules/main/market-footer/market-footer.component';
import {RegisterModule} from './modules/main/register/register.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";
import {ToastModule} from "primeng/toast";
import {MessageService} from "primeng/api";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ButtonModule} from "primeng/button";
import {CheckboxModule} from "primeng/checkbox";
import {InputTextModule} from "primeng/inputtext";
import {RippleModule} from "primeng/ripple";
import {LoginModule} from "./modules/main/login/login.component";

@NgModule({
  bootstrap: [AppComponent],
  declarations: [
    AppComponent,
    MarketFooterComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    HeaderModule,
    RegisterModule,
    FormsModule,
    HttpClientModule,
    ToastModule,
    ButtonModule,
    CheckboxModule,
    InputTextModule,
    ReactiveFormsModule,
    RippleModule,
    LoginModule
  ],
  providers: [MessageService]
})
export class AppModule { }
