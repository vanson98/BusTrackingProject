import React, { useEffect, useState } from 'react';
import {View,Text, StyleSheet, Dimensions, Alert,PermissionsAndroid,TouchableOpacity} from 'react-native';
import MapView, {Callout, Marker, PROVIDER_GOOGLE} from 'react-native-maps';
import { useSelector } from 'react-redux';
import StudentService from '../../../controllers/StudentService';
import Geolocation from '@react-native-community/geolocation';
import SignalRService from '../../../controllers/SignalRService';
import Ionicons from 'react-native-vector-icons/Ionicons';

const {width, height} = Dimensions.get('window')
const SCREEN_WIDTH = width
const SCREEN_HEIGHT = height
const ASPECT_RATIO = width / height
const LATITUDE_DELTA = 0.0922 / 100
const LONGITUDE_DELTA = (LATITUDE_DELTA + ASPECT_RATIO) /100


const RouteMapComponent = ({route})=>{
    // ================= Prop ===============
    var watchId = null
    var user = useSelector((state)=>state.user);
    var userRole = user.roles[0];
    var monitorId = route.params != undefined ? route.params.monitorId : null;
    var typeMap = route.params != undefined ? route.params.typeMap : null;
    var _map;
    // ================= State ==============
    const [myLocation, setMyLocation ] = useState({
        latitude: 0,
        longitude: 0,
        latitudeDelta: 0,
        longitudeDelta: 0
    })
    // Danh sách các điểm dừng
    const [stops,setStops] = useState([])

    // Lấy tọa độ hiện tại (Chỉ cho monitor)
    const getCurentLocation = async ()=>{
        try {
            // Check quyền truy cập
            const granted = await PermissionsAndroid.request(
              PermissionsAndroid.PERMISSIONS.ACCESS_FINE_LOCATION,
              {
                title: "Cấp quyền truy cập",
                message:
                  "Cho phép ứng dụng truy cập vị trí hiện tại của bạn.",
                buttonNegative: "Không",
                buttonPositive: "Đồng ý"
              }
            );
            if (granted === PermissionsAndroid.RESULTS.GRANTED) {
                console.log("Đã có quyền truy cập vị trí");
                // Lấy tọa độ hiện tại sau khi được phép
                await Geolocation.getCurrentPosition(
                    (position)=>{
                        var lat = parseFloat(position.coords.latitude);
                        var long = parseFloat(position.coords.longitude)
                        setMyLocation({
                            latitude: lat,
                            longitude: long,
                            latitudeDelta: LATITUDE_DELTA,
                            longitudeDelta: LONGITUDE_DELTA
                        })
                    },
                    (error)=>{
                        Alert.alert(JSON.stringify(error))
                    },
                    { enableHighAccuracy: false, timeout: 20000}
                )

                // Set callback cho watcher theo dõi vị trí thay đổi
                watchId = Geolocation.watchPosition((position)=>{
                    watchLocation(position);
                })
            } else {
              console.log("Quyền truy cập vị trí bị từ chối");
            }
        } catch (err) {
            console.warn(err);
        }
    }

    // Set lại vị trí trên giao diện (của monitor)
    const watchLocation = (position) => {
        var lat = position.coords.latitude;
        var long = position.coords.longitude;
        var lastRegion = {
            latitude: lat,
            longitude: long,
            longitudeDelta: LONGITUDE_DELTA,
            latitudeDelta: LATITUDE_DELTA
        }
        setMyLocation(lastRegion)
    }

    useEffect(()=>{
        if(userRole=='monitor'){
            getCurentLocation();
        }else if(userRole=='parent'){
            // Kết nối tới group trên hub để nhận tọa độ gửi về
            var signalRService = SignalRService(user.userToken);
            signalRService.start()
            .then(()=>{
                console.log("Kết nối tới hub thành công (PARENT-MAP)");
                signalRService.invoke('AddToGroup',monitorId.toString());
            })
            .catch((err)=>{console.log("Kết nối tới hub thất bại (PARENT-MAP)"+err)});

            signalRService.on('ReceiveLocation',res=>{
                console.log(res);
                setMyLocation({
                    longitudeDelta: LONGITUDE_DELTA,
                    latitudeDelta: LATITUDE_DELTA,
                    latitude: res.latitude,
                    longitude: res.longitude
                })
            })
        }
        return ()=>{
            if(userRole=='parent'){
                signalRService.stop().then(()=>{console.log("Đã ngắt kết nối hub (MAP)")})
            }else if(userRole=='monitor'){
                Geolocation.clearWatch(watchId);
            }
        }
    },[])

    useEffect(()=>{
        if(userRole=='monitor'){
            async function getAllStop(){
                var res = await StudentService.getAllStopOfMonitor(monitorId,typeMap,user.userToken);
                setStops(res.result)
            }
            getAllStop();
        }
    },[])

    // const goToInitialLocation = () => {
        
    //     MapView.animateToRegion(myLocation, 2000);
    // }

    const onPressZoomIn = () => {
        setMyLocation({
            ...myLocation,
            latitudeDelta : myLocation.latitudeDelta / 5,
            longitudeDelta : myLocation.longitudeDelta / 5
        })
        _map.animateToRegion(myLocation, 100);
    }

    const onPressZoomOut = () => {
        setMyLocation({
            ...myLocation,
            latitudeDelta : myLocation.latitudeDelta * 5,
            longitudeDelta : myLocation.longitudeDelta * 5
        })
        _map.animateToRegion(myLocation, 100);
    }

    return (
        <View style={styles.map_container}>
            <MapView
                ref={component=>_map=component}
                style={styles.map_view}
                provider = {PROVIDER_GOOGLE}
                region={myLocation}
                mapType="standard"
                zoomEnabled={true}
                pitchEnabled={true}
                showsUserLocation={true}
                showsCompass={true}
                showsBuildings={true}
                showsTraffic={true}
                showsIndoors={true}
            >
                {stops.map((stop,index)=>(
                    <MapView.Marker
                        coordinate={stop.coordinate}
                        key={index}
                        title={stop.name}
                        >
                        <Callout>
                            <View style={styles.callout_container}>
                                <View style={styles.name}>
                                    <Text style={{fontWeight:"bold"}}>{stop.name}</Text>
                                </View>
                                <View style={styles.address_container}>
                                    <Text style={styles.address}>{stop.address}</Text>
                                </View>
                                <View style={styles.amount}>
                                    <Text >Số lượng học sinh: {stop.numberOfStudents}</Text>
                                </View>
                            </View>
                        </Callout>
                    </MapView.Marker>
                ))}
                {
                userRole=='parent'? 
                    <MapView.Marker
                        coordinate={{
                            longitude: myLocation.longitude,
                            latitude: myLocation.latitude
                        }}
                        image={require("../../../share/assets/image/carMarker.png")}
                        >
                           
                    </MapView.Marker>:
                    null
                }
                
            </MapView>
            <TouchableOpacity
                    style={styles.zoomIn}
                    onPress={()=>{onPressZoomIn()}}
                    >
                    <Ionicons 
                        name="add-circle-outline" 
                        size={35} 
                    />
            </TouchableOpacity>
            <TouchableOpacity
                style={styles.zoomOut}
                onPress={()=>{onPressZoomOut()}}
                >
                <Ionicons 
                    name="remove-circle-outline" 
                    size={35} 
                    />
            </TouchableOpacity>
        </View>
        
    )
}

const styles = StyleSheet.create({
    callout_container: {
        flexDirection: 'column',
        width: 'auto',
    },
    map_container: {
        flex: 1,
        width: '100%',
        height: '100%'
    },
    map_view: {
        ...StyleSheet.absoluteFillObject,
        top: 0,
        left: 0,
        right: 0,
        bottom: 0,
    },
    zoomIn: {
        width: 35,
        height: 35,
        position: 'absolute',
        right: 5,
        top: 5,
    },
    zoomOut: {
        width: 35,
        height: 35,
        position: 'absolute',
        right: 5,
        top: 40,
    },
    address_container:{
        width: 'auto',
        flexGrow: 1,
        height: 0
    },
    address: {
       
    },
    name: {
    },
    amount: {
        marginTop: 20
    }
})
export default RouteMapComponent;