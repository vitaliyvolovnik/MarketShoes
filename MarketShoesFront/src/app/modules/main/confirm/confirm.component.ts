import {Component, NgModule} from '@angular/core';
import {ActivatedRoute, Route, Router, RouterModule} from "@angular/router";
import {CommonModule} from "@angular/common";
import {MessageService} from "primeng/api";
import {AuthHttpService} from "../../../api/services/auth-http.service";
import {first} from "rxjs";

@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.css']
})
export class ConfirmComponent {


  constructor(private router:Router,
              private messageService:MessageService,
              private authService:AuthHttpService,
              private activatedRoute: ActivatedRoute) {

    const token = activatedRoute.snapshot.params['token'];

    this.authService.confirm(token)
      .pipe(first())
      .subscribe({
        next:()=>{messageService.add({severity:'success', summary: 'Email', detail: 'Email confirmed'})},
        error:() =>{messageService.add({severity:'error', summary: 'Email', detail: 'an error occurred while confirming the email'})}
      })
    this.router.navigate([""])
  }
}

@NgModule({
  declarations: [ConfirmComponent],
  imports: [
    RouterModule.forChild([{path:"",component:ConfirmComponent}]),
    CommonModule
  ]
})
export class ConfirmModule { }

