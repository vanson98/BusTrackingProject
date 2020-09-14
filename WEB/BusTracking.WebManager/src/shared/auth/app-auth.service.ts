import { AppConsts } from '@shared/AppConsts';
import { Injectable } from '@angular/core';
import * as moment from 'moment';
//import { AuthService } from '@shared/service-proxies/authen-service';
import { AppSessionService } from '@shared/session/app-session.service';
import { Router } from '@angular/router';

@Injectable()
export class AppAuthService {
    constructor(
        //private _accountService: AuthService,
        private _session: AppSessionService,
        private _router: Router
    ) { }

    logout(): void {
        // this._accountService.logout().subscribe((result)=>{
        //     if(result){
                
        //     }else{
        //         abp.message.error('Không thể đăng xuất','Lỗi');
        //     }
        // })
        localStorage.clear();
        this._session.clear();
        this._router.navigate(['/account/login']);
    }

    getToken(){
        return localStorage.getItem('accessToken');
    }

    // Xem token đã hết hạn chưa
    isTokenExpired(token?: string): boolean {
        if(!token) token = this.getToken();
        // Nếu token rỗng thì trả về true
        if(!token) return true;
      }

    // getExpiration() {
    //     const expiration = this._cookieService.get('expire');
    //     const expiresAt = JSON.parse(expiration);
    //     return moment(expiresAt);
    // }    
}
