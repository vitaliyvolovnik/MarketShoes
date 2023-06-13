import {RouterModule, Routes} from "@angular/router";
import {MainModule} from "./modules/main/main.module";
import {NgModule} from "@angular/core";


const routes: Routes = [
  //{path: "admin", loadChildren: () => import('./modules/admin/admin.module').then(m => m.AdminModule),canActivate:[AdminGuard]},
  {path: "", loadChildren: () => import('./modules/main/main.module').then(m => m.MainModule)},
  {path: "confirm/:token", loadChildren: () => import('./modules/main/confirm/confirm.component').then(m => m.ConfirmModule)},

]

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}

