import {
  EpicServiceProxy,
  EpicListOutputListResultDto,
  EpicListOutput,
  SprintListOutput,
  SprintServiceProxy,
  SprintListOutputListResultDto,
  SprintDto
} from "./../../../shared/service-proxies/service-proxies";
import { CreateEpicDialogComponent } from "./../create-epic-dialog/create-epic-dialog.component";
import { PagedListingComponentBase } from "@shared/paged-listing-component-base";
import { CreateSprintDialogComponent } from "./../create-sprint-dialog/create-sprint-dialog.component";
import { AppComponentBase } from "@shared/app-component-base";
import { Component, OnInit, Injector } from "@angular/core";
import { MatDialog } from "@angular/material";
import { ProjectCategoryDto } from "@shared/service-proxies/service-proxies";
import { Router, ActivatedRoute } from "@angular/router";
import { finalize } from "rxjs/operators";
@Component({
  selector: "app-backlogs",
  templateUrl: "./backlogs.component.html",
  styleUrls: ["./backlogs.component.css"]
})
export class BacklogsComponent extends PagedListingComponentBase<ProjectCategoryDto> implements OnInit {
  listEpic: EpicListOutput[] = [];
  listSprint : SprintListOutput[] = [];
  projectId: number;
  isButtonExpandClicked: boolean;

  constructor(
    injector: Injector,
    private _dialog: MatDialog,
    private _activatedroute: ActivatedRoute,
    private _epicService: EpicServiceProxy,
    private _sprintService: SprintServiceProxy
  ) {
    super(injector);
  }

  ngOnInit() {
    this._activatedroute.paramMap.subscribe(params => {
      console.log(params);
      this.projectId = parseInt(params.get("id"));
    });
    this.list();
  }

  protected list(): void {
    // Load Epic List
    this._epicService
      .getListEpic(this.projectId)
      .subscribe((result: EpicListOutputListResultDto) => {
        this.listEpic = result.items;
      });
    this._sprintService
    .getListSprint(this.projectId,0)
    .subscribe((result: SprintListOutputListResultDto) => {
      this.listSprint = result.items;
    });
  }

  LoadSprintByEpic(epicId:number){
    // Load sprint List
    this._sprintService
      .getListSprint(this.projectId,epicId)
      .subscribe((result: SprintListOutputListResultDto) => {
        this.listSprint = result.items;
      });
  }
  protected delete(): void {}

  onButtonExpandClicked($event) {
    var element = $event.target;
    var stateIcon = element.attributes.state.value;
    if (stateIcon == 0) {
      element.classList.remove("fa-caret-right");
      element.classList.add("fa-caret-down");
      element.parentNode.parentElement.nextElementSibling.classList.remove(
        "expand-none"
      );
      element.attributes.state.value = 1;
    } else {
      element.classList.remove("fa-caret-down");
      element.classList.add("fa-caret-right");
      element.parentNode.parentElement.nextElementSibling.classList.add(
        "expand-none"
      );
      element.attributes.state.value = 0;
    }
  }

  onExpandSprint($event) {
    var element = $event.target;
    var exElement =
      element.parentElement.parentNode.parentNode.nextElementSibling;
    var stateIcon = element.attributes.state.value;
    if (stateIcon == 0) {
      element.classList.remove("fa-caret-right");
      element.classList.add("fa-caret-down");
      exElement.classList.remove("expand-none");
      element.attributes.state.value = 1;
    } else {
      element.classList.remove("fa-caret-down");
      element.classList.add("fa-caret-right");
      exElement.classList.add("expand-none");
      element.attributes.state.value = 0;
    }
  }

  showInputSprintName($event) {
    var element = $event.target;
    element.nextElementSibling.style.display = "block";
    element.style.display = "none";
  }

  editSprintName($event) {
    var element = $event.target;
    element.previousSibling.style.display = "block";
    element.style.display = "none";
  }

  keydownInputSprintName($event) {
    var e = $event;
    if (e.keyCode === 13) {
      e.target.blur();
    }
  }

  createSprint(): void {
    this.showCreateOrEditSprintDialog();
  }
  editSprint(sprint: SprintDto) {
    this.showCreateOrEditSprintDialog(sprint);
  }
  
  createEpic() {
    this.showCreateOrEditEpicDialog();
  }
  editEpic(epic : EpicListOutput){
    this.showCreateOrEditEpicDialog(epic);
  }

  deleteEpic(item: EpicListOutput){
      abp.message.confirm(
          'Bạn có chắc muốn xóa epic '+item.epic.name,
          undefined,
          (result: boolean) => {
              if (result) {
                  this._epicService
                      .delete(item.epic.id)
                      .pipe(
                          finalize(() => {
                              abp.notify.success('Xóa thành công');
                              this.list();
                          })
                      )
                      .subscribe(() => { });
              }
          }
      );
  
  }

  showCreateOrEditEpicDialog(epic?: EpicListOutput): void {
    let createOrEditEpicDialog;
    if (epic === undefined || epic===null) {
      createOrEditEpicDialog =this._dialog.open(CreateEpicDialogComponent,{
        data:{
          typeDialog: 1,
          projectId:this.projectId
        }
      });
    } 
    else {
      createOrEditEpicDialog = this._dialog.open(CreateEpicDialogComponent,{
        data:{
          typeDialog: 2,
          epicEdit:epic
        }
      });
    }

    createOrEditEpicDialog.afterClosed().subscribe(result => {
        if (result) {
            this.list();
        }
    });
  }

  showCreateOrEditSprintDialog(sprint?: SprintDto): void {
    let createOrEditSprintDialog;
    if (sprint === undefined || sprint===null) {
      createOrEditSprintDialog =this._dialog.open(CreateSprintDialogComponent,{
        data:{
          typeDialog: 1,
          projectId:this.projectId
        }
      });
    } 
    else {
      createOrEditSprintDialog = this._dialog.open(CreateSprintDialogComponent,{
        data:{
          typeDialog: 2,
          projectId: this.projectId,
          sprintEdit:sprint
        }
      });
    }

    createOrEditSprintDialog.afterClosed().subscribe(result => {
        if (result) {
            this.list();
        }
    });
  }
}
