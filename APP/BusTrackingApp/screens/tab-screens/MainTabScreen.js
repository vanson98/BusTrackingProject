import React from 'react';
import { createStackNavigator} from '@react-navigation/stack';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import RoundsScreen from './RoundsScreen';
import DetailRoundScreen from './DetailRoundScreen';
import NotificationScreen from './NotificationScreen';
import ProfileScreen from './ProfileScreen';
import Ionicons from 'react-native-vector-icons/Ionicons';
const RoundsStack = createStackNavigator();
const Tab = createBottomTabNavigator();

const MainTabScreen = ()=>{
    return (
          <Tab.Navigator
            initialRouteName="Route"
            lazy='true'
            tabBarOptions={{
              activeTintColor: '#FF9800',
              labelStyle: {fontSize:13}
            }}>
            <Tab.Screen
              name="Route"
              component={RoundsStackScreen}
              options={{
                tabBarLabel: 'Tuyến',
                tabBarIcon: ({color, size}) => (
                  <Ionicons name="bus" color={color} size={size} />
                ),
              }}
            />
            <Tab.Screen
              name="Notification"
              component={NotificationScreen}
              options={{
                tabBarLabel: 'Thông báo',
                tabBarIcon: ({color, size}) => (
                  <Ionicons name="notifications-outline" color={color} size={26} />
                ),
                tabBarBadge: 3,
              }}
            />
            <Tab.Screen
              name="Profile"
              component={ProfileScreen}
              options={{
                tabBarLabel: 'Tài khoản',
                tabBarIcon: ({color, size}) => (
                  <Ionicons name="person-circle-outline" color={color} size={26} />
                ),
              }}
            />
          </Tab.Navigator>
      );
}


const RoundsStackScreen = ({navigation}) => (
    <RoundsStack.Navigator
      screenOptions={{
        headerStyle: {
          backgroundColor: '#009387',
        },
        headerTitleStyle: {
          color: '#fff',
          fontWeight: 'bold',
          textAlign: 'center',
          alignSelf: 'center',
        },
      }}>
      <RoundsStack.Screen
        name="Rounds"
        component={RoundsScreen}
        options={{ 
          title: 'Lượt đi',
        }}
        ></RoundsStack.Screen>
      <RoundsStack.Screen
        name="DetailRound"
        component={DetailRoundScreen}
        options={{
          title: 'Chi tiết lượt đi',
        }}></RoundsStack.Screen>
    </RoundsStack.Navigator>
  );

export default MainTabScreen;

