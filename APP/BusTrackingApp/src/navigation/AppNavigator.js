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
import Geolocation from '@react-native-community/geolocation';
import SignalRService from '../controllers/SignalRService';
import {PermissionsAndroid} from 'react-native';
import React, { useEffect } from 'react';
import MyStudentComponent from '../components/app/MyStudent';


const Stack = createStackNavigator();
const Tab = createBottomTabNavigator();

const MainTabScreen = (props) => {
  const {user}=props;
  var userRole = user.roles[0];
  var signalRService;

   // Lấy tọa độ hiện tại (Chỉ cho monitor)
   const trackingLocation = async () => {
    try {
      // Check quyền truy cập
      const granted = await PermissionsAndroid.request(
        PermissionsAndroid.PERMISSIONS.ACCESS_FINE_LOCATION,
        {
          title: 'Cấp quyền truy cập',
          message: 'Cho phép ứng dụng truy cập vị trí hiện tại của bạn.',
          buttonNegative: 'Không',
          buttonPositive: 'Đồng ý',
        },
      );
      if (granted === PermissionsAndroid.RESULTS.GRANTED) {
        console.log('Đã có quyền truy cập location (Root)');
        // Set callback cho watcher theo dõi vị trí thay đổi
         Geolocation.watchPosition((position) => {
            var lat = position.coords.latitude;
            var long = position.coords.longitude;
            var lastRegion = {
              latitude: lat,
              longitude: long
            };
            // Gửi về cho hub
            signalRService
              .invoke('SendLocationToGroup',user.userId, lastRegion)
              .then(() => {
                console.log('send: ' + lastRegion.latitude);
              })
              .catch((err) => {
                console.log(err);
              });       
        });
      } else {
        console.log('Quyền truy cập vị trí bị từ chối');
      }
    } catch (err) {
      console.warn(err);
    }
  };


  useEffect(()=>{
    if(userRole=='monitor'){
      signalRService = SignalRService(user.userToken);
      signalRService.start()
      .then(()=>{
          console.log("Kết nối tới hub thành công (ROOT)");
          // Join vào group hub
          signalRService.invoke('AddToGroup',user.userId.toString()).catch((err)=>{console.log(err)});
      })
      .catch((err)=>{console.log("Kết nối tới hub thất bại (ROOT): "+err)});

      signalRService.on("CheckAddedGroup",(res)=>{
          console.log(res);
          trackingLocation();
      }); 
    }
  },[])

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
            name="ListChildRoute"
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
        {
          userRole=='teacher' ?
          <Tab.Screen
            name="ListStudentRoute"
            component={ListStudentNavigator}
            options={{
              tabBarLabel: 'Học sinh của tôi',
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
      name='UpdatePassword'
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

const ListStudentNavigator = ({navigation}) => (
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
        component={MyStudentComponent}
        options={{
          title: 'Học sinh của tôi',
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
