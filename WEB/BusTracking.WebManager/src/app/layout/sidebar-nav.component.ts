import { Component, Injector, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MenuItem } from '@shared/layout/menu-item';

@Component({
    templateUrl: './sidebar-nav.component.html',
    selector: 'sidebar-nav',
    encapsulation: ViewEncapsulation.None
})
export class SideBarNavComponent extends AppComponentBase {

    menuItems: MenuItem[] = [
        new MenuItem('HomePage', '', 'home', '/app/home'),
        new MenuItem('Quản lý tài xế','','perm_contact_calendar','/app/driver'),
        new MenuItem('Quản lý xe đưa đón','','directions_bus','/app/bus'),
        new MenuItem('Quản lý tuyến','','alt_route','/app/route'),
        new MenuItem('Quản lý điểm dừng','','location_on','/app/stop'),
        new MenuItem('Quản lý học sinh','','face','/app/student'),
        new MenuItem('Quản lý người dùng','','supervisor_account','/app/user')
    ];

    constructor(
        injector: Injector
    ) {
        super(injector);
    }

    showMenuItem(menuItem): boolean {
        // if (menuItem.permissionName) {
        //     return this.permission.isGranted(menuItem.permissionName);
        // }

        return true;
    }
}
