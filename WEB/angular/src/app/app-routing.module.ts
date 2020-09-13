import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { UsersComponent } from './users/users.component';
import { TenantsComponent } from './tenants/tenants.component';
import { RolesComponent } from 'app/roles/roles.component';
import { ProjectCategoriesComponent } from 'app/project-categories/project-categories.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { ProjectsComponent } from './projects/projects.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'home', component: ProjectsComponent,  canActivate: [AppRouteGuard] },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, canActivate: [AppRouteGuard] },
                    { path: 'project-categories', component: ProjectCategoriesComponent, data: { permission: 'Pages.Projects' }, canActivate: [AppRouteGuard] },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, canActivate: [AppRouteGuard] },
                    { path: 'about', component: AboutComponent },
                    { path: 'update-password', component: ChangePasswordComponent },
                    { path: 'projects', component: ProjectsComponent, data: { permission: 'Pages.Projects' }, canActivate: [AppRouteGuard] },
                    { path: 'project-details/:id/:projectName/:projectKey', loadChildren: './project-details/project-details.module#ProjectDetailsModule', data: { permission: 'Pages.CreateProjects' } },
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
