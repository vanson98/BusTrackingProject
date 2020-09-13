import { Component, OnInit, Injector, Inject, Optional } from '@angular/core';
import { ProjectServiceProxy, UserServiceProxy, ProjectCategoryServiceProxy, ProjectDto, UserDto, ProjectCategoryDto, UserDtoPagedResultDto, ProjectCategoryDtoPagedResultDto } from '@shared/service-proxies/service-proxies';
import { CreateProjectDialogComponent } from '../create-project-dialog/create-project-dialog.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { AppComponentBase } from '@shared/app-component-base';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-edit-project-dialog',
  templateUrl: './edit-project-dialog.component.html',
  styleUrls: ['./edit-project-dialog.component.css']
})
export class EditProjectDialogComponent extends AppComponentBase implements OnInit  {

  project : ProjectDto = new ProjectDto();
  users : UserDto[] = [];
  categories : ProjectCategoryDto[] = [];
  saving = false;
  
  constructor(
    injector: Injector, 
    private _projectService: ProjectServiceProxy, 
    private _userService: UserServiceProxy,
    private _categoryService: ProjectCategoryServiceProxy,
    private _dialogRef : MatDialogRef<EditProjectDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: number) 
  {
    super(injector);
  }
  
  ngOnInit() {
    this._projectService.get(this._id).subscribe((project: ProjectDto)=>{
      this.project.init(project);
    });
    this._userService.getAll("",true,0,10000).subscribe((result: UserDtoPagedResultDto)=>{
      this.users = result.items;
    });
    this._categoryService.getAll("","",0,10000).subscribe((result: ProjectCategoryDtoPagedResultDto)=>{
      this.categories = result.items;
    })
  }

  save() :void {
    this.saving = true;
    // var inputCategory: UpdateProjectCategoryInput =  ;
    // inputCategory.init(this.)
    this._projectService
    .update(this.project)
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
  
  close(result: boolean) {
    this._dialogRef.close(result);
  }

}
