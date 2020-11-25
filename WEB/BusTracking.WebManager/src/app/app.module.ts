import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap';
import { NgxPaginationModule } from 'ngx-pagination';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AbpModule } from '@abp/abp.module';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';
import { HomeComponent } from '@app/home/home.component';
import { TopBarComponent } from '@app/layout/topbar.component';
import { SideBarUserAreaComponent } from '@app/layout/sidebar-user-area.component';
import { SideBarNavComponent } from '@app/layout/sidebar-nav.component';
import { SideBarFooterComponent } from '@app/layout/sidebar-footer.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DriverComponent } from './driver/driver.component';
import { CreateDriverDialogComponent } from './driver/create-driver-dialog/create-driver-dialog.component';
import { EditDriverDialogComponent } from './driver/edit-driver-dialog/edit-driver-dialog.component';
import { MY_FORMATS } from '@shared/const/datepicker-format';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material';
import { BusComponent } from './bus/bus.component';
import { CreateBusDialogComponent } from './bus/create-bus-dialog/create-bus-dialog.component';
import { EditBusDialogComponent } from './bus/edit-bus-dialog/edit-bus-dialog.component';
import { RouteComponent } from './route/route.component';
import { CreateRouteDialogComponent } from './route/create-route-dialog/create-route-dialog.component';
import { EditRouteDialogComponent } from './route/edit-route-dialog/edit-route-dialog.component';
import { StopComponent } from './stop/stop.component';
import { CreateStopDialogComponent } from './stop/create-stop-dialog/create-stop-dialog.component';
import { EditStopDialogComponent } from './stop/edit-stop-dialog/edit-stop-dialog.component';
import { OwlDateTimeModule } from 'ng-pick-datetime/date-time/date-time.module';
import { OwlNativeDateTimeModule } from 'ng-pick-datetime/date-time/adapter/native-date-time.module';
import { AgmCoreModule } from '@agm/core';
import { StudentComponent } from './student/student.component';
import { CreateStudentDialogComponent } from './student/create-student-dialog/create-student-dialog.component';
import { EditStudentDialogComponent } from './student/edit-student-dialog/edit-student-dialog.component';
import { UserComponent } from './user/user.component';
import { CreateUserDialogComponent } from './user/create-user-dialog/create-user-dialog.component';
import { EditUserDialogComponent } from './user/edit-user-dialog/edit-user-dialog.component';
import { CheckinLogComponent } from './checkin-log/checkin-log.component';
import { OWL_DATE_TIME_FORMATS, OWL_DATE_TIME_LOCALE } from 'ng-pick-datetime';
import { MY_NATIVE_FORMATS } from '@shared/validator/datepicker-format';
import { MapCheckinDialogComponent } from './checkin-log/map-checkin-dialog/map-checkin-dialog.component';
import { ChartsModule } from 'ng2-charts';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    TopBarComponent,
    SideBarUserAreaComponent,
    SideBarNavComponent,
    SideBarFooterComponent,
    DriverComponent,
    CreateDriverDialogComponent,
    EditDriverDialogComponent,
    BusComponent,
    CreateBusDialogComponent,
    EditBusDialogComponent,
    RouteComponent,
    CreateRouteDialogComponent,
    EditRouteDialogComponent,
    StopComponent,
    CreateStopDialogComponent,
    EditStopDialogComponent,
    StudentComponent,
    CreateStudentDialogComponent,
    EditStudentDialogComponent,
    UserComponent,
    CreateUserDialogComponent,
    EditUserDialogComponent,
    CheckinLogComponent,
    MapCheckinDialogComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    ModalModule.forRoot(),
    AbpModule,
    AppRoutingModule,
    ServiceProxyModule,
    SharedModule,
    ChartsModule,
    //Google Map Api
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyAlXj3gfNdc17yhMJQ06BoE14Lz8NsbWl0',
      libraries: ['geometry', 'places']
    }),
    // Time Owl
    OwlDateTimeModule, 
    OwlNativeDateTimeModule,
    
  ],
  providers: [
    {provide: MAT_DATE_LOCALE, useValue: 'vi-vn'},
    {provide: OWL_DATE_TIME_LOCALE, useValue: 'vi'},
    {provide: OWL_DATE_TIME_FORMATS, useValue: MY_NATIVE_FORMATS},
  ],
  entryComponents: [
    CreateDriverDialogComponent,
    EditDriverDialogComponent,
    CreateBusDialogComponent,
    EditBusDialogComponent,
    CreateRouteDialogComponent,
    EditRouteDialogComponent,
    CreateStopDialogComponent,
    EditStopDialogComponent,
    CreateStudentDialogComponent,
    EditStudentDialogComponent,
    CreateUserDialogComponent,
    EditUserDialogComponent,
    MapCheckinDialogComponent
  ]
})
export class AppModule {}
