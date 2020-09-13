import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { Routes, RouterModule } from '@angular/router';
import { ProjectDetailComponent } from './project-detail.component';
import { BacklogsComponent } from './backlogs/backlogs.component';
import { SprintsComponent } from './sprints/sprints.component';
import { IssuesComponent } from './issues/issues.component';
import { SettingsComponent } from './settings/settings.component';


const routes : Routes = [
  {
      path: '',
      component: ProjectDetailComponent,
      children: [
        { path:'project-backlog/:id',component:BacklogsComponent, canActivate:[AppRouteGuard] },
        { path: 'project-sprint',component: SprintsComponent,canActivate: [AppRouteGuard]},
        { path:'project-issues',component:IssuesComponent, canActivate:[AppRouteGuard] },
        { path: 'project-setting',component: SettingsComponent,canActivate: [AppRouteGuard]},
      ]
  }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports:[
    RouterModule
  ]
})
export class ProjectDetailRoutingModule { }
