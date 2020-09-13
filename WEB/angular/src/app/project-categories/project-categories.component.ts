import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
    PagedListingComponentBase,
    PagedRequestDto
} from '@shared/paged-listing-component-base';
import {
    ProjectCategoryServiceProxy,
    ProjectCategoryDto,
    ProjectCategoryDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { CreateCategoriesComponent} from './create-categories/create-categories.component';
import { EditCategoriesComponent } from './edit-categories/edit-categories.component';


class PagedProjectCategoryRequestDto extends PagedRequestDto {
  keyword: string;
  keyCategory: string;
}

@Component({
  selector: 'app-project-categories',
  templateUrl: './project-categories.component.html',
  animations: [appModuleAnimation()],
  styleUrls: ['./project-categories.component.css']
})
export class ProjectCategoriesComponent extends PagedListingComponentBase<ProjectCategoryDto> {
  categories: ProjectCategoryDto[] = [];

  keyword = '';
  keyCategory = '';

  constructor(
      injector: Injector,
      private _projectCategoryService: ProjectCategoryServiceProxy,
      private _dialog: MatDialog
  ) {
      super(injector);
  }
 

  list(
      request: PagedProjectCategoryRequestDto,
      pageNumber: number,
      finishedCallback: Function
  ): void {

      request.keyword = this.keyword;
      request.keyCategory = this.keyCategory;
      this._projectCategoryService
          .getAll(request.keyCategory, request.keyword, request.skipCount, request.maxResultCount)
          .pipe(
              finalize(() => {
                  finishedCallback();
              })
          )
          .subscribe((result: ProjectCategoryDtoPagedResultDto) => {
              this.categories = result.items;
              this.showPaging(result, pageNumber);
          });
    }

  delete(category: ProjectCategoryDto): void {
      abp.message.confirm(
          this.l('Bạn có chắc muốn xóa', category.name),
          undefined,
          (result: boolean) => {
              if (result) {
                  this._projectCategoryService
                      .delete(category.id)
                      .pipe(
                          finalize(() => {
                              abp.notify.success(this.l('Xóa thành công'));
                              this.refresh();
                          })
                      )
                      .subscribe(() => { });
              }
          }
      );
  }

  
  createProjectCategory(): void {
    this.showCreateOrEditRoleDialog();
  }
  
  editProjectCategory(role: ProjectCategoryDto): void {
    this.showCreateOrEditRoleDialog(role.id);
  }
  
  showCreateOrEditRoleDialog(id?: number): void {
      let createOrEditRoleDialog;
      if (id === undefined || id <= 0) {
          createOrEditRoleDialog = this._dialog.open(CreateCategoriesComponent);
      } else {
          createOrEditRoleDialog = this._dialog.open(EditCategoriesComponent, {
              data: id
          });
      }

      createOrEditRoleDialog.afterClosed().subscribe(result => {
          if (result) {
              this.refresh();
          }
      });
  }
}
