import { AbpMultiTenancyService } from '@abp/multi-tenancy/abp-multi-tenancy.service';
import { Injectable } from '@angular/core';
import { UserServiceProxy } from '@shared/service-proxies/service-proxies';

@Injectable()
export class AppSessionService {
    user : string;
    constructor(
        private _userService: UserServiceProxy) {
    }
   
    init(): Promise<boolean> {
        return new Promise<boolean>((resolve, reject) => {
            // this._sessionService.getCurrentLoginInformations().toPromise().then((result: GetCurrentLoginInformationsOutput) => {
            //     this._application = result.application;
            //     this._user = result.user;
            //     this._tenant = result.tenant;

            //     resolve(true);
            // }, (err) => {
            //     reject(err);
            // });
            resolve(true);
        });
    }

      
    clear(){
        // this.agencyID = null;
        // this.userID = null;
        // this.userName = null;
        // this.emailAddress = null;
        // this.agencyCode = null;
        // this.isFirstLogin = null;
        // this.permissions = null;
    }

}
