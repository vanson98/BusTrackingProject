import React from 'react';
import {createStackNavigator} from '@react-navigation/stack';
import {createBottomTabNavigator} from '@react-navigation/bottom-tabs';
import Ionicons from 'react-native-vector-icons/Ionicons';
import RouteComponent from '../components/app/Route';
import RouteMapComponent from '../components/app/Route/RouteMap';
import DetailStudentComponent from '../components/app/Route/DetailStudent';
import NotificationComponent from '../components/app/Notification/Notification';
import ProfileComponent from '../components/app/Profile';

const Stack = createStackNavigator();
const Tab = createBottomTabNavigator();

const MainTabScreen = () => {
  return (
    <Tab.Navigator
      initialRouteName="PickUpRoute"
      lazy="true"
      tabBarOptions={{
        activeTintColor: '#FF9800',
        labelStyle: {fontSize: 13},
      }}>
      <Tab.Screen
        name="PickUpRoute"
        component={RouteNavigator}
        options={{
          tabBarLabel: 'Lượt đi',
          tabBarIcon: ({color, size}) => (
            <Ionicons name="person-add" color={color} size={size} />
          ),
        }}
      />
      <Tab.Screen
        name="DropOffRoute"
        component={RouteNavigator}
        options={{
          tabBarLabel: 'Lượt về',
          tabBarIcon: ({color, size}) => (
            <Ionicons name="person-remove" color={color} size={size} />
          ),
        }}
      />
      <Tab.Screen
        name="Notification"
        component={NotificationComponent}
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
        component={ProfileComponent}
        options={{
          tabBarLabel: 'Tài khoản',
          tabBarIcon: ({color, size}) => (
            <Ionicons name="person-circle-outline" color={color} size={26} />
          ),
        }}
      />
    </Tab.Navigator>
  );
};

const RouteNavigator = ({navigation}) => (
  <Stack.Navigator
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
    <Stack.Screen
      name="RouteCheck"
      component={RouteComponent}
      options={{
        title: 'Lượt đi',
      }}
    />
    <Stack.Screen
        name="DetailStudent"
        component={DetailStudentComponent}
        options={{
          title: 'Thông tin học sinh',
        }}
    />
    <Stack.Screen
      name="RouteMap"
      component={RouteMapComponent}
      options={{
        title: 'Bản đồ tuyến đường',
      }}
    />
  </Stack.Navigator>
);

export default MainTabScreen;
