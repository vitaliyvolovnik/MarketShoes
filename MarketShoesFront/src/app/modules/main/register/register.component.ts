import {Component, EventEmitter, NgModule, Output} from '@angular/core';

import {RegisterModel} from "../../../api/models/RegisterModel";
import {RippleModule} from "primeng/ripple";
import {InputTextModule} from "primeng/inputtext";

import {ButtonModule} from "primeng/button";
import {CommonModule} from "@angular/common";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {first} from "rxjs";
import {AuthHttpService} from "../../../api/services/auth-http.service";
import {SecurityService} from "../../../services/security.service";
import {MessageService} from "primeng/api";
import {RadioButtonModule} from "primeng/radiobutton";
import {CheckboxModule} from "primeng/checkbox";
import {Role} from "../../../api/models/enums/Role";


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registerForm: FormGroup;
  isSeller:boolean= false;
  @Output() closeClick = new EventEmitter<string>();

  constructor(private fb: FormBuilder,
              private authService: AuthHttpService,
              private securityService: SecurityService,
              private messageService: MessageService) {
    this.registerForm = this.fb.group({
      Email: ['', [Validators.required, Validators.email]],
      Firstname: ['', [Validators.required, Validators.pattern('[a-zA-Z ]*')]],
      Lastname: ['', [Validators.required, Validators.pattern('[a-zA-Z ]*')]],
      Number: ['', [Validators.required, Validators.pattern('[0-9]*')]],
      Password: ['', [Validators.required, Validators.minLength(6)]],
      isSeller: [false]
    });
  }

  register() {

    if (this.registerForm.valid) {
      let registerModel: RegisterModel = {...this.registerForm.value};
      if(this.registerForm.value.isSeller[0]){
        registerModel.Role = Role.SELLER;
      }
      else{
        registerModel.Role = Role.CUSTOMER;
      }
      this.authService.register(registerModel)
        .pipe(first())
        .subscribe({
          next: () => {
            this.messageService.add({severity: 'success', summary: 'Регістрація', detail: 'регістрація відбулась успішно'});
          },
          error: (err) => {
            this.messageService.add({severity: 'error', summary: 'Регістрація', detail: 'не вдалося зареєструватися'});
          }
        });
    }
  }

  close() {
    this.closeClick.emit("")
  }
}


@NgModule({
  declarations: [
    RegisterComponent
  ],
  exports: [
    RegisterComponent
  ],
  imports: [
    CommonModule,
    RippleModule,
    InputTextModule,
    ButtonModule,
    FormsModule,
    ReactiveFormsModule,
    RadioButtonModule,
    CheckboxModule,

  ]
})
export class RegisterModule {
}
