import axios from 'axios';
import {Alert} from 'react-native'
import userService from '../controllers/userService';

export const actionTypes = {
  RETRIEVE_TOKEN: 'RETRIEVE_TOKEN',
  LOGIN: 'LOGIN',
  LOGOUT: 'LOGOUT',
};

export const login= (user) => ({
  type: actionTypes.LOGIN,
  payload: {user},
});


export const signIn = (userName, password) => async (dispatch) => {
  var responseObj ;
  //call service 
  try {
    responseObj = await userService.login(userName,password);
  } catch (error) {
    console.log(error);
  }
  if(responseObj.statusCode=='B002'){
    dispatch(login({ id: userName, userToken: responseObj.result}));
  }else{
    Alert.alert('Thông báo','Tên đăng nhập hoặc mật khẩu không đúng')
  }
};

export const signOut = () => async (dispatch) => {
  // try {
  //   await AsyncStorage.removeItem('userToken',null);
  // } catch (e) {
  //   console.log(e);
  // }
  dispatch({type: 'LOGOUT',payload:null});
};


// axios.get('http://192.168.0.121:45456/api/Auth/GetAllRole')
  // .then(res=>{
  //   console.log(res);
  // }).catch(err=>{
  //   console.log(err);
  // })