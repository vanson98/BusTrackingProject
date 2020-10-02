import { Injector, ElementRef } from '@angular/core';
import { AppConsts } from '@shared/AppConsts';
import { LocalizationService } from '@abp/localization/localization.service';
import { PermissionCheckerService } from '@abp/auth/permission-checker.service';
import { FeatureCheckerService } from '@abp/features/feature-checker.service';
import { NotifyService } from '@abp/notify/notify.service';
import { SettingService } from '@abp/settings/setting.service';
import { MessageService } from '@abp/message/message.service';
import { AbpMultiTenancyService } from '@abp/multi-tenancy/abp-multi-tenancy.service';
import { AppSessionService } from '@shared/session/app-session.service';

export abstract class AppComponentBase {
    notify: NotifyService;
    message: MessageService;
    appSession: AppSessionService;

    constructor(injector: Injector) {
        this.notify = injector.get(NotifyService);
        this.message = injector.get(MessageService);
        this.appSession = injector.get(AppSessionService);
    }

    // isGranted(permissionName: string): boolean {
    //     return this.permission.isGranted(permissionName);
    // }
}
