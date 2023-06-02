import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'MarketShoesFront';
  activeElement: string = ""

  openModalWindow(type: string) {
    this.activeElement = type;
  }

  close() {
    this.activeElement="";
  }
}


