import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.css']
})
export class ProjectDetailComponent implements OnInit {

  projectID : string;
  projectName : string;
  projectKey: string;
  constructor(private _activatedRoute:ActivatedRoute) { }

  ngOnInit() {
    this.projectID = this._activatedRoute.snapshot.paramMap.get('id');
    this.projectName = this._activatedRoute.snapshot.paramMap.get('projectName');
    this.projectKey = this._activatedRoute.snapshot.paramMap.get('projectKey');
  }

  onResize(event) {
    // exported from $.AdminBSB.activateAll
    $.AdminBSB.leftSideBar.setMenuHeight();
    $.AdminBSB.leftSideBar.checkStatuForResize(false);

    // exported from $.AdminBSB.activateDemo
    $.AdminBSB.demo.setSkinListHeightAndScroll();
    $.AdminBSB.demo.setSettingListHeightAndScroll();
}
}
