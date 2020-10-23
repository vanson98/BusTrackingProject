import {NavigationContainer} from '@react-navigation/native';
import PropTypes from 'prop-types';
import React from 'react';
import {useSelector,useDispatch} from 'react-redux';
import AppNavigator from './AppNavigator';
import getUser from '../selector/UserSelector';
import AuthNavigator from './AuthNavigator';
import { getUserSession } from '../actions/UserActions';

function AppNavigation({theme}) {

  const dispatch = useDispatch();
  //dispatch(getUserSession());
  const user = useSelector(getUser);

  return (
    <NavigationContainer>
      {user.userToken!=null ? <AppNavigator userRole={user.roles[0]}/> : <AuthNavigator />}
    </NavigationContainer>
  );
}


export default AppNavigation;
