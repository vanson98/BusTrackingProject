import { Component, Injector, ViewEncapsulation, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MenuItem } from '@shared/layout/menu-item';
import { AppAuthService } from '@shared/auth/app-auth.service';

@Component({
    templateUrl: './topbar.component.html',
    selector: 'top-bar',
    styleUrls: ['./layout.component.css'],
    encapsulation: ViewEncapsulation.None
})
export class TopBarComponent extends AppComponentBase implements OnInit {
    shownLoginName = '';

    menuItems: MenuItem[]=[
        // new MenuItem(this.l("Dashboard"), '', 'dashboard', '/app/home'),
        new MenuItem(this.l("Dự án"), 'Pages.Projects', 'work','/app/projects'),
        new MenuItem(this.l("Loại Dự án"), 'Pages.Projects', 'work','/app/project-categories'),
        new MenuItem(this.l("Công việc"), 'Pages.Issues', 'vertical_split', '/app/'),
        new MenuItem(this.l("Nhật kí"), 'Pages.Timesheets', 'today', '/app/'),
        

        new MenuItem(this.l("Hệ thống"), '', 'build',"" ,[
            new MenuItem(this.l("Người dùng"), 'Pages.Users', 'people', '/app/users'),
            new MenuItem(this.l("Vai trò"), 'Pages.Roles', 'donut_small', '/app/roles'),
            new MenuItem(this.l("Phòng ban"), 'Pages.Tenants', 'business', '/app/tenants')
            // new MenuItem(this.l("About"), '', 'info', '/app/about'),
        ]),
        
    ]    
    
    constructor(
        injector: Injector,
        private _authService: AppAuthService
    ){
        super(injector);
    }

    ngOnInit() {
        this.shownLoginName = this.appSession.getShownLoginName();
    }

    showMenuItem(menuItem): boolean {
        if (menuItem.permissionName) {
            return this.permission.isGranted(menuItem.permissionName);
        }

        return true;
    }

    logout(): void {
        this._authService.logout();
    }
}
