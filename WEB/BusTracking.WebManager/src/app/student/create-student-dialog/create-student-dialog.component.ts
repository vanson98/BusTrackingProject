import { CreateStudentRequestDto, StudentServiceProxy, UserDto, StopDto, BusDto, StopServiceProxy, BusServiceProxy, UserServiceProxy } from './../../../shared/service-proxies/service-proxies';
import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MatDialogRef } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppResCode } from '@shared/const/AppResCode';

@Component({
  selector: 'app-create-Student-dialog',
  templateUrl: './create-Student-dialog.component.html',
  styleUrls: ['./create-Student-dialog.component.css']
})
export class CreateStudentDialogComponent extends AppComponentBase
implements OnInit {
  saving = false;
  isActive = true;
  parents : UserDto[] = [];
  stops : StopDto[] = [];
  buses : BusDto[] = [];
  student: CreateStudentRequestDto = new CreateStudentRequestDto();
  
  constructor(
    injector: Injector,
    public _studentService: StudentServiceProxy,
    public _stopService: StopServiceProxy,
    public _busService: BusServiceProxy,
    public _userService: UserServiceProxy,
    private _dialogRef: MatDialogRef<CreateStudentDialogComponent>
    ) {
    super(injector);
  }

  ngOnInit() {
    this.initSelect();
  }

  initSelect(){
    this._busService.getAllPaging(undefined,undefined,undefined,undefined,1,1000).subscribe(result=>{
      if(result.statusCode==AppResCode.Success){
        this.buses = result.items;
      }else{
        abp.message.error(result.message);
      }
    })
    this._userService.getAllByType(0).subscribe(res=>{
      if(res.statusCode==AppResCode.Success){
        this.parents = res.result;
      }else{
        abp.message.error(res.message);
      }
    })
    this._stopService.getAllPaging(undefined,undefined,undefined,undefined,1,1000).subscribe(result=>{
      if(result.statusCode==AppResCode.Success){
        this.stops = result.items;
      }else{
        abp.message.error(result.message);
      }
    })
  }

  save(): void {
    this.saving = true;
    this._studentService
      .create(this.student)
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
