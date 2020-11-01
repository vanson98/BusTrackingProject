import React, { useEffect, useState } from 'react';
import {View,Text,Button, StyleSheet, Dimensions, Alert,PermissionsAndroid} from 'react-native';
import MapView, {Marker, PROVIDER_GOOGLE} from 'react-native-maps';
import { useSelector } from 'react-redux';
import Geolocation from '@react-native-community/geolocation';
import SignalRService from '../../../controllers/SignalRService';

const {width, height} = Dimensions.get('window')
const SCREEN_WIDTH = width
const SCREEN_HEIGHT = height
const ASPECT_RATIO = width / height
const LATITUDE_DELTA = 0.0922
const LONGITUDE_DELTA = LATITUDE_DELTA + ASPECT_RATIO


const RouteMapComponent = ({route})=>{
    // ================= Prop ===============
    var watchId = null
    var user = useSelector((state)=>state.user);
    var userRole = user.roles[0];
    var signalRService;
    var { busId } = route.params
    // ================= State ==============
    const [myLocation, setMyLocation ] = useState({
        latitude: 21.016453337618890,
        longitude: 105.81447296773682,
        latitudeDelta: 0,
        longitudeDelta: 0
    })
    // Danh sách các điểm dừng
    const [stops,setStops] = useState(
        [
            {
                title: 'Điểm dừng 01',
                coordinate: {
                    latitude: 21.016453337618890,
                    longitude: 105.81447296773682
                }
            },
            {
                title: 'Điểm dừng 02',
                coordinate: {
                    latitude: 21.019284192021840,
                    longitude: 105.80912652418978
                }
            }
        ]
    )

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
                console.log("Đã có quyền truy cập location");

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

    // Set lại vị trí trên giao diện và gửi về hub (của monitor)
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
        // Gửi về cho hub
        signalRService
        .invoke('SendLocationToGroup',busId.toString(),lastRegion)
        .then(()=>{
            console.log('send: '+lastRegion.latitude)
        })
        .catch((err)=>{
            console.log(err)
        })
    }

    useEffect(()=>{
        console.log("BusId: "+busId);
        signalRService = SignalRService(user.userToken);

        signalRService.start()
        .then(()=>{
            console.log("Kết nối tới hub thành công (MAP)");
            // Join vào group hub
            if(userRole=='monitor'){
                // Chỉ lấy tọa độ 1 lần
                getCurentLocation();
            }
            signalRService.invoke('AddToGroup',busId.toString()).catch((err)=>{console.log(err)});
            signalRService.on("CheckAddedGroup",(res)=>{
                console.log(res);
            })
        })
        .catch((err)=>{console.log("Kết nối tới hub thất bại (MAP): "+err)});

        
        
        return ()=>{
            Geolocation.clearWatch(watchId);
            //signalRService.stop().then(()=>{console.log("Đã ngắt kết nối hub")});
        }
    },[])

    return (
        <View style={styles.map_container}>
            <MapView
                style={styles.map_view}
                provider = {PROVIDER_GOOGLE}
                region={myLocation}
                mapType="standard"
                zoomEnabled={true}
                pitchEnabled={true}
                showsUserLocation={true}
                followsUserLocation={true}
                showsCompass={true}
                showsBuildings={true}
                showsTraffic={true}
                showsIndoors={true}
            >
                {stops.map((stop,index)=>(
                    <MapView.Marker
                        coordinate={stop.coordinate}
                        key={index}
                        title={stop.title}>

                    </MapView.Marker>
                ))}
                
            </MapView>
        </View>
        
    )
}

const styles = StyleSheet.create({
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
    }
})
export default RouteMapComponent;