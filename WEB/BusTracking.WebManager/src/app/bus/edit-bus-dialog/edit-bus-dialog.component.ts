import { UserServiceProxy, RouteServiceProxy, DriverServiceProxy, UpdateBusRequestDto, BusDto, ResponseDto } from './../../../shared/service-proxies/service-proxies';
import { Component, Inject, Injector, OnInit, Optional } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { BusServiceProxy, DriverDto, RouteDto, UserDto } from '@shared/service-proxies/service-proxies';
import { AppResCode } from './../../../shared/const/AppResCode';

@Component({
  selector: 'app-edit-bus-dialog',
  templateUrl: './edit-bus-dialog.component.html',
  styleUrls: ['./edit-bus-dialog.component.css']
})
export class EditBusDialogComponent extends AppComponentBase
implements OnInit {

  saving = false;
  bus : BusDto = new BusDto();
  busUpdate: UpdateBusRequestDto = new UpdateBusRequestDto();

  isActive = true;
  drivers : DriverDto[] = [];
  monitors : UserDto[] = [];
  routes: RouteDto[] = [];

  driverId : number | null;
  monitorId: string;
  routeId: number | null;

  constructor(
    injector: Injector,
    public _busService: BusServiceProxy,
    public _diverService: DriverServiceProxy,
    public _userService: UserServiceProxy,
    public _routeService: RouteServiceProxy,
    private _dialogRef: MatDialogRef<EditBusDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _data: object
    ) {
    super(injector);
  }

  ngOnInit() {
    this.bus.init(this._data['busEdit']);
    this.driverId = this.bus.driverId == -1 ? null : this.bus.driverId;
    this.monitorId = this.bus.monitorId == '00000000-0000-0000-0000-000000000000' ? null : this.bus.monitorId;
    this.routeId = this.bus.routeId == -1 ? null : this.bus.routeId;
    // Lấy danh sách tài xế, GSX, tuyến chưa gán cho xe nào
    this.initCombobox();
  }

  initCombobox(){
    this._diverService.getAllDriverUnAssign().subscribe(res=>{
      if(res != null && res.statusCode==AppResCode.Success){
        this.drivers = res.result
      }else{
        abp.message.error("Đã có lỗi xảy ra","Lỗi")
      }
    })
    this._userService.getAllMonitorUnAssign().subscribe(res=>{
      if(res != null && res.statusCode==AppResCode.Success){
        this.monitors = res.result
      }else{
        abp.message.error("Đã có lỗi xảy ra","Lỗi");
      }
    })
    this._routeService.getAllRouteUnAssign().subscribe(res=>{
      if(res != null && res.statusCode==AppResCode.Success){
        this.routes = res.result
      }else{
        abp.message.error("Đã có lỗi xảy ra","Lỗi");
      }
    })
  }

  save(): void {
    this.saving = true;
    this.bus.status = this.isActive == true ? 1 : 0;
    this.bus.driverId = this.bus.driverId  == -1 ? null : this.bus.driverId;
    this.bus.monitorId = this.bus.monitorId == '00000000-0000-0000-0000-000000000000' ? null : this.bus.monitorId;
    this.bus.routeId = this.bus.routeId  == -1 ? null : this.bus.routeId;
    this.busUpdate.init(this.bus);
    this._busService
      .update(this.busUpdate)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe((result: ResponseDto) => {
        if(result != null && result.statusCode==AppResCode.Success){
          this.notify.success("Cập nhật thành công");
          this.close(true);
        }else{
          this.notify.success("Cập nhật thất bại");
        }
      });
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }

}
