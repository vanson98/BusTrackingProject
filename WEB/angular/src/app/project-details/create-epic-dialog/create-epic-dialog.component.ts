import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { CreateEpicDto, EpicServiceProxy, EpicDto } from './../../../shared/service-proxies/service-proxies';
import { Component, OnInit, Injector, Optional, Inject } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { finalize } from 'rxjs/operators';
import * as moment from 'moment';

@Component({
  selector: 'app-create-epic-dialog',
  templateUrl: './create-epic-dialog.component.html',
  styleUrls: ['./create-epic-dialog.component.css']
})
export class CreateEpicDialogComponent extends AppComponentBase implements OnInit {
  epic : EpicDto = new EpicDto();
  newEpic : CreateEpicDto = new CreateEpicDto();
  dateEdit : Date ;
  saving=false;
  editMode = false;
  projectId: number ;

  constructor(
    injector: Injector,
    private _epicService: EpicServiceProxy,
    private _dialogRef: MatDialogRef<CreateEpicDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _data: Object
  ) { 
    super(injector)
  }

  ngOnInit() {
    var data = this._data;
    if(data["typeDialog"]!=1){
      this.epic.init(data["epicEdit"]) 
      this.dateEdit = new Date(this.epic.dueDate.toString());
      this.editMode=true;
    }else{
      this.projectId = data["projectId"];
    }
  }

  save(): void {
    this.saving = true;
    this.epic.projectId = this.projectId;
    this.epic.dueDate = moment(this.dateEdit).add(1,"days");
    // var oldDate = this.epic.dueDate.getDate();
    // this.epic.dueDate.setDate(oldDate+1);
    this.newEpic.init(this.epic);
    this._epicService
      .create(this.newEpic)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info('SavedSuccessfully');
        this.close(true);
      });
  }

  
  saveFromEdit() :void{
    this.saving = true;
    this.epic.dueDate = moment(this.dateEdit).add(1,"days");
    this._epicService
      .update(this.epic)
      .pipe(
        finalize(() => {
          this.saving = false;
          this.editMode=false;
        })
      )
      .subscribe(() => {
        this.notify.info('Saved Successfully');
        this.close(true);
      });
  }
  close(result: any): void {
    this._dialogRef.close(result);
    this.editMode=false;
  }

}
