import {NavigationContainer} from '@react-navigation/native';
import React from 'react';
import {useSelector, useDispatch} from 'react-redux';
import AppNavigator from './AppNavigator';
import getUser from '../selector/UserSelector';
import AuthNavigator from './AuthNavigator';

function AppNavigation({theme}) {
  // =================== Props ===============
  const dispatch = useDispatch();
  //dispatch(getUserSession());
  var user = useSelector(getUser);

  return (
    <NavigationContainer>
      {user.userToken != null ? (
        <AppNavigator user={user} />
      ) : (
        <AuthNavigator />
      )}
    </NavigationContainer>
  );
}

export default AppNavigation;
