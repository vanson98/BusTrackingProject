import * as moment from 'moment';
import { AppConsts } from '@shared/AppConsts';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { Type, CompilerOptions, NgModuleRef } from '@angular/core';
import { environment } from './environments/environment';

export class AppPreBootstrap {

    static run(appRootUrl: string, callback: () => void): void {
        AppPreBootstrap.getApplicationConfig(appRootUrl,callback);
    }

    private static getApplicationConfig(appRootUrl: string, callback: () => void) {
        return abp.ajax({
            url: appRootUrl + 'assets/' + environment.appConfig,
            method: 'GET',
        }).done(result => {
            AppConsts.appBaseUrl = result.appBaseUrl;
            AppConsts.remoteServiceBaseUrl = result.remoteServiceBaseUrl;
            callback();
        });
    }
}
