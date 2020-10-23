import axios from 'axios';
import {Alert} from 'react-native'
import userService from '../controllers/UserService';
import AsyncStorage from '@react-native-community/async-storage';

export const actionTypes = {
  RETRIEVE_SESSION: 'RETRIEVE_SESSION',
  LOGIN: 'LOGIN',
  LOGOUT: 'LOGOUT',
};

export const login= (user) => ({
  type: actionTypes.LOGIN,
  payload: {user},
});

// Đăng nhập 
// NVS - 19/10/2020
export const signIn = (userName, password) => async (dispatch) => {
  var responseObj ;
  try {
    responseObj = await userService.login(userName,password);
    debugger
  } catch (error) {
    Alert.alert('Thông báo','Đã có lỗi xảy ra');
  }
  if(responseObj.statusCode=='B002'){
    var result = responseObj.result;
    var user = {
      userId: result.userId,
      fullName: result.fullName,
      email: result.email,
      roles: result.roles,
      userToken: result.accessToken
    }
    dispatch(login(user));
  }else{
    Alert.alert('Thông báo','Tên đăng nhập hoặc mật khẩu không đúng')
  }
};

// Đăng xuất
// NVS - 19/10/2020
export const signOut = () => async (dispatch) => {
  var user ={
    userId: null,
    fullName: null,
    email: null,
    roles: null,
    userToken: null
  }
  dispatch({
    type: 'LOGOUT',
    payload: {user}
  });
};

// Lấy session từ server
//
export const getUserSession = () => async (dispatch) => {
  var responseObj ;
  var user ={
    userId: null,
    fullName: null,
    email: null,
    roles: null,
    userToken: null
  }
  try {
    responseObj = await userService.getUserSession(token);
  } catch (error) {
    Alert.alert('Thông báo','Đã hết phiên đăng nhập!!!!');
    signOut();
  }
  if(responseObj!=null && responseObj.statusCode=='B002'){
    var result = responseObj.result;
    var user = {
      userId: result.userId,
      fullName: result.fullName,
      email: result.email,
      roles: result.roles,

    }
    dispatch({
      type: actionTypes.RETRIEVE_SESSION,
      payload: {user}
    });
  }
  dispatch({
    type: actionTypes.LOGOUT
  })
}


// axios.get('http://192.168.0.121:45456/api/Auth/GetAllRole')
  // .then(res=>{
  //   console.log(res);
  // }).catch(err=>{
  //   console.log(err);
  // })