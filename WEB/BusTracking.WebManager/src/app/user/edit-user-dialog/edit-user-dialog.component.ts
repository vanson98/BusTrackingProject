import { Component, Inject, Injector, OnInit, Optional } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatCheckboxChange, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AppComponentBase } from '@shared/app-component-base';
import { AppResCode } from '@shared/const/AppResCode';
import { AuthServiceProxy, CreateUserRequestDto, RoleDto, UpdateUserRequestDto, UserDto, UserDtoResultDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppValidator } from '@shared/validator/AppValidator';
import * as _ from 'lodash';
import * as moment from 'moment';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-edit-user-dialog',
  templateUrl: './edit-user-dialog.component.html',
  styleUrls: ['./edit-user-dialog.component.css']
})
export class EditUserDialogComponent extends AppComponentBase implements OnInit {
  saving = false;
  isActive: boolean;
  userDto: UserDto = new UserDto();
  user: UpdateUserRequestDto = new UpdateUserRequestDto();
  roles: RoleDto[] = null;
  
  checkedRolesMap: { [key: string]: boolean } = {};
  defaultRoleCheckedStatus = false;

  formEditUser: FormGroup;
  name: FormControl;
  dob: FormControl;
  email: FormControl;
  phone: FormControl;
  userName: FormControl;
  typeUser: FormControl;

  constructor(
    injector: Injector,
    private _userService: UserServiceProxy,
    private _authService: AuthServiceProxy,
    private _dialogRef: MatDialogRef<EditUserDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _data: object
  ) {
    super(injector);
  }

  ngOnInit(): void {
    // Get user
    this._userService.getById(this._data['userId']).subscribe(res=>{
      this.userDto = res.result;
      this.user.init(this.userDto);
      this.dob.setValue(this.userDto.dob.toISOString());
      this.isActive = this.user.status ==1 ? true : false;
    });
    // Get list role
    this._authService.getAllRole().subscribe(res => {
      this.roles = res.result;
      this.setInitialRolesStatus();
    });
    this.initForm();
  }

  setInitialRolesStatus(): void {
    _.map(this.roles, item => {
      this.checkedRolesMap[item.normalizedName] = this.isRoleChecked(item.name);
    });
  }

  isRoleChecked(name: string): boolean {
    return _.includes(this.userDto.roles, name);
  }

  initForm() {
    this.userName = new FormControl('', [Validators.pattern(/^[\w]+$/)]),
    this.name = new FormControl('', [AppValidator.cannotContainSpecialCharactor]),
    this.dob = new FormControl(Validators.required);
    this.email = new FormControl(''),
    this.phone = new FormControl(''),
    this.typeUser = new FormControl()
    this.formEditUser = new FormGroup({
      'userName': this.userName,
      'name': this.name,
      'dob': this.dob,
      'email': this.email,
      'phone': this.phone,
      'typeUser': this.typeUser
    })
  }

  onRoleChange(role: RoleDto, $event: MatCheckboxChange) {
    this.checkedRolesMap[role.normalizedName] = $event.checked;
  }

  getCheckedRoles(): any {
    var addedRole: string[] =[];
    var removedRole: string[] = [];
    _.forEach(this.checkedRolesMap, function (value, key) {
      if (value) {
        addedRole.push(key);
      }else{
        removedRole.push(key);
      }
    });
    if (addedRole.length==0) {
      return null;
    }
    return {
      addedRole : addedRole,
      removedRole: removedRole
    };
  }

  save(): void {
    this.saving = true;
    var rolesAssign : Object= this.getCheckedRoles();
    if (rolesAssign != null) {
      this.user.addedRoles = rolesAssign['addedRole'];
      this.user.removedRoles = rolesAssign['removedRole'];
      this.user.dob = moment(this.dob.value).add(1, 'days');
      this.user.status = this.isActive ? 1 : 0;
      this._userService
        .update(this.user)
        .pipe(
          finalize(() => {
            this.saving = false;
          })
        )
        .subscribe((result) => {
          if (result.statusCode === AppResCode.Success) {
            this.close(true);
            this.notify.success("Cập nhật người dùng thành công!");
          } else {
            //this.close(false);
            abp.message.error(result.message,'Lỗi');
            this.notify.error("Cập nhật người dùng thất bại!");
          }
          abp.ui.clearBusy();
        });
    } else {
      this.notify.warn("Bạn chưa phân quyền cho người dùng");
      abp.ui.clearBusy();
    }
  }
  close(result: any): void {
    this._dialogRef.close(result);
  }
}
