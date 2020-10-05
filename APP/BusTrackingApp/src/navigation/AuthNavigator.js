import React from 'react';
import { createStackNavigator } from '@react-navigation/stack';
import LoginComponent from '../components/account/Login';
import SplashComponent from '../components/account/Splash';

const Stack = createStackNavigator();
 
function AuthNavigator(){
    return (
        <Stack.Navigator headerMode='none'>
            <Stack.Screen name="SplashScreen" component={SplashComponent}/>
            <Stack.Screen name="LoginScreen" component={LoginComponent}/>
        </Stack.Navigator>
    );
}

export default AuthNavigator;