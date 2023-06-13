import {Component, EventEmitter, NgModule, OnInit, Output} from '@angular/core';
import {SecurityService} from "../../../services/security.service";
import {AsyncPipe, NgIf} from "@angular/common";

@Component({
  selector: 'app-market-header',
  templateUrl: './market-header.component.html',
  styleUrls: ['./market-header.component.scss']
})
export class MarketHeaderComponent implements OnInit {
  @Output() buttonClick = new EventEmitter<string>();

  constructor(public securityService:SecurityService) {

  }

  ngOnInit(): void {
  }


  loginClick() {
    this.buttonClick.emit("login")
  }

  registerClick() {
    this.buttonClick.emit("register")
  }

  loguot() {
    this.securityService.logout();
  }

  basketClick() {
    this.buttonClick.emit("basket")
  }
}

@NgModule({
  declarations: [
    MarketHeaderComponent
  ],
  exports: [
    MarketHeaderComponent
  ],
  imports: [
    NgIf,
    AsyncPipe

  ]
})
export class HeaderModule { }
