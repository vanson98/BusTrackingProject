import { PagedListingComponentBase, PagedRequestDto } from './../../shared/paged-listing-component-base';
import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { StopDto, StopDtoPageResultDto, StopServiceProxy } from '@shared/service-proxies/service-proxies';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { CreateStopDialogComponent } from './create-stop-dialog/create-stop-dialog.component';
import { EditStopDialogComponent } from './edit-stop-dialog/edit-stop-dialog.component';
import { AppResCode } from '@shared/const/AppResCode';

@Component({
  selector: 'app-stop',
  templateUrl: './stop.component.html',
  animations: [appModuleAnimation()],
  styleUrls: ['./stop.component.css']
})
export class StopComponent extends PagedListingComponentBase<StopDto> {
  stops: StopDto[] = [];
  // Search Field
  name: string = '';
  address: string = '';
  status: number | null;

  constructor(
    injector: Injector,
    private _stopService: StopServiceProxy,
    private _dialog: MatDialog
  ) {
    super(injector);
  }

  ngOnInit() {
    this.refresh();
  }

  editStop(Stop: StopDto) {
    this.showCreateOrEditStopDialog(Stop)
  }

  createStop(){
    this.showCreateOrEditStopDialog();
  }
  
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this._stopService
          .getAllPaging(this.name.trim(),this.address.trim(),this.status,pageNumber,request.maxResultCount)
          .pipe(
            finalize(() => {
                finishedCallback();
            })
          )
          .subscribe((result: StopDtoPageResultDto) => {
              this.stops = result.items;
              this.showPaging(result.totalRecord, pageNumber);
          });
  }
  
  protected delete(Stop: StopDto): void {
    abp.message.confirm(
      "Bạn có chắc muốn xóa "+Stop.name,
      "Cảnh báo",
      (result: boolean) => {
        if (result) {
          this._stopService
            .delete(Stop.id)
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

  private showCreateOrEditStopDialog(stopDto?: StopDto){
    let createOrEditStop; 
    if (stopDto === undefined || stopDto == null) {
      createOrEditStop = this._dialog.open(CreateStopDialogComponent);
    } else {
      createOrEditStop = this._dialog.open(EditStopDialogComponent, {
        data: {
          stopEdit: stopDto
        },
      });
    }

    createOrEditStop.afterClosed().subscribe((result) => {
      if (result) {
        this.refresh();
      }
    });
  }
}
