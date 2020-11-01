import React, { useEffect, useState,useRef } from 'react';
import {StyleSheet, TouchableOpacity,Text} from 'react-native';
import { FlatList } from 'react-native-gesture-handler';
import StudentItem from '../Layout/StudentItem';
import Ionicons from 'react-native-vector-icons/Ionicons';
import StudentService from '../../../controllers/StudentService';
import { useSelector } from 'react-redux';
import SignalRService from '../../../controllers/SignalRService';
import moment from 'moment';
import * as _ from 'lodash';

const RouteComponent = (props)=>{
    //======================== Property =========================
    const navigation = props.navigation;
    var typeCheck = props.typeCheck;
    var user = useSelector((state)=>state.user);
    var listStudent = [];
    const stateRef = useRef();

    //=========================  State ==========================
    const [students, setListStudent]=useState(listStudent)
    stateRef.current = students;
    const [bus, setBus] = useState({
        busId: null,
        busName: null
    });
    
    // ======================= Function =====================
    // Lấy tất cả danh sách học sinh trên tuyến đi
    useEffect(()=>{
        async function getListStudent(){
            var response = await StudentService.getAllStudentOfMonitor(user.userId,user.userToken);
            setListStudent(response.result);
            setBus({
                busId: response.result[0].busId,
                busName: response.result[0].busName
            })
        }
        getListStudent();
        // const unsubscribe = navigation.addListener('focus', () => {
        //     // Lấy lại list student mới khi focus tab
            
        //   });
        //   // Gắn sự kiện khi unmount
        //   return unsubscribe;
    },[])

    // Kết nối tới hub theo dõi trạng thái HS
    useEffect(()=>{
        var signalRService = SignalRService(user.userToken);
        signalRService.start()
        .then(()=>{console.log("Kết nối tới hub thành công");})
        .catch((err)=>{console.log("Kết nối tới hub thất bại: "+err)});
        
        signalRService.on('ReceiveCheckIn',res=>{
            if(res.statusCode=='B002'){
                var checkIn = res.result;
                let listStudent = stateRef.current;
                let newListStudent = [];
                _.forEach(listStudent,(item,index)=>{
                    if(item.id==checkIn.studentId){
                        item.status = checkIn.checkInResult
                    }
                    newListStudent.push(item);
                });
                setListStudent(newListStudent);
            }
        })

        return ()=>{
            signalRService.stop().then(()=>{console.log("Đã ngắt kết nối hub")})
        }
    },[])

    // Config header component
    React.useLayoutEffect(()=>{
        navigation.setOptions({
            headerRight: ()=>(
                <TouchableOpacity 
                    style={styles.headerRight} 
                    onPress={()=>navigation.navigate('RouteMap',{
                        busId: bus.busId
                    })}
                >
                    <Ionicons name="map" color={'#FFF'} size={26} />
                </TouchableOpacity>
            ),
            headerLeft: ()=>(
                <Text style={styles.headerLeft}>{bus.busName}</Text>
            ),
            title: typeCheck==0? "Lượt đi" : "Lượt về"
        },[navigation])
    })

    return (
        <FlatList 
            style={styles.container}
            data={students}
            renderItem={({item})=>
                <StudentItem studentData={item} router={navigation} typeCheck={typeCheck}/>
            }
            keyExtractor={item=>item.id.toString()}
        >
        </FlatList>
    )
}

const styles = StyleSheet.create({
    container: {
        width: "100%",
        height: 500,
        backgroundColor: '#e3dfde'
    },
    headerRight: {
        width: 50,
        height: '100%',
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'center'
    },
    headerLeft: {
        marginLeft: 8,
        color: '#FFF',
        fontSize: 16,
        fontWeight: "bold"
    }
})

export default RouteComponent