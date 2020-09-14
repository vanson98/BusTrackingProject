import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';

import { ModalModule } from 'ngx-bootstrap';

import { AbpModule } from '@abp/abp.module';

import { AccountRoutingModule } from './account-routing.module';

import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';

import { SharedModule } from '@shared/shared.module';

import { AccountComponent } from './account.component';
import { LoginComponent } from './login/login.component';
import { LoginService } from './login/login.service';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        HttpClientJsonpModule,
        AbpModule,
        SharedModule,
        ServiceProxyModule,
        AccountRoutingModule,
        ModalModule.forRoot()
    ],
    declarations: [
        AccountComponent,
        LoginComponent,
    ],
    providers: [
        LoginService
    ],
    entryComponents: [
        
    ]
})
export class AccountModule {

}
