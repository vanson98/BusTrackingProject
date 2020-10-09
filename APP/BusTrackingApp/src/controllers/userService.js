import httpClient from './httpClient';

const UserService =  {
    login: (username,password)=>{
        const url = '/Auth/authenticate';
        return httpClient.post(url,{
            'userName': username, 
            'password': password, 
            'rememberMe': true
        });
    }
}

export default UserService;