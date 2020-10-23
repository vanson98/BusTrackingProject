import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { LoginService } from './../../account/login/login.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AppAuthService } from './app-auth.service';

@Injectable({
  providedIn: "root"
})
export class TokenInterceptorService implements HttpInterceptor{

  constructor(private injector: Injector) { }
  // Mọi request trước khi gửi về server sẽ đều phải đi qua hàm này để gán token
  intercept(req : HttpRequest<any>,next:HttpHandler) : Observable<HttpEvent<any>>{
    let _authService = this.injector.get(AppAuthService);
    var token = _authService.getToken();
    if(token===null || token==''){
      return next.handle(req);
    }
    // Ghi đè request cũ
    let tokenizedReq = req.clone({
      setHeaders: {
        Authorization: `${"Bearer "+token}`
      }
    })
    // Trả về request mới sau khi được ghi đè
    return next.handle(tokenizedReq);
  }
}
