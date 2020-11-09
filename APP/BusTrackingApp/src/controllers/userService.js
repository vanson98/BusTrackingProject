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
    },
    updateAccount: (id,fullName,email,sdt,jwt)=>{
        const url = "/User/UpdateAccount";
        return httpClient.put(url,{
            id: id,
            fullName: fullName,
            email: email,
            phoneNumber: sdt
        },{
            headers:{
                'Authorization': "Bearer " + jwt
            }
        })
    },
    changePass: (id,currentPass,newPass,jwt)=>{
        const url = "/Auth/ChangePassword";
        return httpClient.put(url,{
            userId: id,
            oldPass: currentPass,
            newPass: newPass
        },{
            headers:{
                'Authorization': "Bearer " + jwt
            }
        })
    }
}

export default UserService;