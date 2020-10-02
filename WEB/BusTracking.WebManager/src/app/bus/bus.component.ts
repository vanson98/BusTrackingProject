import { Component, Injector, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppResCode } from '@shared/const/AppResCode';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BusDto, BusDtoPageResultDto, BusServiceProxy } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { CreateBusDialogComponent } from './create-bus-dialog/create-bus-dialog.component';
import { EditBusDialogComponent } from './edit-bus-dialog/edit-bus-dialog.component';

@Component({
  selector: 'app-bus',
  templateUrl: './bus.component.html',
  animations: [appModuleAnimation()],
  styleUrls: ['./bus.component.css']
})
export class BusComponent extends PagedListingComponentBase<BusDto>{
  buses : BusDto[] = [];
  licenseCode : string = '';
  status : number ;
  driverName : string = '';
  routeName : string = '';

  constructor(
    injector: Injector,
    private _driverService: BusServiceProxy,
    private _dialog: MatDialog
  ) {
    super(injector);
  }

  ngOnInit() {
    this.refresh();
  }

  editDriver(bus: BusDto) {
    this.showCreateOrEditDriverDialog(bus)
  }

  createDriver(){
    this.showCreateOrEditDriverDialog();
  }
  
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this._driverService
          .getAllPaging(this.licenseCode.trim(),this.status,this.driverName.trim(),this.routeName.trim(),pageNumber,request.maxResultCount)
          .pipe(
            finalize(() => {
                finishedCallback();
            })
          )
          .subscribe((result: BusDtoPageResultDto) => {
              this.buses = result.items;
              this.showPaging(result.totalRecord, pageNumber);
          });
  }
  
  protected delete(bus: BusDto): void {
    abp.message.confirm(
      "Bạn có chắc muốn xóa "+bus.name,
      "Cảnh báo",
      (result: boolean) => {
        if (result) {
          this._driverService
            .delete(bus.id)
            .subscribe((res) => {
              if(res.statusCode==AppResCode.Success){
                abp.notify.success("Xóa menu thành công");
                this.refresh();
              }else{
                abp.notify.error(res.message);
              }
            });
        }
      }
    );
  }

  private showCreateOrEditDriverDialog(busDto?: BusDto){
    let createOrEditDriver; 
    if (busDto === undefined || busDto == null) {
      createOrEditDriver = this._dialog.open(CreateBusDialogComponent);
    } else {
      createOrEditDriver = this._dialog.open(EditBusDialogComponent, {
        data: {
          busEdit: busDto
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
