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
        localStorage.clear();
        this._session.clear();
        this._router.navigate(['/account/login']);
    }

    getToken(){
        return localStorage.getItem('accessToken');
    }

    checkHaveToken(): boolean {
        var token = this.getToken();
        if(!token) return true;
    }
}
