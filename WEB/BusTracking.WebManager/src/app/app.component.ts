import { Component, ViewContainerRef, Injector, OnInit, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';


@Component({
    templateUrl: './app.component.html'
})
export class AppComponent extends AppComponentBase implements OnInit {

    private viewContainerRef: ViewContainerRef;

    constructor(
        injector: Injector
    ) {
        super(injector);
    }

    ngOnInit(): void {

      
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
