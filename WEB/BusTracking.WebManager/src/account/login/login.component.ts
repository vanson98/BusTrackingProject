import { Component, Injector } from '@angular/core';
import { AbpSessionService } from '@abp/session/abp-session.service';
import { AppComponentBase } from '@shared/app-component-base';
import { accountModuleAnimation } from '@shared/animations/routerTransition';
import { LoginService } from './login.service';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less'],
  animations: [accountModuleAnimation()]
})
export class LoginComponent extends AppComponentBase {
  submitting = false;
  constructor(
    injector: Injector,
    public loginService: LoginService,
  ) {
    super(injector);
  }

  login(): void {
    this.submitting = true;
    this.loginService.authenticate(() => (this.submitting = false));
  }
}
