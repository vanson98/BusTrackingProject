import { PagedListingComponentBase, PagedRequestDto } from './../../shared/paged-listing-component-base';
import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { DriverDto, DriverDtoPageResultDto, DriverServiceProxy } from '@shared/service-proxies/service-proxies';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { CreateDriverDialogComponent } from './create-driver-dialog/create-driver-dialog.component';
import { EditDriverDialogComponent } from './edit-driver-dialog/edit-driver-dialog.component';
import { AppResCode } from '@shared/const/AppResCode';

@Component({
  selector: 'app-driver',
  templateUrl: './driver.component.html',
  animations: [appModuleAnimation()],
  styleUrls: ['./driver.component.css']
})

export class DriverComponent extends PagedListingComponentBase<DriverDto> {
  drivers: DriverDto[] = [];
  // Search Field
  name: string = '';
  phoneNumber: string = '';
  status: number | null;

  constructor(
    injector: Injector,
    private _driverService: DriverServiceProxy,
    private _dialog: MatDialog
  ) {
    super(injector);
  }

  ngOnInit() {
    this.refresh();
  }

  editDriver(driver: DriverDto) {
    this.showCreateOrEditDriverDialog(driver)
  }

  createDriver(){
    this.showCreateOrEditDriverDialog();
  }
  
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this._driverService
          .getAllPaging(this.name.trim(),this.phoneNumber.trim(),this.status,pageNumber,request.maxResultCount)
          .pipe(
            finalize(() => {
                finishedCallback();
            })
          )
          .subscribe((result: DriverDtoPageResultDto) => {
              this.drivers = result.items;
              this.showPaging(result.totalRecord, pageNumber);
          });
  }
  
  protected delete(driver: DriverDto): void {
    abp.message.confirm(
      "Bạn có chắc muốn xóa "+driver.name,
      "Cảnh báo",
      (result: boolean) => {
        if (result) {
          this._driverService
            .delete(driver.id)
            .subscribe((res) => {
              if(res.statusCode==AppResCode.Success){
                abp.notify.success("Xóa thành công");
                this.refresh();
              }else{
                abp.notify.error(res.message);
              }
            });
        }
      }
    );
  }

  private showCreateOrEditDriverDialog(driverDto?: DriverDto){
    let createOrEditDriver; 
    if (driverDto === undefined || driverDto == null) {
      createOrEditDriver = this._dialog.open(CreateDriverDialogComponent);
    } else {
      createOrEditDriver = this._dialog.open(EditDriverDialogComponent, {
        data: {
          driverEdit: driverDto
        },
      });
    }

    createOrEditDriver.afterClosed().subscribe((result) => {
      if (result) {
        this.refresh();
      }
    });
  }
}
