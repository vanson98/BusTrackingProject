import { UserServiceProxy, RouteServiceProxy, DriverServiceProxy } from './../../../shared/service-proxies/service-proxies';
import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MatDialogRef } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { BusServiceProxy, CreateBusRequestDto, DriverDto, RouteDto, UserDto } from '@shared/service-proxies/service-proxies';
import { AppResCode } from './../../../shared/const/AppResCode';

@Component({
  selector: 'app-create-bus-dialog',
  templateUrl: './create-bus-dialog.component.html',
  styleUrls: ['./create-bus-dialog.component.css']
})
export class CreateBusDialogComponent extends AppComponentBase
implements OnInit {

  saving = false;
  bus: CreateBusRequestDto = new CreateBusRequestDto();

  isActive = true;
  drivers : DriverDto[] = [];
  monitors : UserDto[] = [];
  routes: RouteDto[] = [];

  constructor(
    injector: Injector,
    public _busService: BusServiceProxy,
    public _diverService: DriverServiceProxy,
    public _userService: UserServiceProxy,
    public _routeService: RouteServiceProxy,
    private _dialogRef: MatDialogRef<CreateBusDialogComponent>
    ) {
    super(injector);
  }

  ngOnInit() {
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
    this._busService
      .create(this.bus)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe((result) => {
        if(result != null && result.statusCode==AppResCode.Success){
          this.notify.success("Tạo mới thành công");
          this.close(true);
        }else{
          this.notify.success("Tạo mới thất bại");
        }
      });
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }

}
