import React, { useEffect,useMemo,useReducer } from 'react';
import {View,ActivityIndicator,Text} from 'react-native';
import {NavigationContainer} from '@react-navigation/native';
import {createStackNavigator} from '@react-navigation/stack';
import {StyleSheet} from 'react-native';
import MainTabScreen from './screens/tab-screens/MainTabScreen';
import RootStackScreen from './screens/RootStackScreen';
import {AuthContext} from './components/context';
import AsyncStorage from '@react-native-community/async-storage'

const App = () => {
  const initialLoginState = {
    isLoading: true,
    userName: null,
    userToken: null
  }

  const loginReducer = (prevState,action) =>{
    switch(action.type){
      case 'RETRIEVE_TOKEN': 
        return {
          ...prevState,
          userToken: action.token,
          isLoading: false
        }
      case 'LOGIN':
        return {
          ...prevState,
          userName: action.id,
          userToken: action.token,
          isLoading: false
        }
      case 'LOGOUT':
        return {
          ...prevState,
          userName: null,
          userToken: null,
          isLoading: false
        }
    }
  }

  const [loginState,dispatch] = useReducer(loginReducer,initialLoginState);

  // Dữ liệu trả vể của useMeno này chỉ trả về một lần duy nhất lúc bắt đầu mounting
  const authContext = useMemo(()=>({
    signIn: async (userName, password)=>{
      let userToken;
      userToken = null;
      if(userName=='son' && password == '123'){
        userToken = "token abcdef"
        try{
          await AsyncStorage.setItem('userToken',userToken)
        }catch(e){
          console.log(e)
        }
      }
      dispatch({type: 'LOGIN' , id: userName, token: userToken})
    },
    signOut: async ()=>{
      try {
        await AsyncStorage.removeItem('userToken',null);
      } catch (e) {
        console.log(e);
      }
      dispatch({type: 'LOGOUT'})
    }
  }),[])

  // Fake delay
  useEffect(()=>{
    setTimeout( async ()=>{
      var userToken = 'null' ;
      try{
        userToken = await AsyncStorage.getItem('userToken');
      }catch(e){
        console.log(e);
      }
      if(userToken==null){
        dispatch({type: 'LOGOUT'})
      }else{
        dispatch({type:'RETRIEVE_TOKEN',token: userToken});
      }
    },1000)
  },[])

  if(loginState.isLoading){
    return (
      <View style={{flex:1,justifyContent:'center',alignItems:'center'}}>
        <ActivityIndicator size='large' color ='#bc2b78'/>
      </View>
    )
  }

  return (
    <AuthContext.Provider value={authContext}>
      <NavigationContainer>
        { loginState.userToken != null ? 
        <MainTabScreen></MainTabScreen>
        :
        <RootStackScreen></RootStackScreen>
      }
      </NavigationContainer>
    </AuthContext.Provider>
    
  );
};


const style = StyleSheet.create({
  navbar: {},
});

export default App;