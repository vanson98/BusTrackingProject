import React, { useState } from 'react';
import {View, Text, Button, StyleSheet} from 'react-native';
import { ScrollView, TouchableOpacity } from 'react-native-gesture-handler';
import DatePicker from 'react-native-datepicker';
import Ionicons from 'react-native-vector-icons/Ionicons';

const NotificationComponent = () => {

  const [date,setDate]=useState({
    fromDate: Date.now(),
    toDate: Date.now()
  });

  return (
    <View style={styles.container}>
      <View style={styles.time_box}>
        <DatePicker
          mode='date'
          date={date.fromDate}
          placeholder='Chọn ngày bắt đầu'
          format='DD-MM-YYYY'
          customStyles={{
            dateIcon: {
              position: 'absolute',
              left: 0,
              top: 4,
              marginLeft: 0
            },
            dateInput: {
              marginLeft: 36,
              borderRadius: 50,
              height: 30
            }
          }}
          onDateChange={(val) => {setDate({...date,fromDate: val})}}
          />
           <DatePicker
          mode='date'
          date={date.toDate}
          placeholder='Chọn ngày bắt đầu'
          format='DD-MM-YYYY'
          customStyles={{
            dateIcon: {
              position: 'absolute',
              left: 0,
              top: 4,
              marginLeft: 0
            },
            dateInput: {
              marginLeft: 36,
              borderRadius: 50,
              height: 30
            }
          }}
          onDateChange={(val) => {setDate({...date,toDate: val})}}
          />
          <TouchableOpacity>
            <Ionicons name="search" size={20} color={'#FFF'} />
          </TouchableOpacity>
      </View>
      <ScrollView>

      </ScrollView>
    </View>
  );
};

export const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: 'column',
    backgroundColor: 'green'
  },
  time_box: {
    flexDirection: 'row',
    justifyContent:'space-around',
    alignItems: "center",
    width: '100%',
    height: 50,
    backgroundColor: '#6fe89e'
  }
})
export default NotificationComponent