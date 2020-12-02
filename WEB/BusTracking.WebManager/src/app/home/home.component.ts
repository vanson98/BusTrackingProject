import { Component, Injector, AfterViewInit, OnInit, OnDestroy } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BusDto, BusServiceProxy, StudentCheckInDto, StudentCheckInDtoResultDto, StudentServiceProxy, TotalStudentStatus } from '@shared/service-proxies/service-proxies';
import { SignalRService } from '@shared/signalr-service/signal-r.service';
import { BaseChartDirective, ThemeService } from 'ng2-charts';
import * as moment from 'moment';
import * as _ from 'lodash';

@Component({
    templateUrl: './home.component.html',
    animations: [appModuleAnimation()],
    styleUrls: ['./home.component.css'],
    providers: [BaseChartDirective ,ThemeService]
})
export class HomeComponent extends AppComponentBase implements OnInit, OnDestroy{
    totalStudentStatus : TotalStudentStatus = new TotalStudentStatus();
    buses : BusDto[] = [];
    // Search Data Chart
    busId: number;
    checkIntype: string = "";
    timeRequest: Date = new Date();

    // Dữ liệu biểu đồ tròn
    doughnutChartLabel:string[]=["Muộn","Sớm","Đúng giờ"];
    doughnutChartData = [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0];
    // Dữ liệu biểu đồ cột
    barChartOptions= {
        scaleShowVerticalLines: false,
        reponsive: true
      }
    barChartLegend = true;
    barChartLabel = [];
    barChartData = [{data: [],label:""}];

    constructor(
        injector: Injector,
        public _studentService: StudentServiceProxy,
        public _signalRService: SignalRService,
        public _busService: BusServiceProxy,
    ) {
        super(injector);
    }
   

    ngOnInit(){
        this._busService.getAllPaging(undefined,undefined,undefined,undefined,1,9999)
                        .subscribe(res=>{
                            this.buses = res.items;
                            this.busId = this.buses[0].id;
                            this.getDataChart();
                        });
        this._studentService.getTotalStudentStatus().subscribe(res=>{
            this.totalStudentStatus = res.result;
        })
        
        this.trackingStudentCheckIn();
        
    }

    ngOnDestroy(): void {
        this._signalRService.closeConnection();
    }

    trackingStudentCheckIn(){
        this._signalRService.startConnection();
        this._signalRService.hubConnection.on('ReceiveCheckIn',(data: StudentCheckInDtoResultDto)=>{
          if(data.result!=null){
            var studentCheckIn : StudentCheckInDto = new StudentCheckInDto();
            studentCheckIn.init(data.result);
            if(studentCheckIn.checkInResult==3){
                this.totalStudentStatus.atSchool++;
                this.totalStudentStatus.onBus--;
            }else if(studentCheckIn.checkInResult==1 || studentCheckIn.checkInResult==4){
                this.totalStudentStatus.absent++;
            }else if(studentCheckIn.checkInResult == 7){
                this.totalStudentStatus.atHome++;
            }else if(studentCheckIn.checkInResult==2 ){
                this.totalStudentStatus.onBus++;
                this.totalStudentStatus.atHome--;
            }else if(studentCheckIn.checkInResult==5){
                this.totalStudentStatus.onBus++;
                this.totalStudentStatus.atSchool--;
            }else if(studentCheckIn.checkInResult==6){
                this.totalStudentStatus.onBus--;
            }else if(studentCheckIn.checkInResult == 8){
                this.totalStudentStatus.onLeave++;
            }else if(studentCheckIn.checkInResult ==10){
                this.totalStudentStatus.atSchool--;
            }
          }
        })
    }

    getDataChart(){
        //this.timeRequest.setDate(this.timeRequest.getDate()+1);
        var time = moment(this.timeRequest);
        var typeCheckIn = this.checkIntype=='' ? undefined : parseInt(this.checkIntype);
        this.doughnutChartData = [];
        this._studentService.getDataChar(typeCheckIn,time,this.busId).subscribe(res=>{
            var totalCheckInState = res.result.totalCheckInState;
            var checkInsOfMonth = res.result.checkInChartModel;
            this.doughnutChartData.push(res.result.totalCheckInState.late);
            this.doughnutChartData.push(res.result.totalCheckInState.soon);
            this.doughnutChartData.push(res.result.totalCheckInState.onTime);
            
            this.barChartLabel = this.getDaysArray(time.year(),time.month());
            this.barChartData = [];
            if(checkInsOfMonth.length>0){
                var a = {data:[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],label: 'Muộn'}
                var b = {data:[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],label: 'Sớm'}
                var c = {data:[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],label: 'Đúng giờ'}
                _.forEach(checkInsOfMonth,(item)=>{
                    var day = moment(item.checkInDay).date();
                    if(item.checkInState==0){
                        a.data[day]++
                    }
                    if(item.checkInState==2){
                        b.data[day]++
                    }
                    if(item.checkInState==1){
                        c.data[day]++
                    }
                })
                this.barChartData.push(a);
                this.barChartData.push(b);
                this.barChartData.push(c);
            }
        })
    }


    getDaysArray(year, month) {
        var monthIndex = month ; 
        var date = new Date(year, monthIndex, 1);
        var result = [];
        while (date.getMonth() == monthIndex) {
          result.push(date.getDate());
          date.setDate(date.getDate() + 1);
        }
        return result;
    }
    
}
