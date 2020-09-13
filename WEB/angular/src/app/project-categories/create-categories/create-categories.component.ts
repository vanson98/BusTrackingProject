import { Component, Injector, OnInit} from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MatDialogRef } from '@angular/material';
import { ProjectCategoryServiceProxy, ProjectCategoryDto } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-create-categories',
  templateUrl: './create-categories.component.html',
  styleUrls: ['./create-categories.component.css']
  
})
export class CreateCategoriesComponent  extends AppComponentBase implements OnInit  {
  
  category: ProjectCategoryDto = new ProjectCategoryDto();
  saving = false;

  constructor(
    injector: Injector,
    private _categoryService: ProjectCategoryServiceProxy,
    private _dialogRef: MatDialogRef<CreateCategoriesComponent>
  ) {
    super(injector);
  }

  ngOnInit() {
  }

  save(): void {
    this.saving = true;

    const category_ = new ProjectCategoryDto();
    category_.init(this.category);

    this._categoryService
      .create(category_)
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
