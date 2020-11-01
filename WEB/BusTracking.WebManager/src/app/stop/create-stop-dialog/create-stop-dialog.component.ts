
import { StopServiceProxy, CreateStopRequestDto, RouteDto, RouteServiceProxy } from './../../../shared/service-proxies/service-proxies';
import { AfterViewInit, Component, ElementRef, Injector, NgZone, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MatDialogRef } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { getTime } from 'ngx-bootstrap/chronos/utils/date-getters';
import * as moment from 'moment';
import { AppResCode } from '@shared/const/AppResCode';
import { MapsAPILoader } from '@agm/core';
declare var google: any;

@Component({
  selector: 'app-create-stop-dialog',
  templateUrl: './create-stop-dialog.component.html',
  styleUrls: ['./create-stop-dialog.component.css']
})
export class CreateStopDialogComponent extends AppComponentBase implements OnInit,AfterViewInit  {
  // Var
  saving = false;
  stop: CreateStopRequestDto = new CreateStopRequestDto();
  routes: RouteDto[] = []
  isActive = true;
  // Map
  longitude : number;
  latitude : number;
  zoom:number;
  address: string;
  geoCoder;
  // Time
  timePickUp : Date;
  timeDropOff: Date;

  @ViewChild('search',{read:false,static:true}) searchElementRef: ElementRef;
  
  constructor(
    injector: Injector,
    public _diverService: StopServiceProxy,
    private _dialogRef: MatDialogRef<CreateStopDialogComponent>,
    private mapsAPILoader: MapsAPILoader,
    private _routeService: RouteServiceProxy,
    private ngZone: NgZone
    ) {
    super(injector);
  }

  ngAfterViewInit(): void {
    this.mapsAPILoader.load().then(() => {
      this.setCurrentLocation();
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

  ngOnInit() {
    this._routeService.getAllPaging(undefined,undefined,undefined,1,999999).subscribe(res=>{
      this.routes = res.items;
    });
  }

  setTypeStop($event){
    this.stop.typeStop = $event.value;
  }

  setCurrentLocation() {
    if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.zoom = 16;
      });
    }
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

  getTime(){
    if(this.stop.typeStop==0){
      var timePick = moment(this.timePickUp);
      this.stop.hourPickUp = timePick.hours();
      this.stop.minutePickUp = timePick.minutes();
    }else{
      var timeDrop = moment(this.timeDropOff);
      this.stop.hourDropOff = timeDrop.hour();
      this.stop.minuteDropOff = timeDrop.minutes();
    }
  }

  save(): void {
    this.getTime();
    this.saving = true;
    this.stop.address = this.address;
    this.stop.latitude = this.latitude;
    this.stop.longitude = this.longitude;
    this.stop.status = this.isActive == true ? 1 : 0;
    this._diverService
      .create(this.stop)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe((result) => {
        if(result != null && result.statusCode==AppResCode.Success){
          this.notify.success("Tạo mới thành công");
          this.close(true);
        }else{
          this.notify.error("Tạo mới thất bại");
        }
      });
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }

}
