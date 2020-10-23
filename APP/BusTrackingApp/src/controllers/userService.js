import httpClient from './httpClient';

const UserService =  {
    login: (username,password)=>{
        const url = '/Auth/AppAuthenticate';
        return httpClient.post(url,{
            'userName': username, 
            'password': password, 
            'rememberMe': true
        });
    },
    getUserSession: (jwt)=>{
        const url = "/Auth/GetUserSession";
        return httpClient.get(url,{
            'Authorization': "Bearer " + jwt
        });
    }
}

export default UserService;