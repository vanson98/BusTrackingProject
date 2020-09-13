import { SprintServiceProxy, SprintDto } from './../../../shared/service-proxies/service-proxies';
import { Component, OnInit, Injector, Inject, Optional } from "@angular/core";
import { EpicDto, EpicServiceProxy } from "@shared/service-proxies/service-proxies";
import { AppComponentBase } from "@shared/app-component-base";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import * as moment from 'moment';
import { finalize } from 'rxjs/operators';

@Component({
  selector: "app-create-sprint-dialog",
  templateUrl: "./create-sprint-dialog.component.html",
  styleUrls: ["./create-sprint-dialog.component.css"]
})
export class CreateSprintDialogComponent extends AppComponentBase implements OnInit {
  listEpic: EpicDto[] = [];
  sprint : SprintDto = new SprintDto();
  saving = false;
  editMode = false;
  projectId = null;
  startDate : Date ;
  duaDate: Date;

  constructor(injector: Injector,
    private _epicService: EpicServiceProxy,
    private _sprintService: SprintServiceProxy,
    private _dialogRef: MatDialogRef<CreateSprintDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _data: Object) {
      super(injector)
    }

  ngOnInit() {
    var data = this._data;
    if(data["typeDialog"]!=1){
      this.sprint.init(data["sprintEdit"]) ;
      this.startDate = new Date(this.sprint.startDate.toString());
      this.duaDate = new Date(this.sprint.dueDate.toString());
      this.projectId = data["projectId"];
      this.editMode=true;
    }else{
      this.projectId = data["projectId"];
    }
    // Get all Epic 
    this._epicService.getAll(this.projectId,1,99999).subscribe(result =>{
      this.listEpic = result.items;
    })
  }

  save() {
    this.saving = true;
    this.sprint.projectId = this.projectId;
    this.sprint.startDate = moment(this.startDate).add(1,"days");
    this.sprint.dueDate = moment(this.duaDate).add(1,"days");

    this._sprintService
      .create(this.sprint)
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
    this.sprint.startDate = moment(this.startDate).add(1,"days");
    this.sprint.dueDate = moment(this.duaDate).add(1,"days");
    this._sprintService
      .update(this.sprint)
      .pipe(
        finalize(() => {
          this.saving = false;
          this.editMode=false;
        })
      )
      .subscribe(() => {
        this.notify.info('Updated Successfully');
        this.close(true);
      });
  }
  close(result: any): void {
    this._dialogRef.close(result);
    this.editMode=false;
  }

}
