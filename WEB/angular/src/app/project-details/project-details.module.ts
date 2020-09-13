import { FormsModule } from '@angular/forms';
import { CreateEpicDialogComponent } from './create-epic-dialog/create-epic-dialog.component';
import { CreateSprintDialogComponent } from './create-sprint-dialog/create-sprint-dialog.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';
import { ProjectDetailComponent } from './project-detail.component';
import { ProjectDetailRoutingModule } from './project-detail-routing.module';
import { BacklogsComponent } from './backlogs/backlogs.component';
import { IssuesComponent } from './issues/issues.component';
import { SprintsComponent } from './sprints/sprints.component';
import { SettingsComponent } from './settings/settings.component';



@NgModule({
  declarations: [
    ProjectDetailComponent,
    BacklogsComponent,
    IssuesComponent,
    SprintsComponent,
    SettingsComponent,
    CreateSprintDialogComponent,
    CreateEpicDialogComponent
  ],
  imports: [
    CommonModule,
    ServiceProxyModule,
    ProjectDetailRoutingModule,
    SharedModule,
    FormsModule
  ],
  providers: [

  ],
  entryComponents: [
    CreateSprintDialogComponent,
    CreateEpicDialogComponent
  ]
})
export class ProjectDetailsModule { }
