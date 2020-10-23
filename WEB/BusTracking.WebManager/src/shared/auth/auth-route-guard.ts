
import { Injectable, Inject } from '@angular/core';
import {CanActivate, Router, ActivatedRouteSnapshot} from '@angular/router';
import { AppAuthService } from './app-auth.service';
import { AppSessionService } from '@shared/session/app-session.service';
import { AdminBSB } from 'jquery';
import { AppConsts } from '@shared/AppConsts';
import * as _ from 'lodash';

@Injectable()
export class AppRouteGuard implements CanActivate {
    private _authService: AppAuthService;
    private _router: Router;
    private session: AppSessionService;

    constructor(
        router: Router,
        authService: AppAuthService,
        @Inject(AppSessionService) session: AppSessionService
    ) { 
        this.session = session
        this._router = router
        this._authService = authService
    }

    canActivate(route: ActivatedRouteSnapshot): boolean {
        if(this._authService.checkHaveToken()){
            this._router.navigate(['/account/login']);
            return false;
        }else {
            return true;
        }
    }


    // isGranted(permissionId?: number): boolean {
    //     if(permissionId==null || permissionId==0)
    //         return true;
    //     var permissionArray = this.session.getPermissions();
    //     return _.includes(permissionArray, permissionId);
    // }
}
