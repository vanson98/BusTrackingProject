import React, { useEffect,useMemo } from 'react';
import {View,ActivityIndicator,Text} from 'react-native';
import {NavigationContainer} from '@react-navigation/native';
import {createStackNavigator} from '@react-navigation/stack';
import {StyleSheet} from 'react-native';
import MainTabScreen from './screens/tab-screens/MainTabScreen';
import RootStackScreen from './screens/RootStackScreen';
import {AuthContext} from './components/context';
 
const RootStack = createStackNavigator();
const MainStack = createStackNavigator();

const App = () => {
  const [isLoading, setIsLoading] = React.useState(true);
  const [userToken, setUserToken] = React.useState(null);

  // Dữ liệu trả vể của useMeno này chỉ trả về một lần duy nhất lúc bắt đầu mounting
  const authContext = useMemo(()=>({
    signIn: ()=>{
      setUserToken('abc');
      setIsLoading(false);
    },
    signOut: ()=>{
      setUserToken(null);
      setIsLoading(false);
    }
  }),[])
  // Fake delay
  useEffect(()=>{
    setTimeout(()=>{
      setIsLoading(false)
    },1000)
  },[])

  if(isLoading){
    return (
      <View style={{flex:1,justifyContent:'center',alignItems:'center'}}>
        <ActivityIndicator size='large' color ='#bc2b78'/>
      </View>
    )
  }

  return (
    <AuthContext.Provider value={authContext}>
      <NavigationContainer>
        { userToken != null ? 
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