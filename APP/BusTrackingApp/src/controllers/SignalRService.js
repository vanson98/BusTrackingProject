import * as signalR from '@aspnet/signalr';

  function SignalRService(token){
    const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("http://192.168.42.150:5005/bushub",{ accessTokenFactory: () => token })
    .build();
    return hubConnection;
  }

  export default SignalRService;