import { PagedListingComponentBase, PagedRequestDto } from './../../shared/paged-listing-component-base';
import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { StudentDto, StudentDtoPageResultDto, StudentServiceProxy } from '@shared/service-proxies/service-proxies';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { CreateStudentDialogComponent } from './create-student-dialog/create-student-dialog.component';
import { EditStudentDialogComponent } from './edit-student-dialog/edit-student-dialog.component';
import { AppResCode } from '@shared/const/AppResCode';

@Component({
  selector: 'app-Student',
  templateUrl: './student.component.html',
  animations: [appModuleAnimation()],
  styleUrls: ['./student.component.css']
})

export class StudentComponent extends PagedListingComponentBase<StudentDto> {
  students: StudentDto[] = [];
  // Search Field
  name: string = '';
  parentName: string = '';
  busName: string = '';
  stopName: string = '';

  constructor(
    injector: Injector,
    private _studentService: StudentServiceProxy,
    private _dialog: MatDialog
  ) {
    super(injector);
  }

  ngOnInit() {
    this.refresh();
  }

  editStudent(Student: StudentDto) {
    this.showCreateOrEditStudentDialog(Student)
  }

  createStudent(){
    this.showCreateOrEditStudentDialog();
  }
  
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this._studentService
          .getAllPaging(this.busName.trim(),this.parentName.trim(),this.stopName.trim(),this.name.trim(),pageNumber,request.maxResultCount)
          .pipe(
            finalize(() => {
                finishedCallback();
            })
          )
          .subscribe((result: StudentDtoPageResultDto) => {
              this.students = result.items;
              this.showPaging(result.totalRecord, pageNumber);
          });
  }
  
  protected delete(Student: StudentDto): void {
    abp.message.confirm(
      "Bạn có chắc muốn xóa "+Student.name,
      "Cảnh báo",
      (result: boolean) => {
        if (result) {
          this._studentService
            .delete(Student.id)
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

  private showCreateOrEditStudentDialog(StudentDto?: StudentDto){
    let createOrEditStudent; 
    if (StudentDto === undefined || StudentDto == null) {
      createOrEditStudent = this._dialog.open(CreateStudentDialogComponent);
    } else {
      createOrEditStudent = this._dialog.open(EditStudentDialogComponent, {
        data: {
          studentEdit: StudentDto
        },
      });
    }

    createOrEditStudent.afterClosed().subscribe((result) => {
      if (result) {
        this.refresh();
      }
    });
  }
}
