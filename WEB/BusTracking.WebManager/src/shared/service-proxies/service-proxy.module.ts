import { AppSessionService } from './../session/app-session.service';
import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AbpHttpInterceptor } from '@abp/abpHttpInterceptor';

import * as ApiServiceProxies from './service-proxies';

@NgModule({
    providers: [
        ApiServiceProxies.UserServiceProxy,
        ApiServiceProxies.BusServiceProxy,
        ApiServiceProxies.DriverServiceProxy,
        ApiServiceProxies.RouteServiceProxy,
        ApiServiceProxies.StopServiceProxy,
        ApiServiceProxies.StudentServiceProxy,
        ApiServiceProxies.AuthServiceProxy,
        { provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true }
    ]
})
export class ServiceProxyModule { }
