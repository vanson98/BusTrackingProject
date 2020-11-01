import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CreateStudentRequestDto, StudentServiceProxy, UserDto, StopDto, BusDto, StopServiceProxy, BusServiceProxy, UserServiceProxy, UpdateStudentRequestDto } from './../../../shared/service-proxies/service-proxies';
import { Component, Inject, Injector, OnInit, Optional } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppResCode } from '@shared/const/AppResCode';
import * as moment from 'moment';

@Component({
  selector: 'app-edit-student-dialog',
  templateUrl: './edit-student-dialog.component.html',
  styleUrls: ['./edit-student-dialog.component.css']
})
export class EditStudentDialogComponent  extends AppComponentBase
implements OnInit {
  saving = false;
  isActive = true;
  parents : UserDto[] = [];
  stops : StopDto[] = [];
  buses : BusDto[] = [];
  student: UpdateStudentRequestDto = new UpdateStudentRequestDto();
  
  // Form
  editForm: FormGroup;
  name : FormControl;
  bus : FormControl;
  parent: FormControl;
  class: FormControl;
  teacher: FormControl;
  phoneTeacher: FormControl;
  stop: FormControl;
  dob: FormControl;
  address: FormControl;
  email : FormControl;
  phone: FormControl;


  constructor(
    injector: Injector,
    public _studentService: StudentServiceProxy,
    public _stopService: StopServiceProxy,
    public _busService: BusServiceProxy,
    public _userService: UserServiceProxy,
    private _dialogRef: MatDialogRef<EditStudentDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _data: object
    ) {
    super(injector);
  }

  ngOnInit() {
    this.student.init(this._data['studentEdit']);
    this.initSelect();
    this.initForm();
  }

  initForm(){
    this.name = new FormControl();
    this.dob = new FormControl(this.student.dob.toISOString(),Validators.required);
    this.address = new FormControl();
    this.class = new FormControl();
    this.teacher = new FormControl();
    this.phoneTeacher = new FormControl();
    this.email = new FormControl();
    this.phone = new FormControl();
    this.bus = new FormControl();
    this.parent = new FormControl();
    this.stop = new FormControl();
    this.editForm = new FormGroup({
      'name': this.name,
      'dob': this.dob,
      'address': this.address,
      'class': this.class,
      'teacher': this.teacher,
      'phoneTeacher': this.phoneTeacher,
      'email': this.email,
      'phone': this.phone,
      'bus': this.bus,
      'parent': this.parent,
      'stop': this.stop
    })
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
    this.student.dob = moment(this.dob.value).add(1, 'days');
    this._studentService
      .update(this.student)
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
