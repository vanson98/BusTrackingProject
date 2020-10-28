import React from 'react';
import {createStackNavigator} from '@react-navigation/stack';
import {createBottomTabNavigator} from '@react-navigation/bottom-tabs';
import Ionicons from 'react-native-vector-icons/Ionicons';
import RouteComponent from '../components/app/Route';
import DetailStudentComponent from '../components/app/DetailStudent';
import NotificationsComponent from '../components/app/Notifications';
import ProfileComponent from '../components/app/Profile';
import UpdateAccount from '../components/app/Profile/UpdateAccount/UpdateAccount';
import UpdatePassword from '../components/app/Profile/UpdatePassword/UpdatePassword';
import MyChildrenComponent from '../components/app/MyChildren';
import RouteMapComponent from '../components/app/RouteMap';

const Stack = createStackNavigator();
const Tab = createBottomTabNavigator();

const MainTabScreen = (props) => {
  const {userRole}=props;

  return (
    <Tab.Navigator
      initialRouteName="PickUpRoute"
      lazy="true"
      tabBarOptions={{
        activeTintColor: '#FF9800',
        labelStyle: {fontSize: 13},
      }}
      >
        {
          userRole=='monitor'? 
          <Tab.Screen
            name="PickUpRoute"
            component={PickUpRouteNavigator}
            options={{
              tabBarLabel: 'Lượt đi',
              tabBarIcon: ({color, size}) => (
                <Ionicons name="person-add" color={color} size={size} />
              )
            }}
          /> 
          : null
        }
        {
          userRole=='monitor' ?
          <Tab.Screen
            name="DropOffRoute"
            component={DropOffRouteNavigator}
            options={{
              tabBarLabel: 'Lượt về',
              tabBarIcon: ({color, size}) => (
                <Ionicons name="person-remove" color={color} size={size} />
              ),
            }}
          />
          : null
        }
        {
          userRole=='parent' ?
          <Tab.Screen
            name="DropOffRoute"
            component={ListChildNavigator}
            options={{
              tabBarLabel: 'Con của tôi',
              tabBarIcon: ({color, size}) => (
                <Ionicons name="people-outline" color={color} size={size} />
              ),
            }}
          />
          : null
        }
        <Tab.Screen
          name="Notification"
          component={NotifyStackNavigator}
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
          component={AccountStackNavigator}
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

const PickUpRouteNavigator = ({navigation}) => (
  <Stack.Navigator
    screenOptions={{
      headerStyle: {
        backgroundColor: '#FF9800',
      },
      headerTitleStyle: {
        color: '#fff',
        fontWeight: 'bold',
        textAlign: 'center',
        alignSelf: 'center',
      },
    }}>
    <Stack.Screen name="RouteCheck" >
        {props => <RouteComponent {...props} typeCheck={0} />}
    </Stack.Screen>
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

const DropOffRouteNavigator = ({navigation}) => (
  <Stack.Navigator
    screenOptions={{
      headerStyle: {
        backgroundColor: '#FF9800',
      },
      headerTitleStyle: {
        color: '#fff',
        fontWeight: 'bold',
        textAlign: 'center',
        alignSelf: 'center',
      },
    }}>
    <Stack.Screen name="RouteCheck" >
        {props => <RouteComponent {...props} typeCheck={1} />}
    </Stack.Screen>
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

const NotifyStackNavigator = () =>(
  <Stack.Navigator
    screenOptions={{
      headerStyle: {
        backgroundColor: '#FFA000',
      },
      headerTitleStyle: {
        color: '#fff',
        fontWeight: 'bold',
        textAlign: 'center',
        alignSelf: 'center',
      },
    }}>
    <Stack.Screen
      name="Notification"
      component={NotificationsComponent}
      options={{
        title: 'Thông báo'
      }}>
    </Stack.Screen>
  </Stack.Navigator>
)

const AccountStackNavigator = ()=>(
  <Stack.Navigator 
  screenOptions={{
    headerStyle: {
      backgroundColor: '#FF9800',
    },
    headerTitleStyle: {
      color: '#fff',
      fontWeight: 'bold',
      textAlign: 'center',
      alignSelf: 'center',
    }
  }}>
    <Stack.Screen
      name='Account'
      component={ProfileComponent}
      options={{
        title: 'Tài khoản',
      }}
      />
    <Stack.Screen
      name='UpdateProfile'
      component={UpdateAccount}
      options={{
        title: 'Cập nhật tài khoản',
      }}
    />
    <Stack.Screen
      name='UpdatePass'
      component={UpdatePassword}
      options={{
        title: 'Cập nhật mật khẩu',
      }}
    />
  </Stack.Navigator>
)

const ListChildNavigator = ({navigation}) => (
  <Stack.Navigator
    screenOptions={{
      headerStyle: {
        backgroundColor: '#FF9800',
      },
      headerTitleStyle: {
        color: '#fff',
        fontWeight: 'bold',
        textAlign: 'center',
        alignSelf: 'center',
      },
    }}>
    <Stack.Screen
        name="ListChild"
        component={MyChildrenComponent}
        options={{
          title: 'Con của tôi',
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
