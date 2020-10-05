import {NavigationContainer} from '@react-navigation/native';
import PropTypes from 'prop-types';
import React from 'react';
import {useSelector} from 'react-redux';
import AppNavigator from './AppNavigator';
import getUser from '../selector/UserSelector';
import AuthNavigator from './AuthNavigator';

function AppNavigation({theme}) {

  const user = useSelector(getUser);

  return (
    <NavigationContainer>
      {user.userToken!=null ? <AppNavigator /> : <AuthNavigator />}
    </NavigationContainer>
  );
}


export default AppNavigation;
