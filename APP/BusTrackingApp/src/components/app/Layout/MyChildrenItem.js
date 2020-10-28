import React, { useEffect, useState } from 'react';
import {View,Text,Image,StyleSheet,TouchableOpacity, Button, Alert} from 'react-native';
import { useSelector } from 'react-redux';
import StudentService from '../../../controllers/StudentService';
import moment from 'moment';
import Ionicons from 'react-native-vector-icons/Ionicons';

function MyChildrenItem (props) {

    // ================== Property ===============
    const { studentData }= props;
    const router = props.router;
    const parent = useSelector((state)=>state.user);

    // ================== State ==================
    const [student,setStudent] = useState(studentData);

    // ================= Function ================

    // Load lại giao diện Item khi data change
    useEffect(()=>{
        setStudent(studentData);
    },[studentData])

    // Xin nghỉ cho học sinh
    const checkIn = async (checkInResult) =>{
        var dateCheck = moment().format("YYYY-MM-DDTHH:mm:ss");
        var res = await StudentService.checkIn(student.id,student.monitorId,2,2,dateCheck,checkInResult,parent.userToken)
        if(res.statusCode=='B002'){
            setStudent({
                ...student,
                status: res.result.checkInResult
            })
            if(res.result.checkInResult==8){
                Alert.alert('Xin nghỉ học thành công');
            }else if(res.result.checkInResult==7){
                Alert.alert('Xác nhận đón con thành công');
            }
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
                        <Text style={{fontSize:16,fontWeight:'bold'}}>
                            {student.name}
                        </Text>
                    </TouchableOpacity>
                    <Text style={{fontSize:16}}>Lớp: {student.classOfStudent}</Text>
                    <Text style={{fontSize:16}}>Trạng thái: { student.status == 1  ? 'Vắng mặt lúc đón' : 
                            student.status == 2  ? 'Đã đón' : 
                            student.status == 3  ? 'Đã tới trường' :
                            student.status == 4  ? 'Vắng mặt lúc về' :
                            student.status == 5  ? 'Đang trên đường về' :
                            student.status == 6  ? 'Đã trả' :
                            student.status == 7  ? 'Đã về nhà' :
                            student.status == 8  ? 'Nghỉ học' : ''
                        }
                    </Text>
                    
                </View>
                <View style={styles.action}>
                        <TouchableOpacity 
                            style={[styles.container_action,(student.status==7 | student.status==8) ? styles.blur : styles.notBlur]}
                            onPress={()=>{Alert.show("Bản đồ")}}
                            disabled={student.status==8 | student.status==7}
                            >
                            <Ionicons name="locate-outline" style={styles.action_icon} />
                            <Text style={{color:'#FF9800'}}>Theo dõi xe</Text>
                        </TouchableOpacity>
                        <TouchableOpacity 
                            style={[styles.container_action,(student.status!=0 | student.status==8) ? styles.blur : styles.notBlur]}
                            onPress={()=>{checkIn(8)}}
                            disabled={student.status!=0 | student.status==8}
                            >
                            <Ionicons name="thermometer-outline" style={[styles.action_icon,{color:'red',borderColor:'red'}]} />
                            <Text style={{color:'red'}}>Xin nghỉ</Text>
                        </TouchableOpacity>
                        <TouchableOpacity 
                            style={[styles.container_action,(student.status!=6) ? styles.blur : styles.notBlur]}
                            onPress={()=>{checkIn(7)}}
                            disabled={student.status!=6}
                            >
                            <Ionicons name="home-outline" style={[styles.action_icon,{color:'blue',borderColor:'blue'}]} />
                            <Text style={{color:'blue'}}>Đã đón con</Text>
                        </TouchableOpacity>
                    </View>
              </View>
        </View>
    );
}


const styles = StyleSheet.create({
    container: {
        flexDirection: 'row',
        height: 145,
        width: "100%",
        backgroundColor: "#FFF",
        marginBottom: 8,
        alignItems: "center"
    },
    content: {
        flexDirection:'column',
        flexGrow: 1,
        paddingLeft: 10
    },
    profile:{
        flexDirection: 'column',
        marginBottom: 8,
    },
    action:{
        display: "flex",
        flexDirection: 'row',
        justifyContent: "space-between",
        paddingRight: 25
    },
    container_action: {
        flexDirection: 'column',
        alignItems: 'center'
    },
    action_icon: {
        fontSize: 25,
        borderRadius: 50,
        borderWidth: 2,
        borderColor: '#FFC107',
        color: '#FFC107',
        textAlign: 'center',
        paddingTop: 4,
        marginTop: 8,
        width: 35,
        height: 35
    },
    avartar: {
        width: 55,
        height: 55,
        marginTop: "auto",
        marginBottom: "auto",
        marginLeft: 5,
        marginRight: 5,
        borderRightWidth: 2,
        overflow: "hidden",
        borderColor: "#9c9c9c",
    },
    blur: {
        opacity: 0.2
    },
    notBlur: {
        opacity: 1
    }
})

export default MyChildrenItem;