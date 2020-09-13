import { Component, OnInit, Injector } from '@angular/core';
import { ProjectDto, ProjectServiceProxy, UserServiceProxy, ProjectDtoPagedResultDto, UserDto, UserDtoPagedResultDto, ProjectCategoryServiceProxy, ProjectCategoryDtoPagedResultDto, ProjectCategoryDto } from '@shared/service-proxies/service-proxies';
import { MatDialogRef } from '@angular/material';
import { AppComponentBase } from '@shared/app-component-base';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-create-project-dialog',
  templateUrl: './create-project-dialog.component.html',
  styleUrls: ['./create-project-dialog.component.css']
})
export class CreateProjectDialogComponent extends AppComponentBase implements OnInit {

  project : ProjectDto = new ProjectDto();
  users : UserDto[] = [];
  categories : ProjectCategoryDto[] = [];
  saving = false;

  constructor(
    injector: Injector, 
    private _projectService: ProjectServiceProxy, 
    private _userService: UserServiceProxy,
    private _categoryService: ProjectCategoryServiceProxy,
    private _dialogRef : MatDialogRef<CreateProjectDialogComponent>) 
  {
    super(injector);
  }

  ngOnInit() {
    this._userService.getAll("",true,0,10000).subscribe((result: UserDtoPagedResultDto)=>{
      this.users = result.items;
    })
    this._categoryService.getAll("","",0,10000).subscribe((result: ProjectCategoryDtoPagedResultDto)=>{
      this.categories = result.items;
    })
  }

  save(): void {
    this.saving = true;

    const project_ = new ProjectDto();
    project_.init(this.project);

    this._projectService
      .create(project_)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.close(true);
      });
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }
}
