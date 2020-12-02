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
    const teacher = useSelector((state)=>state.user);

    // ================== State ==================
    const [student,setStudent] = useState(studentData);

    // ================= Function ================

    // Load lại giao diện Item khi data change
    useEffect(()=>{
        setStudent(studentData);
    },[studentData])

    // Xác nhận học sinh đã tới lớp
    const checkIn = async (checkInResult) =>{
        var dateCheck = moment().subtract(10,'second').format("YYYY-MM-DDTHH:mm:ss");
        var res = await StudentService.checkIn(student.id,student.monitorId,0,0,0,dateCheck,checkInResult,teacher.userToken)
        console.log(res);
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
                            student.status == 8  ? 'Nghỉ học' : 
                            student.status == 9 ? 'Đã tới lớp' :
                            student.status == 10 ? 'Không có trong lớp' : ''
                        }
                    </Text>
                </View>
                <View style={styles.action}>
                        <TouchableOpacity 
                            style={[styles.container_action,(student.status==2 | student.status==5) ? styles.notBlur : styles.blur ]}
                            onPress={()=>router.navigate('RouteMap',{
                                monitorId: student.monitorId
                            })}
                            disabled={!(student.status==2 | student.status==5)}
                            >
                            <Ionicons name="locate-outline" style={styles.action_icon} />
                            <Text style={{color:'#FF9800'}}>Theo dõi xe</Text>
                        </TouchableOpacity>
                        <TouchableOpacity 
                            style={[styles.container_action,(student.status!=3) ? styles.blur : styles.notBlur]}
                            onPress={()=>{checkIn(9)}}
                            disabled={student.status!=3}
                            >
                            <Ionicons name="log-in-outline" style={[styles.action_icon,{color:'red',borderColor:'red'}]} />
                            <Text style={{color:'red'}}>Đã tới lớp</Text>
                        </TouchableOpacity>
                        <TouchableOpacity 
                            style={[styles.container_action,(student.status!=3 | student.status==9) ? styles.blur : styles.notBlur]}
                            onPress={()=>{checkIn(10)}}
                            disabled={student.status!=3 | student.status==9}
                            >
                            <Ionicons name="megaphone-outline" style={[styles.action_icon,{color:'blue',borderColor:'blue'}]} />
                            <Text style={{color:'blue'}}>Văng mặt</Text>
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