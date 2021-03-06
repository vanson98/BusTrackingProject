import { Component, Injector, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BusDto, BusServiceProxy, StopDto, StopServiceProxy, StudentCheckInDto, StudentCheckInDtoResultDto, StudentServiceProxy } from '@shared/service-proxies/service-proxies';
import { SignalRService } from '@shared/signalr-service/signal-r.service';
import { MessageService } from 'abp-ng2-module/dist/src/message/message.service';
import * as moment from 'moment';
import { finalize } from 'rxjs/operators';
import { MapCheckinDialogComponent } from './map-checkin-dialog/map-checkin-dialog.component';

@Component({
  selector: 'app-checkin-log',
  templateUrl: './checkin-log.component.html',
  animations: [appModuleAnimation()],
  styleUrls: ['./checkin-log.component.css']
})
export class CheckinLogComponent extends PagedListingComponentBase<StudentCheckInDto> {
  
  logs: StudentCheckInDto[] = [];
  buses: BusDto[] = [];

  // Search Field
  fromDate : Date;
  toDate: Date;
  studentName: string = '';
  studentStatus: string = '';
  busId: string = '';
  stopId: string = '';
  checkInType: string = '';

  constructor(
    injector: Injector,
    private _studentService: StudentServiceProxy,
    private _busService: BusServiceProxy,
    private _dialog: MatDialog,
    private _signalRService: SignalRService,
    private _messageService: MessageService
  ) {
    super(injector);
  }

  ngOnInit() {
    this.toDate = new Date();
    var tenDayAgo = new Date();
    tenDayAgo.setDate(tenDayAgo.getDate()-10);
    this.fromDate = tenDayAgo;

    this.refresh();
    // SignalR
    this.trackingStudentCheckIn();
    // Get all bus
    this._busService.getAllPaging(undefined,undefined,undefined,undefined,1,10000).subscribe(res=>{
      this.buses = res.items;
    })
  }

  trackingStudentCheckIn(){
    this._signalRService.startConnection();
    this._signalRService.hubConnection.on('ReceiveCheckIn',(data: StudentCheckInDtoResultDto)=>{
      if(data.result!=null){
        var studentCheckIn : StudentCheckInDto = new StudentCheckInDto();
        studentCheckIn.init(data.result);
        this.logs.unshift(studentCheckIn);
      }
    })
  }
  
  openMap(log: StudentCheckInDto){
    if(log.longitude==0 && log.latitude == 0){
      this._messageService.info("Thông báo","Bản ghi điểm danh không có vị trí");
    }else{
      this._dialog.open(MapCheckinDialogComponent,{
        data: {
          long: log.longitude,
          lat: log.latitude
        }
      })
    }

  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    var studentStatus = this.studentStatus == "" ? undefined : parseInt(this.studentStatus);
    var busId = this.busId == "" ? undefined : parseInt(this.busId);
    var stopId = this.stopId == "" ? undefined : parseInt(this.stopId);
    var checkInType = this.checkInType == "" ? undefined : parseInt(this.checkInType);
    this._studentService
          .getLogsCheckIn(
            moment(this.fromDate),
            moment(this.toDate),
            this.studentName.trim(),
            studentStatus,
            busId,
            checkInType,
            pageNumber,
            this.pageSize)
          .pipe(
            finalize(() => {
                finishedCallback();
            })
          )
          .subscribe((res) => {
              this.logs = res.items;
              this.showPaging(res.totalRecord, pageNumber);
          });
  }

  protected delete(entity: StudentCheckInDto): void {
    throw new Error('Method not implemented.');
  }
}
