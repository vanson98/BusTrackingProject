import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { AppConsts } from '@shared/AppConsts';
import { StudentCheckInDto } from '@shared/service-proxies/service-proxies';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public hubConnection : signalR.HubConnection;
  public data: StudentCheckInDto ;
  public startConnection(){
    this.hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(AppConsts.remoteServiceBaseUrl+"/bushub",{ accessTokenFactory: () => localStorage.getItem('accessToken') })
    .build();

    this.hubConnection
    .start()
    .then(()=>{console.log("Kết nối tới hub thành công")})
    .catch((err)=>{console.log("Kết nối tới hub thất bại: "+err)});
  }

  public closeConnection(){
    this.hubConnection.stop()
    .then(()=>{console.log("Đã ngắt kết nối tới hub")})
    .catch(()=>{console.log("Ngắt kết nối thắt bại")});
  }
}
