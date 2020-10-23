import { PagedListingComponentBase, PagedRequestDto } from './../../shared/paged-listing-component-base';
import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { UserDto, UserDtoPageResultDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { CreateUserDialogComponent } from './create-user-dialog/create-user-dialog.component';
import { EditUserDialogComponent } from './edit-user-dialog/edit-user-dialog.component';
import { AppResCode } from '@shared/const/AppResCode';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  animations: [appModuleAnimation()],
  styleUrls: ['./user.component.css']
})
export class UserComponent extends PagedListingComponentBase<UserDto> {
  users: UserDto[] = [];
  // Search Field
  fullName: string = '';
  userName: string = '';
  phoneNumber: string = '';
  typeAccount: number | null;

  constructor(
    injector: Injector,
    private _userService: UserServiceProxy,
    private _dialog: MatDialog
  ) {
    super(injector);
  }

  ngOnInit() {
    this.refresh();
  }

  editStudent(user: UserDto) {
    this.showCreateOrEditStudentDialog(user)
  }

  createStudent(){
    this.showCreateOrEditStudentDialog();
  }
  
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    var fullName = this.fullName.trim();
    var userName = this.userName.trim();
    var phone = this.phoneNumber.trim();
    var typeAcc = this.typeAccount;
    this._userService
          .getAllPaging(fullName,userName,phone,typeAcc,pageNumber,request.maxResultCount)
          .pipe(
            finalize(() => {
                finishedCallback();
            })
          )
          .subscribe((result: UserDtoPageResultDto) => {
              this.users = result.items;
              this.showPaging(result.totalRecord, pageNumber);
          });
  }
  
  protected delete(user: UserDto): void {
    abp.message.confirm(
      "Bạn có chắc muốn xóa người dùng"+user.fullName,
      "Cảnh báo",
      (result: boolean) => {
        if (result) {
          this._userService
            .delete(user.id)
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

  private showCreateOrEditStudentDialog(userDto?: UserDto){
    let createOrEditStudent; 
    if (userDto === undefined || userDto == null) {
      createOrEditStudent = this._dialog.open(CreateUserDialogComponent);
    } else {
      createOrEditStudent = this._dialog.open(EditUserDialogComponent,{
        data:{
          userId: userDto.id
        }
      });
    }

    createOrEditStudent.afterClosed().subscribe((result) => {
      if (result) {
        this.refresh();
      }
    });
  }
}
