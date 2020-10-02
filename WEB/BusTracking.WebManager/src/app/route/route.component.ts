import { PagedListingComponentBase, PagedRequestDto } from './../../shared/paged-listing-component-base';
import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { RouteDto, RouteDtoPageResultDto, RouteServiceProxy } from '@shared/service-proxies/service-proxies';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { EditRouteDialogComponent } from './edit-route-dialog/edit-route-dialog.component';
import { CreateRouteDialogComponent } from './create-route-dialog/create-route-dialog.component';
import { AppResCode } from '@shared/const/AppResCode';

@Component({
  selector: 'app-route',
  templateUrl: './route.component.html',
  animations: [appModuleAnimation()],
  styleUrls: ['./route.component.css']
})
export class RouteComponent extends PagedListingComponentBase<RouteDto> {
  routes: RouteDto[] = [];
  // Search Field
  name: string = '';
  routeCode: string = '';
  status: number | null;

  constructor(
    injector: Injector,
    private _routeService: RouteServiceProxy,
    private _dialog: MatDialog
  ) {
    super(injector);
  }

  ngOnInit() {
    this.refresh();
  }

  editRoute(route: RouteDto) {
    this.showCreateOrEditRouteDialog(route)
  }

  createRoute(){
    this.showCreateOrEditRouteDialog();
  }
  
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this._routeService
          .getAllPaging(this.routeCode.trim(),this.name,this.status,pageNumber,request.maxResultCount)
          .pipe(
            finalize(() => {
                finishedCallback();
            })
          )
          .subscribe((result: RouteDtoPageResultDto) => {
              this.routes = result.items;
              this.showPaging(result.totalRecord, pageNumber);
          });
  }
  
  protected delete(route: RouteDto): void {
    abp.message.confirm(
      "Bạn có chắc muốn xóa "+route.name,
      "Cảnh báo",
      (result: boolean) => {
        if (result) {
          this._routeService
            .delete(route.id)
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

  private showCreateOrEditRouteDialog(routeDto?: RouteDto){
    let createOrEditDriver; 
    if (routeDto === undefined || routeDto == null) {
      createOrEditDriver = this._dialog.open(CreateRouteDialogComponent);
    } else {
      createOrEditDriver = this._dialog.open(EditRouteDialogComponent, {
        data: {
          routeEdit: routeDto
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
