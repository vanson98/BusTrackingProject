import React, { useState,useEffect } from 'react';
import {View, Text, Button, Image, StyleSheet,Linking} from 'react-native';
import { ScrollView, TouchableOpacity} from 'react-native-gesture-handler';
import moment from 'moment';
import Ionicons from 'react-native-vector-icons/Ionicons';

const DetailStudentComponent = ({route}) => {
  const {studentData} = route.params
  const [student,setStudent] = useState(studentData);

  // useEffect(()=>{
  //   setStudent(studentData);
  // },[studentData])
  function callPhone(phoneNumber){
    Linking.openURL(`tel:${phoneNumber}`);
  }

  return (
    <ScrollView>
      <View style={styles.container}>
        <View style={styles.ct_avartar}>
          <Image
            source={require('../../../share/assets/image/student.png')}
            style={styles.avartar}
          />
        </View>
        <View >
          <View style={styles.ct_lable}>
            <Text style={styles.label}>THÔNG TIN HỌC SINH</Text>
          </View>
          <View style={styles.sub_content}>
            <Text style={styles.text}>Họ và tên: {student.name}</Text>
            <Text style={styles.text}>Ngày sinh: {moment(student.dob).format("DD/MM/YYYY")}</Text>
            <View style={styles.phone_container}>
              <Text style={[styles.text,styles.phone_text]}>Số điện thoại: {student.phoneNumber}</Text>
              <TouchableOpacity
                onPress={()=>{callPhone(student.phoneNumber)}}>
                <Ionicons name="call-outline" style={styles.phone_icon}></Ionicons>
              </TouchableOpacity>
            </View>
            <Text style={styles.text}>Địa chỉ: {student.address} </Text>
            <Text style={styles.text}>Email: {student.email}</Text>
            <Text style={styles.text}>Lớp: {student.classOfStudent}</Text>
            <Text style={styles.text}>Xe: {student.busName}</Text>
            <Text style={styles.text}>Địa chỉ điểm dừng: {student.stopAddress}</Text>
          </View>
          <View style={styles.ct_lable}>
            <Text style={styles.label}>THÔNG TIN PHỤ HUYNH</Text>
          </View>
          <View style={styles.sub_content}>
            <Text style={styles.text}>Họ và tên: {student.parentName}</Text>
            <View style={styles.phone_container}>
              <Text style={[styles.text,styles.phone_text]}>Số điện thoại: {student.phoneParent}</Text>
              <TouchableOpacity
                onPress={()=>{callPhone(student.phoneParent)}}>
                <Ionicons name="call-outline" style={styles.phone_icon}></Ionicons>
              </TouchableOpacity>
            </View>
          </View>
          <View style={styles.ct_lable}>
            <Text style={styles.label}>THÔNG TIN GIÁM SÁT XE</Text>
          </View>
          <View style={styles.sub_content}>
            <Text style={styles.text}>Họ và tên: {student.monitorName}</Text>
            <View style={styles.phone_container}>
              <Text style={[styles.text,styles.phone_text]}>Số điện thoại: {student.phoneMonitor}</Text>
              <TouchableOpacity
                onPress={()=>{callPhone(student.phoneMonitor)}}>
                <Ionicons name="call-outline" style={styles.phone_icon}></Ionicons>
              </TouchableOpacity>
            </View>
          </View>
          <View style={styles.ct_lable}>
            <Text style={styles.label}>THÔNG TIN GVCN</Text>
          </View>
          <View style={styles.sub_content}>
            <Text style={styles.text}>Họ và tên: {student.teacherName}</Text>
            <View style={styles.phone_container}>
              <Text style={[styles.text,styles.phone_text]}>Số điện thoại: {student.phoneTeacher}</Text>
              <TouchableOpacity
                onPress={()=>{callPhone(student.phoneTeacher)}}>
                <Ionicons name="call-outline" style={styles.phone_icon}></Ionicons>
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </View>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flexDirection: 'column',
    flex: 0,
    height: 'auto',
    alignItems: 'stretch',
    paddingLeft: 10,
    paddingRight: 10,
  },
  ct_avartar: {
    height: 130,
    justifyContent: 'center',
    alignItems: 'center',
  },
  avartar: {
    width: 100,
    height: 100,
  },
  ct_lable: {
    height: 40,
    justifyContent: 'center',
    backgroundColor: '#FF9800',
    borderRadius: 10,
    paddingLeft: 10,
  },
  label: {
    color: '#FFF',
  },
  text: {
    fontSize: 15,
    marginBottom: 10,
  },
  sub_content: {
    paddingLeft: 10,
    marginTop: 10,
    marginBottom: 10,
  },
  phone_container: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  phone_text:{
    flexGrow: 1,
  },
  phone_icon:{
    width: 25,
    height: 25,
    fontSize: 20,
    color: '#FF9800'
  }
});

export default DetailStudentComponent;
