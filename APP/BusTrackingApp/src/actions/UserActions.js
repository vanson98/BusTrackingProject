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

const logout = () => ({
  type: actionTypes.LOGOUT,
  payload: null
})

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
  console.log(responseObj);
  if(responseObj.statusCode=='B002'){
    var result = responseObj.result;
    var user = {
      userId: result.userId,
      fullName: result.fullName,
      email: result.email,
      phoneNumber: result.phoneNumber,
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
// NVS - 19/10/2020
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

export const updateAccount = (id,fullName,email,sdt,jwt) => async (dispatch) => {
  var response ;
  try {
    response = await userService.updateAccount(id,fullName,email,sdt,jwt);
  } catch (error) {
    Alert.alert('Thông báo','Đã có lỗi xảy ra');
  }
  if(response.statusCode=='B002'){
    var user = {
      fullName: fullName,
      email: email,
      phoneNumber: sdt
    }
    dispatch(login(user));
    Alert.alert('Thông báo','Cập nhật thành công')
  }else{
    Alert.alert('Thông báo','Cập nhật thất bại')
  }
}


export const updatePassword = (id,currentPass,newPass,jwt) => async (dispatch) => {
  var response ;
  try {
    response = await userService.changePass(id,currentPass,newPass,jwt);
  } catch (error) {
    Alert.alert('Thông báo','Đã có lỗi xảy ra');
  }
  if(response.statusCode=='B002'){
    dispatch(logout());
    Alert.alert('Thông báo','Cập nhật mật khẩu thành công')
  }else{
    Alert.alert('Thông báo','Cập nhật mật khẩu thất bại')
  }
}
