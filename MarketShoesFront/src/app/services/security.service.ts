import {Router} from "@angular/router";
import {BehaviorSubject, Observable} from "rxjs";
import {SecurityUser} from "../api/models/SecurityUser";
import {Role} from "../api/models/enums/Role";
import {Injectable} from "@angular/core";

@Injectable({providedIn: "root"})
export class SecurityService {
  private readonly USER_KEY = "user";


  private _isAuthenticated$:BehaviorSubject<boolean> =new BehaviorSubject<boolean>(false);
  public isAuthenticated$:Observable<boolean> = this._isAuthenticated$.asObservable();

  constructor(private router:Router) {
    this._isAuthenticated$.next(this.isAuthenticated());
  }

  login(securityUser: SecurityUser) {
    this.updateUserInLocalStorage(securityUser);
    this._isAuthenticated$.next(true);
    return this.router.navigate([this.hasRole(Role.ADMIN)?'/admin':'/']);
  }

  logout() {
    this.removeUserFromLocaleStorage();
    this._isAuthenticated$.next(false);
    this.router.navigate(["/login"]);
  }


  private updateUserInLocalStorage(securityUser: SecurityUser) {
    localStorage.setItem(this.USER_KEY,JSON.stringify(securityUser));
  }

  public isAuthenticated(): boolean {
    return localStorage.getItem(this.USER_KEY) != null;
  }

  getUser(): SecurityUser {
    const stringUser: string | null = localStorage.getItem(this.USER_KEY)
    return stringUser ? JSON.parse(stringUser) : null
  }

  getToken():string|null{
    return  this.getUser()?.jwtBearer;
  }

  private removeUserFromLocaleStorage(){
    localStorage.removeItem(this.USER_KEY);
  }

  hasRole(role: Role) {
    return this.getUser()?.role === role;
  }

  hasAnyRole(roles: Array<Role>) {
    return roles.includes(this.getUser().role);
  }


}
