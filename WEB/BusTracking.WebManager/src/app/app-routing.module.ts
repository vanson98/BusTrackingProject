import { DriverComponent } from './driver/driver.component';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { BusComponent } from './bus/bus.component';
import { RouteComponent } from './route/route.component';
import { StopComponent } from './stop/stop.component';
import { StudentComponent } from './student/student.component';
import { UserComponent } from './user/user.component';
import { CheckinLogComponent } from './checkin-log/checkin-log.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'driver',component: DriverComponent, canActivate: [AppRouteGuard]},
                    { path: 'home', component: HomeComponent,  canActivate: [AppRouteGuard] },
                    { path: 'bus', component: BusComponent,  canActivate: [AppRouteGuard] },
                    { path: 'route', component: RouteComponent,  canActivate: [AppRouteGuard] },
                    { path: 'stop', component: StopComponent,  canActivate: [AppRouteGuard] },
                    { path: 'student', component: StudentComponent,  canActivate: [AppRouteGuard] },
                    { path: 'user', component: UserComponent,  canActivate: [AppRouteGuard] },
                    { path: 'log', component: CheckinLogComponent,  canActivate: [AppRouteGuard] },
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
