import { Component, Inject, Injector, OnInit, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AppComponentBase } from '@shared/app-component-base';
import { DriverServiceProxy, UpdateDriverRequestDto } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import * as moment from 'moment';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-driver-dialog',
  templateUrl: './edit-driver-dialog.component.html',
  styleUrls: ['./edit-driver-dialog.component.css']
})

export class EditDriverDialogComponent  extends AppComponentBase
implements OnInit {

  saving = false;
  isActive : boolean;
  driver: UpdateDriverRequestDto = new UpdateDriverRequestDto();

  editForm: FormGroup;
  name : FormControl;
  dob : FormControl;
  address: FormControl;
  email: FormControl;
  phone: FormControl;


  constructor(
    injector: Injector,
    private _diverService: DriverServiceProxy,
    private fb: FormBuilder,
    private _dialogRef: MatDialogRef<EditDriverDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _data: object
    ) {
    super(injector);
  }

  ngOnInit() {
    this.driver.init(this._data["driverEdit"]);
    this.isActive = this.driver.status == 1 ? true : false;
    this.initForm();
  }

  initForm(){
    this.name = new FormControl();
    this.dob = new FormControl(this.driver.dob.toISOString(),Validators.required);
    this.address = new FormControl();
    this.email = new FormControl();
    this.phone = new FormControl();
    this.editForm = this.fb.group({
      'name': this.name,
      'dob': this.dob,
      'address': this.address,
      'email': this.email,
      'phone': this.phone,
    })
  }

  save(): void {
    this.saving = true;
    this.driver.status = this.isActive == true ? 1 : 0;
    this.driver.dob = moment(this.dob.value).add(1, 'days');
    this._diverService
      .update(this.driver)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe((result) => {
        if(result != null && result.statusCode=="S200"){
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
