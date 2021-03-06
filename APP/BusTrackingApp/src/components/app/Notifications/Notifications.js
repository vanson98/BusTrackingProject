import React, { useEffect, useState,useRef } from 'react';
import {View, Text, Button, StyleSheet,FlatList, ScrollView, TouchableOpacity } from 'react-native';
import { TextInput } from 'react-native-gesture-handler';
import DateTimePickerModal from "react-native-modal-datetime-picker";
import Ionicons from 'react-native-vector-icons/Ionicons';
import { useSelector } from 'react-redux';
import StudentService from '../../../controllers/StudentService';
import NotificationItem from '../Layout/NotificationItem';
import moment from 'moment';
import SignalRService from '../../../controllers/SignalRService';
const NotificationsComponent = (props) => {
  // ================= prop =================
  var user = useSelector((state)=>state.user);
  const navigation = props.navigation;
  const currentDate = new Date();
  const fiveDayAgo = new Date(); 
  fiveDayAgo.setDate(currentDate.getDate()-5);
  const stateRef = useRef();

  // ================== State ===============

  // Giá trị khởi tạo cho state chỉ được gán lần đầu tiên, từ lần load thứ 2 thì sẽ bị bỏ qua
  const [notications,setNotify] = useState({
    listNotify: []
  });

  stateRef.current = notications.listNotify;

  const [date,setDate]=useState({
    fromDate: fiveDayAgo,
    toDate: currentDate
  });

  const [datePickerVisible, setDatePickerVisibility] = useState({
    visibleFromDate: false,
    visibleToDate: false,
  });

  // ================== Function =====================
  const getNotification = async ()=>{
    var fromDate = date.fromDate.toISOString();
    var toDate = date.toDate.toISOString();
    var res;
    if(user.roles[0]=='monitor'){
      res = await StudentService.getAllNotificationOfMonitor(user.userId,fromDate,toDate,user.userToken)
    }else if(user.roles[0]=='parent'){
      res = await StudentService.getAllNotificationOfParent(user.userId,fromDate,toDate,user.userToken)
    }else{
      res = await StudentService.getAllNotificationOfTeacher(user.userId,fromDate,toDate,user.userToken)
    }
    setNotify({
      listNotify: res.result
    })
  }

  // Lấy lại danh sách thông báo khi thời gian thay đổi
  useEffect(()=>{
    getNotification();
  },[date])

  // Lấy tất cả danh sách học sinh trên tuyến đi
  useEffect(()=>{
    const unsubscribe = navigation.addListener('focus', () => {
        getNotification();
      });
      // Gắn sự kiện khi kết thúc navigation 
      return unsubscribe;
  },[navigation])

  // Kết nối tới hub khi khởi tạo component
  useEffect(()=>{
    var signalRService = SignalRService(user.userToken);
    signalRService.start()
    .then(()=>{console.log("Kết nối tới hub thành công");})
    .catch((err)=>{console.log("Kết nối tới hub thất bại: "+err)});
    
    signalRService.on('ReceiveNotication',data=>{
      console.log(data);
      let listNotify = stateRef.current;
      listNotify.unshift(data);
      setNotify({
        listNotify: listNotify
      })
    })
  },[])
  

  const showDatePicker = (type) => {
    if(type==0){
      setDatePickerVisibility({
        ...datePickerVisible,
        visibleFromDate: true
      }); 
    }else{
      setDatePickerVisibility({
        ...datePickerVisible,
        visibleToDate: true
      });
    }
    
  };

  const hideDatePicker = () => {
    setDatePickerVisibility(false);
  };

  const setFromDate = (dateInput) => {
    setDate({
      ...date,
      fromDate: new Date(dateInput)
    })
    hideDatePicker();
  };

  const setToDate = (dateInput) => {
    setDate({
      ...date,
      toDate: new Date(dateInput)
    })
    hideDatePicker();
  };

  return (
    <View style={styles.container}>
      <View style={styles.time_container}>
          <View style={styles.time_box}>
            <Ionicons style={styles.time_icon} name='calendar-outline' onPress={()=>{showDatePicker(0)}}></Ionicons>
            <TextInput style={styles.date_input} placeholder="Từ ngày" 
              value={moment(date.fromDate.toISOString()).format("DD/MM/YYYY")} editable = {false}></TextInput>
            <DateTimePickerModal
              isVisible={datePickerVisible.visibleFromDate}
              mode="date"
              onConfirm={setFromDate}
              onCancel={hideDatePicker}
            />
          </View>
          <Ionicons name='arrow-forward-outline' style={{fontSize:25,color:"#FFF"}}></Ionicons>
          <View style={styles.time_box}>
            <Ionicons style={styles.time_icon} name='calendar-outline' onPress={()=>{showDatePicker(1)}}></Ionicons>
            <TextInput style={styles.date_input} placeholder="Đến ngày" 
              value={moment(date.toDate.toISOString()).format("DD/MM/YYYY")} editable = {false}></TextInput>
            <DateTimePickerModal
              isVisible={datePickerVisible.visibleToDate}
              mode="date"
              onConfirm={setToDate}
              onCancel={hideDatePicker}
            />
          </View>
          {/* <TouchableOpacity onPress={()=>{getNotification()}}>
            <Ionicons name="search" size={20} color={'#FFF'} />
          </TouchableOpacity> */}
      </View>
      <FlatList
        data={notications.listNotify}
        renderItem={({item})=>(
          <NotificationItem message={item}></NotificationItem>
        )}
        keyExtractor={item=>item.id.toString()}>
      </FlatList>
    </View>
  );
};

export const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: 'column',
    backgroundColor: '#FFF'
  },
  time_container: {
    flexDirection: 'row',
    justifyContent:'space-around',
    alignItems: "center",
    width: '100%',
    height: 50,
    backgroundColor: '#FFC107'
  },
  time_box: {
    flexDirection: 'row',
    alignItems: 'center',
    width: 150
  },
  date_input: {
    width: 100,
    height: 35,
    borderWidth: 2,
    borderColor: "#FFF",
    backgroundColor: '#FFF',
    borderRadius: 20,
    color: '#000',
    paddingLeft: 10,
    
  },
  time_icon: {
    height: 30,
    width: 30,
    fontSize: 30,marginRight:10,
    color: "#FFF"
  }
})
export default NotificationsComponent