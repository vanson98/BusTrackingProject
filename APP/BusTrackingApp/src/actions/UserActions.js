import {Alert} from 'react-native'

export const actionTypes = {
  RETRIEVE_TOKEN: 'RETRIEVE_TOKEN',
  LOGIN: 'LOGIN',
  LOGOUT: 'LOGOUT',
};

export const login= (user) => ({
  type: actionTypes.LOGIN,
  payload: {user: user},
});


export const signIn = (userName, password) => async dispatch => {
  let userToken;
  userToken = null;
  if (userName == 'son' && password == '123') {
    userToken = 'token abcdef';
  }else{
    Alert.alert('Thông báo!', 'Tên đăng nhập hoặc mật khẩu không đúng', [
      {text:'OK'}
    ]);
  }
  dispatch(login({ id: userName, userToken: userToken}));
};

export const signOut = () => async (dispatch) => {
  // try {
  //   await AsyncStorage.removeItem('userToken',null);
  // } catch (e) {
  //   console.log(e);
  // }
  dispatch({type: 'LOGOUT',payload:null});
};
