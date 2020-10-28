import React, { useEffect, useState,useRef } from 'react';
import {StyleSheet} from 'react-native';
import { FlatList } from 'react-native-gesture-handler';
import MyChildrenItem from '../Layout/MyChildrenItem';
import StudentService from '../../../controllers/StudentService';
import { useSelector } from 'react-redux';
import SignalRService from '../../../controllers/SignalRService';
import * as _ from 'lodash';

const MyChildrenComponent = (props)=>{
    //======================== Property =========================
    const navigation = props.navigation;
    var user = useSelector((state)=>state.user);
    var listStudent = [];
    const stateRef = useRef();

    //=========================  State ==========================
    const [students, setListStudent]=useState(listStudent)
    stateRef.current = students;

    // Lấy tất cả danh sách học sinh
    useEffect(()=>{
        async function getListStudent(){
            var response = await StudentService.getAllChildOfParent(user.userId,user.userToken);
            setListStudent(response.result)
        }
        getListStudent();
    },[])

    // Kết nối tới hub khi khởi tạo component
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
    },[])
    

    return (
        <FlatList 
            style={styles.container}
            data={students}
            renderItem={({item})=>
                <MyChildrenItem studentData={item} router={navigation}/>
            }
            keyExtractor={item=>item.id.toString()}
        >
        </FlatList>
    )
}

const styles = StyleSheet.create({
    container: {
        paddingTop: 10,
        padding: 10,
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
    }
})

export default MyChildrenComponent