import { StopServiceProxy, CreateStopRequestDto, RouteDto, RouteServiceProxy } from './../../../shared/service-proxies/service-proxies';
import { AfterViewInit, Component, ElementRef, Inject, Injector, NgZone, OnInit, Optional, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { getTime } from 'ngx-bootstrap/chronos/utils/date-getters';
import * as moment from 'moment';
import { AppResCode } from '@shared/const/AppResCode';
import { MapsAPILoader } from '@agm/core';
declare var google: any;

@Component({
  selector: 'app-map-checkin-dialog',
  templateUrl: './map-checkin-dialog.component.html',
  styleUrls: ['./map-checkin-dialog.component.css']
})
export class MapCheckinDialogComponent implements OnInit {
// Map
longitude : number;
latitude : number;
zoom:number;
address: string;
geoCoder;
  constructor(
    private _dialogRef: MatDialogRef<MapCheckinDialogComponent>,
    private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone,
    @Optional() @Inject(MAT_DIALOG_DATA) private _data: object
  ) { 

  }

  ngOnInit() {
    this.longitude = this._data['long'];
    this.latitude = this._data['lat'];
  }

  ngAfterViewInit(): void {
    this.mapsAPILoader.load().then(() => {
      this.setLocation();
      this.geoCoder = new google.maps.Geocoder;
    });
  }
 
  
   // Get Current Location Coordinates
   private setLocation() {
    this.latitude = this.latitude;
    this.longitude = this.longitude;
    this.zoom = 16;
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }
}
