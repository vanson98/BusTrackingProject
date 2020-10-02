import { FormGroup, FormControl } from '@angular/forms';
import { CreateRouteRequestDto, RouteServiceProxy } from '@shared/service-proxies/service-proxies';
import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MatDialogRef } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppResCode } from '@shared/const/AppResCode';

@Component({
  selector: 'app-create-route-dialog',
  templateUrl: './create-route-dialog.component.html',
  styleUrls: ['./create-route-dialog.component.css']
})
export class CreateRouteDialogComponent extends AppComponentBase
implements OnInit {
  // Variable
  saving = false;
  isActive = true;
  route: CreateRouteRequestDto = new CreateRouteRequestDto();
  // Form Control
  createForm: FormGroup;
  name: FormControl;
  routeCode: FormControl;
  distance: FormControl;

  constructor(
    injector: Injector,
    public _routeService: RouteServiceProxy,
    private _dialogRef: MatDialogRef<CreateRouteDialogComponent>
    ) {
    super(injector);
  }

  ngOnInit() {
    this.initForm();
  }

  initForm(){
    this.name = new FormControl('');
    this.routeCode = new FormControl('');
    this.distance = new FormControl('');
    this.createForm = new FormGroup({
      'name': this.name,
      'routeCode' : this.routeCode,
      'distance': this.distance
    })
  }

  save(): void {
    this.saving = true;
    this.route.status = this.isActive == true ? 1 : 0;
    this._routeService
      .create(this.route)
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