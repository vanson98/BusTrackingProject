import { Component, ViewContainerRef, OnInit, ViewEncapsulation, Injector } from '@angular/core';
import { LoginService } from './login/login.service';
//import { AppComponentBase } from '@shared/app-component-base';
import * as $ from 'jquery'
@Component({
    templateUrl: './account.component.html',
    styleUrls: [
        './account.component.less'
    ],
    encapsulation: ViewEncapsulation.None
})
export class AccountComponent implements OnInit {


  

    private viewContainerRef: ViewContainerRef;

    public constructor(
        injector: Injector,
        private _loginService: LoginService
    ) {}

    ngOnInit(): void {
        $('body').addClass('login-page');
    }
}
