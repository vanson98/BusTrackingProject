import React, { useEffect, useState } from 'react';
import {View,Text,Image,StyleSheet,TouchableOpacity, PermissionsAndroid, Alert} from 'react-native';
import Geolocation from '@react-native-community/geolocation';
import { useSelector } from 'react-redux';
import StudentService from '../../../controllers/StudentService';
import moment from 'moment';


function StudentItem (props) {
    const { studentData }= props;
    const router = props.router;
    const { typeCheck } = props;
    const monitor = useSelector((state)=>state.user);
    // State
    const [student,setStudent] = useState(studentData);
    // Effect - Render lại component khi prop change
    useEffect(()=>{
        setStudent(studentData);
    },[studentData])

    // Lấy tọa độ hiện tại (Chỉ cho monitor)
    const checkIn = async (checkInResult)=>{
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
               
                Geolocation.getCurrentPosition(
                    (position)=>{
                        var lat = parseFloat(position.coords.latitude);
                        var long = parseFloat(position.coords.longitude);
                        sendCheckIn(long,lat,checkInResult);
                    },
                    (error)=>{
                        Alert.alert(JSON.stringify(error))
                    },
                    { enableHighAccuracy: false, timeout: 20000}
                )
                
            } else {
              console.log("Quyền truy cập vị trí bị từ chối");
            }
        } catch (err) {
            console.warn(err);
        }
    }

    const sendCheckIn = async (long,lat,checkInResult) =>{
        var dateCheck = moment().subtract(10,'second').format("YYYY-MM-DDTHH:mm:ss");
        var res = await StudentService.checkIn(student.id,monitor.userId,long,lat,typeCheck,dateCheck,checkInResult,monitor.userToken)
        if(res.statusCode=='B002'){
            setStudent({
                ...student,
                status: res.result.checkInResult
            })
        }else{
            Alert.show('Đã có lỗi xảy ra');
        }
    }

    return (
        <View style={styles.container}>
            <TouchableOpacity onPress={()=>router.navigate('DetailStudent',{
                        studentData: studentData
            })}>
                <Image 
                    source={require("../../../share/assets/image/student.png")} 
                    style={styles.avartar}
                />
            </TouchableOpacity>
            <View style={styles.content}>
                <View style={styles.profile}>
                    <TouchableOpacity onPress={()=>router.navigate('DetailStudent',{
                        studentData: studentData
                    })}>
                        <Text style={{fontSize:18,fontWeight:'bold'}}>
                            {student.name}
                        </Text>
                    </TouchableOpacity>
                    <Text style={{fontSize: 16}}>{student.classOfStudent}</Text>
                    <Text style={{marginTop:10,fontSize:16}}>
                        {   
                            (student.status == 1  ) ? 'Vắng mặt lúc đón' : 
                            (student.status == 2 && typeCheck==0 ) ? 'Đã đón' : 
                            ((student.status == 3 |student.status == 4 | student.status ==5 | student.status ==6 | student.status ==7 ) && typeCheck==0) ? 'Đã tới trường' :
                            (student.status == 4 && typeCheck!=0 ) ? 'Vắng mặt lúc về' :
                            (student.status == 5 && typeCheck!=0 ) ? 'Đang trên đường về' :
                            (student.status == 6 && typeCheck!=0 ) ? 'Đã trả' :
                            (student.status == 7 && typeCheck!=0 ) ? 'Đã về nhà' :
                            student.status == 8 ? 'Nghỉ học' : ''
                        }
                    </Text>
                </View>
                <View style={styles.action}>
                    <TouchableOpacity 
                        style={[(typeCheck==0 && student.status==0) ? styles.show : styles.hide, {}]}
                        onPress={()=>{checkIn(2)}}
                        >
                        <Text style={{color: '#FFF',fontSize:16}}>Đã đón</Text>
                    </TouchableOpacity>
                    <TouchableOpacity 
                        style={[(typeCheck==0 && student.status==0) ? styles.show2 : styles.hide]}
                        onPress={()=>{checkIn(1)}}>
                        <Text style={{color: '#FFF',fontSize:16}}>Vắng mặt</Text>
                    </TouchableOpacity> 
                    <TouchableOpacity 
                        style={[(typeCheck==0 && student.status==2) ? styles.show3 : styles.hide]}
                        onPress={()=>{checkIn(3)}}>
                        <Text style={{color: '#FFF',fontSize:16}}>Đã tới trường</Text>
                    </TouchableOpacity> 
                    
                    <TouchableOpacity 
                        style={[(typeCheck!=0 && student.status==5) ? styles.show : styles.hide]}
                        onPress={()=>{checkIn(6)}}>
                        <Text style={{color: '#FFF',fontSize:16}}>Đã trả</Text>
                    </TouchableOpacity>
                    <TouchableOpacity 
                        style={[(typeCheck!=0 && student.status==3) ? styles.show3 : styles.hide]}
                        onPress={()=>{checkIn(5)}}>
                        <Text style={{color: '#FFF',fontSize:16}}>Đã lên xe</Text>
                    </TouchableOpacity>
                    <TouchableOpacity 
                        style={[(typeCheck!=0 && student.status==3) ? styles.show2 : styles.hide]}
                        onPress={()=>{checkIn(4)}}>
                        <Text style={{color: '#FFF',fontSize:16}}>Vắng mặt</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </View>
    );
}


const styles = StyleSheet.create({
    container: {
        flexDirection: 'row',
        height: 120,
        width: "100%",
        backgroundColor: "#FFF",
        marginBottom: 8,
        alignItems: "center",

    },
    content: {
        flexDirection:'row',
        paddingRight: 65,
        flexGrow: 1
    },
    profile:{
        flexDirection: 'column',
        flexGrow: 1,
        height: 90,
        borderLeftWidth: 1,
        borderColor: '#d1d1d1',
        paddingLeft:10,
    },
    action:{
        display: "flex",
        flexDirection: 'column',
        width:100,
        justifyContent: "space-around",
        
    },
    avartar: {
        width: 50,
        height: 50,
        marginTop: "auto",
        marginBottom: "auto",
        marginLeft: 5,
        marginRight: 5,
        borderRightWidth: 2,
        overflow: "hidden",
        borderColor: "#9c9c9c",
    },
    hide: {
        display: 'none'
    },
    show: {
        width: 100,
        height: 35,
        display: 'flex',
        backgroundColor: '#FF9800',
        justifyContent: 'center',
        alignItems: 'center',
        borderRadius: 10
    },
    show2: {
        width: 100,
        height: 35,
        display: 'flex',
        backgroundColor: '#F44336',
        justifyContent: 'center',
        alignItems: 'center',
        borderRadius: 10
    },
    show3: {
        width: 100,
        height: 35,
        display: 'flex',
        backgroundColor: '#FF9800',
        justifyContent: 'center',
        alignItems: 'center',
        borderRadius: 10
    }
    
})

export default StudentItem;