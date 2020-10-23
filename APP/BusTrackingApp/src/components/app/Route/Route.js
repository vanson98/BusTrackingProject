import React, { useEffect, useState } from 'react';
import {StyleSheet, TouchableOpacity} from 'react-native';
import { FlatList } from 'react-native-gesture-handler';
import StudentItem from '../Layout/StudentItem';
import Ionicons from 'react-native-vector-icons/Ionicons';
import StudentService from '../../../controllers/StudentService';
import { useSelector } from 'react-redux';
import { useFocusEffect } from '@react-navigation/native';

const RouteComponent = (props)=>{
    //======================== Property =========================
    const navigation = props.navigation;
    var typeCheck = props.typeCheck;
    var user = useSelector((state)=>state.user);
    var listStudent = [];
   
    //=========================  State ==========================
    const [students, setListStudent]=useState(listStudent)

    // Lấy tất cả danh sách học sinh trên tuyến đi
    useEffect(()=>{
        const unsubscribe = navigation.addListener('focus', () => {
            // Lấy lại list student mới khi focus tab
            async function getListStudent(){
                var response = await StudentService.getAllStudentByMonitor(user.userId,user.userToken);
                setListStudent(response.result)
            }
            getListStudent();
          });
          // Gắn sự kiện khi kết thúc navigation 
          return unsubscribe;
    },[navigation])

    React.useLayoutEffect(()=>{
        navigation.setOptions({
            headerRight: ()=>(
                <TouchableOpacity style={styles.headerRight}>
                    <Ionicons name="map" color={'#FFF'} size={26} />
                </TouchableOpacity>
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
        // <View style={{flex:1,alignItems:'center',justifyContent:'center'}}>
        //     <Text>Các lượt đi</Text>
        //     <Button 
        //         title="Go to Details Round"
        //         onPress={()=>navigation.navigate("RouteMap")}/>
        // </View> 
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
    }
})

export default RouteComponent