import { AuthServiceProxy, LoginRequestDto, StringResultDto } from './../../shared/service-proxies/service-proxies';
import { MessageService } from '@abp/message/message.service';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';
import { finalize } from 'rxjs/operators';


@Injectable()
export class LoginService {
    authenticateModel: LoginRequestDto;
    authenticateResult: StringResultDto;

    constructor(
        private _authService: AuthServiceProxy,
        private _router: Router,
        private _messageService: MessageService,
    ) {
        this.clear();
    }

    authenticate(finallyCallback?: () => void): void {
        finallyCallback = finallyCallback || (() => { });
        this._authService
            .authenticate(this.authenticateModel)
            .pipe(finalize(() => { finallyCallback(); }))
            .subscribe((result) => {
                this.processAuthenticateResult(result);
            });
    }

    private processAuthenticateResult(authenticateResult) {
        this.authenticateResult = authenticateResult;
        if (authenticateResult.result) {
            this.login(authenticateResult.result);
        } else {
            this._messageService.error("Tên đăng nhập hoặc mật khẩu không đúng!");
            this._router.navigate(['account/login']);
        }
    }

    private login(accessToken: string): void {
        localStorage.setItem('accessToken',accessToken);
        location.href = AppConsts.appBaseUrl
    }

    private clear(): void {
        this.authenticateModel = new LoginRequestDto();
        this.authenticateResult = null;
    }
}
