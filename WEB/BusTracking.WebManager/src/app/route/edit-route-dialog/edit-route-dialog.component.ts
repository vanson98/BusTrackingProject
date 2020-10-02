import { FormGroup, FormControl } from '@angular/forms';
import {  RouteServiceProxy, UpdateRouteRequestDto } from '@shared/service-proxies/service-proxies';
import { Component, Inject, Injector, OnInit, Optional } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppResCode } from '@shared/const/AppResCode';

@Component({
  selector: 'app-edit-route-dialog',
  templateUrl: './edit-route-dialog.component.html',
  styleUrls: ['./edit-route-dialog.component.css']
})
export class EditRouteDialogComponent  extends AppComponentBase
implements OnInit {
  // Variable
  saving = false;
  isActive : boolean;
  route: UpdateRouteRequestDto = new UpdateRouteRequestDto();
  // Form Control
  editForm: FormGroup;
  name: FormControl;
  routeCode: FormControl;
  distance: FormControl;

  constructor(
    injector: Injector,
    public _routeService: RouteServiceProxy,
    private _dialogRef: MatDialogRef<EditRouteDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _data: object
    ) {
    super(injector);
  }

  ngOnInit() {
    this.route.init(this._data['routeEdit']);
    this.isActive = this.route.status == 1 ? true : false;
    this.initForm();
  }

  initForm(){
    this.name = new FormControl('');
    this.routeCode = new FormControl('');
    this.distance = new FormControl('');
    this.editForm = new FormGroup({
      'name': this.name,
      'routeCode' : this.routeCode,
      'distance': this.distance
    })
  }

  save(): void {
    this.saving = true;
    this.route.status = this.isActive == true ? 1 : 0;
    this._routeService
      .update(this.route)
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