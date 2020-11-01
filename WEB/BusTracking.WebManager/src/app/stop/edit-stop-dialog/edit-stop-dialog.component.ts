import { StopServiceProxy, CreateStopRequestDto, UpdateStopRequestDto, StopDto, RouteServiceProxy, RouteDto } from './../../../shared/service-proxies/service-proxies';
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
  selector: 'app-edit-stop-dialog',
  templateUrl: './edit-stop-dialog.component.html',
  styleUrls: ['./edit-stop-dialog.component.css']
})
export class EditStopDialogComponent extends AppComponentBase implements OnInit,AfterViewInit {
  // Var
  saving = false;
  stop: UpdateStopRequestDto = new UpdateStopRequestDto();
  isActive = true;
  routes: RouteDto[] = []
  // Map
  longitude: number;
  latitude: number;
  zoom:number;
  address: string;
  geoCoder;
  // Time
  timePickUp : Date;
  timeDropOff: Date;
  @ViewChild('search',{read:false,static:true}) searchElementRef: ElementRef;
  
  constructor(
    injector: Injector,
    public _stopService: StopServiceProxy,
    private _dialogRef: MatDialogRef<EditStopDialogComponent>,
    private mapsAPILoader: MapsAPILoader,
    private _routeService: RouteServiceProxy,
    private ngZone: NgZone,
    @Optional() @Inject(MAT_DIALOG_DATA) private _data: object
    ) {
    super(injector);
  }
  
  ngOnInit() {
    let stopOrigin :StopDto = this._data['stopEdit'];
    this.stop.init(stopOrigin);
    this._routeService.getAllPaging(undefined,undefined,undefined,1,999999).subscribe(res=>{
      this.routes = res.items;
    });
    this.timePickUp = new Date(2020,1,1,stopOrigin.timePickUp.hours,stopOrigin.timePickUp.minutes);
    this.timeDropOff = new Date(2020,1,1,stopOrigin.timeDropOff.hours,stopOrigin.timeDropOff.minutes);
  }

  ngAfterViewInit(): void {
    this.mapsAPILoader.load().then(() => {
      this.setLocation();
      this.geoCoder = new google.maps.Geocoder;
      let autocomplete = new google.maps.places.Autocomplete(this.searchElementRef.nativeElement);
      autocomplete.addListener("place_changed", () => {
        this.ngZone.run(() => {
          //get the place result
          let place: google.maps.places.PlaceResult = autocomplete.getPlace();

          //verify result
          if (place.geometry === undefined || place.geometry === null) {
            return;
          }
          this.address = this.searchElementRef.nativeElement.value;
          //set latitude, longitude and zoom
          this.latitude = place.geometry.location.lat();
          this.longitude = place.geometry.location.lng();

          this.zoom = 16;
        });
      });
    });
  }

  markerDragEnd($event) {
    this.latitude = $event.coords.lat;
    this.longitude = $event.coords.lng;
    this.getAddress(this.latitude, this.longitude);
  }

  getAddress(latitude, longitude) {
    this.geoCoder.geocode({ 'location': { lat: latitude, lng: longitude } }, (results, status) => {
      if (status === 'OK') {
        if (results[0]) {
          this.zoom = 16;
          this.address = results[0].formatted_address;
        } else {
          abp.message.error('Không tìm thấy địa chỉ cụ thể');
        }
      } else {
        abp.message.error('Đã có lỗi xảy ra');
      }

    });
  }
  
   // Get Current Location Coordinates
   private setLocation() {
    this.latitude = this.stop.latitude;
    this.longitude = this.stop.longitude;
    this.address = this.stop.address;
    this.zoom = 16;
  }

  setTypeStop($event){
    this.stop.typeStop = $event.value;
  }

  getTime(){
    if(this.stop.typeStop==0){
      var timePick = moment(this.timePickUp);
      this.stop.hourPickUp = timePick.hours();
      this.stop.minutePickUp = timePick.minutes();
      this.stop.hourDropOff = 0;
      this.stop.minuteDropOff = 0;
    }else{
      var timeDrop = moment(this.timeDropOff);
      this.stop.hourDropOff = timeDrop.hour();
      this.stop.minuteDropOff = timeDrop.minutes();
      this.stop.hourPickUp = 0;
      this.stop.minutePickUp = 0;
    }
  }

  save(): void {
    this.getTime();
    this.saving = true;
    this.stop.address = this.address;
    this.stop.latitude = this.latitude;
    this.stop.longitude = this.longitude;
    this.stop.status = this.isActive == true ? 1 : 0;
    this._stopService
      .update(this.stop)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe((result) => {
        if(result != null && result.statusCode==AppResCode.Success){
          this.notify.success("Cập nhật thành công");
          this.close(true);
        }else{
          this.notify.error("Cập nhật thất bại");
        }
      });
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }

}
