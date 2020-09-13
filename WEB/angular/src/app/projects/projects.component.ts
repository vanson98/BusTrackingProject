import { Component, OnInit, Injector } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase } from '@shared/paged-listing-component-base';
import { ProjectDto, ProjectServiceProxy, ProjectCategoryDto, ProjectCategoryServiceProxy, ProjectCategoryDtoPagedResultDto, ProjectListOutput, ProjectListOutputPagedResultDto } from '@shared/service-proxies/service-proxies';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { CreateProjectDialogComponent } from './create-project-dialog/create-project-dialog.component';
import { EditProjectDialogComponent } from './edit-project-dialog/edit-project-dialog.component';

class PagedProjectRequestDto extends PagedRequestDto {
  keyword: string;
  key: string;
  categoryID : number;
}

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  animations: [appModuleAnimation()],
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent extends PagedListingComponentBase<ProjectDto>  implements OnInit{

  projects: ProjectListOutput[] = [];
  categories : ProjectCategoryDto[] = [];
  keyword = '';
  key = '';
  categoryKey = '';

  constructor(
      injector: Injector,
      private _projectService: ProjectServiceProxy,
      private _projectCategoryService: ProjectCategoryServiceProxy,
      private _dialog: MatDialog
  ) {
      super(injector);
  }
  
  ngOnInit(){
      this.refresh();
      // Load category data
      this._projectCategoryService
      .getAll(this.categoryKey,this.keyword, 0, 100)
      .subscribe((result: ProjectCategoryDtoPagedResultDto) => {
          this.categories = result.items;
      });
  }

  createProjectCategory(): void {
    this.showCreateOrEditProjectDialog();
  }

  list(
      request: PagedProjectRequestDto,
      pageNumber: number,
      finishedCallback: Function
  ): void {
        // Load project data
        request.keyword = this.keyword;
        request.key = this.key;
        this._projectService
            .getListProject(request.keyword,request.key,request.categoryID, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: ProjectListOutputPagedResultDto) => {
                this.projects = result.items;
                this.showPaging(result, pageNumber);
            });
        
  }
  
  fillterProjectByCategory(categoriesID:number){
    var requestDto : PagedProjectRequestDto = new PagedProjectRequestDto();
    requestDto.keyword = this.keyword
    requestDto.categoryID = categoriesID;
    this.list(requestDto, 1, () => {
        this.isTableLoading = false;
    });
  }

  editProjectCategory(project: ProjectDto): void {
    this.showCreateOrEditProjectDialog(project.id);
  }

  delete(project: ProjectDto): void {
      abp.message.confirm(
          'Bạn có chắc muốn xóa dự án '+project.name+'?',
          undefined,
          (result: boolean) => {
              if (result) {
                  this._projectService
                      .delete(project.id)
                      .pipe(
                          finalize(() => {
                              abp.notify.success('Xóa thành công');
                              this.refresh();
                          })
                      )
                      .subscribe(() => { });
            }
          }
      );
  }

  
  
  
  showCreateOrEditProjectDialog(id?: number): void {
      let createOrEditRoleDialog;
      if (id === undefined || id <= 0) {
          createOrEditRoleDialog = this._dialog.open(CreateProjectDialogComponent );
      } else {
          createOrEditRoleDialog = this._dialog.open(EditProjectDialogComponent , {
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
