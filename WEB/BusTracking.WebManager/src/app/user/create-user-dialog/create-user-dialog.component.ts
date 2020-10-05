import { Component, Injector, OnInit, Optional } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatCheckboxChange, MatDialogRef } from '@angular/material';
import { AppComponentBase } from '@shared/app-component-base';
import { AppResCode } from '@shared/const/AppResCode';
import { AuthServiceProxy, CreateUserRequestDto, RoleAssignRequest, RoleDto, RoleDtoListResultDto, SelectedItem, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppValidator } from '@shared/validator/AppValidator';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-create-user-dialog',
  templateUrl: './create-user-dialog.component.html',
  styleUrls: ['./create-user-dialog.component.css']
})
export class CreateUserDialogComponent extends AppComponentBase implements OnInit {
  saving = false;
  isActive: boolean;
  user: CreateUserRequestDto = new CreateUserRequestDto();
  roles: RoleDto[] = null;

  checkedRolesMap: { [key: string]: boolean } = {};
  defaultRoleCheckedStatus = false;

  formCreateUser: FormGroup;
  name: FormControl;
  dob: FormControl;
  email: FormControl;
  phone: FormControl;
  userName: FormControl;
  typeUser: FormControl;
  password: FormControl;
  confirmPass: FormControl;

  constructor(
    injector: Injector,
    private _userService: UserServiceProxy,
    private _authService: AuthServiceProxy,
    private _dialogRef: MatDialogRef<CreateUserDialogComponent>
  ) {
    super(injector);
  }

  initForm() {
    this.userName = new FormControl('', [Validators.pattern(/^[\w]+$/)]),
    this.name = new FormControl('', [AppValidator.cannotContainSpecialCharactor]),
    this.dob = new FormControl();
    this.password = new FormControl('', Validators.pattern('(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$')),
    this.email = new FormControl(''),
    this.phone = new FormControl(''),
    this.confirmPass = new FormControl(),
    this.typeUser = new FormControl()
    this.formCreateUser = new FormGroup({
      'userName': this.userName,
      'name': this.name,
      'dob': this.dob,
      'password': this.password,
      'email': this.email,
      'phone': this.phone,
      'confirmPass': this.confirmPass,
      'typeUser': this.typeUser
    })
  }


  ngOnInit(): void {
    // Get list role
    this._authService.getAllRole().subscribe(res => {
      this.roles = res.result;
      this.setInitialRolesStatus();
    });
    this.initForm();
  }

  setInitialRolesStatus(): void {
    _.map(this.roles, item => {
      this.checkedRolesMap[item.normalizedName] = this.defaultRoleCheckedStatus;
    });
  }

  onRoleChange(role: RoleDto, $event: MatCheckboxChange) {
    this.checkedRolesMap[role.normalizedName] = $event.checked;
  }

  getCheckedRoles(): string[] | null {
    const roleAssign: string[] = [];
    _.forEach(this.checkedRolesMap, function (value, key) {
      if (value) {
        roleAssign.push(key);
      }
    });
    if (roleAssign.length == 0) {
      return null;
    }
    return roleAssign;
  }

  save(): void {
    var rolesAssign : string[] = this.getCheckedRoles();
    if (rolesAssign != null) {
      this.saving = true;
      this.user.rolesName = rolesAssign;
      this._userService
        .createUser(this.user)
        .pipe(
          finalize(() => {
            this.saving = false;
          })
        )
        .subscribe((result) => {
          if (result.statusCode === AppResCode.Success) {
            this.close(true);
            this.notify.error("Tạo người dùng thành công!");
          } else {
            this.close(false);
            this.notify.error("Tạo người dùng thất bại!");
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
