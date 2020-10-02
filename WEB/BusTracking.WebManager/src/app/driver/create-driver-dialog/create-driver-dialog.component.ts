import { CreateDriverRequestDto, DriverServiceProxy} from './../../../shared/service-proxies/service-proxies';
import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MatDialogRef } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppResCode } from '@shared/const/AppResCode';

@Component({
  selector: 'app-create-driver-dialog',
  templateUrl: './create-driver-dialog.component.html',
  styleUrls: ['./create-driver-dialog.component.css']
})
export class CreateDriverDialogComponent extends AppComponentBase
implements OnInit {
  saving = false;
  isActive = true;
  driver: CreateDriverRequestDto = new CreateDriverRequestDto();
  
  constructor(
    injector: Injector,
    public _diverService: DriverServiceProxy,
    private _dialogRef: MatDialogRef<CreateDriverDialogComponent>
    ) {
    super(injector);
  }

  ngOnInit() {
  }

  save(): void {
    this.saving = true;
    this.driver.status = this.isActive == true ? 1 : 0;
    this._diverService
      .create(this.driver)
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
