import { Component, OnInit, Injector, Optional, Inject } from '@angular/core';
import { ProjectCategoryServiceProxy, ProjectCategoryDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { finalize } from 'rxjs/operators';


@Component({
  selector: 'app-edit-categories',
  templateUrl: './edit-categories.component.html',
  styleUrls: ['./edit-categories.component.css']
})
export class EditCategoriesComponent extends AppComponentBase
implements OnInit  {

  saving = false;
  projectCate: ProjectCategoryDto = new ProjectCategoryDto();

  constructor(
    injector: Injector,
    private _projectCateService: ProjectCategoryServiceProxy,
    private _dialogRef: MatDialogRef<EditCategoriesComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: number) 
  {
    super(injector);
  }

  // Lấy dữ liệu ProjectCategory hiện thời đưa vào dialog
  ngOnInit(): void {
    this._projectCateService.get(this._id)
      .subscribe((result: ProjectCategoryDto) => {
        this.projectCate.init(result);
      });
  }

  save() :void {
    this.saving = true;
    // var inputCategory: UpdateProjectCategoryInput =  ;
    // inputCategory.init(this.)
    this._projectCateService
    .update(this.projectCate)
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
