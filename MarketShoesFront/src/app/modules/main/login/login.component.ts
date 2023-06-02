import {Component, EventEmitter, NgModule, Output} from '@angular/core';
import {CommonModule} from "@angular/common";
import {RippleModule} from "primeng/ripple";
import {InputTextModule} from "primeng/inputtext";
import {ButtonModule} from "primeng/button";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {RadioButtonModule} from "primeng/radiobutton";
import {AuthHttpService} from "../../../api/services/auth-http.service";
import {SecurityService} from "../../../services/security.service";
import {MessageService} from "primeng/api";
import {first} from "rxjs";
import {Credentials} from "../../../api/models/Credentials";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;
  @Output() closeClick = new EventEmitter<string>();

  constructor(private fb: FormBuilder,
              private authService: AuthHttpService,
              private securityService: SecurityService,
              private messageService: MessageService) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  login() {

    if (this.loginForm.valid) {
      let credentials: Credentials = {...this.loginForm.value};
      this.authService.login(credentials)
        .pipe(first())
        .subscribe({
          next: () => {
            this.messageService.add({severity: 'success', summary: 'Авторизація', detail: 'авторизація відбулась успішно'});
          },
          error: (err) => {
            this.messageService.add({severity: 'error', summary: 'Авторизація', detail: 'не вдалося авторизуватися'});
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
    LoginComponent
  ],
  exports: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    RippleModule,
    InputTextModule,
    ButtonModule,
    FormsModule,
    ReactiveFormsModule,
    RadioButtonModule,

  ]
})
export class LoginModule {
}
