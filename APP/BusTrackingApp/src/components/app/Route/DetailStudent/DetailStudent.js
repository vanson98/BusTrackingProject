import React from 'react';
import {View, Text, Button, Image, StyleSheet} from 'react-native';
import { ScrollView} from 'react-native-gesture-handler';

const DetailStudentComponent = () => {
  return (
    <ScrollView>
      <View style={styles.container}>
        <View style={styles.ct_avartar}>
          <Image
            source={require('../../../../share/assets/image/student.png')}
            style={styles.avartar}
          />
        </View>
        <View >
          <View style={styles.ct_lable}>
            <Text style={styles.label}>THÔNG TIN HỌC SINH</Text>
          </View>
          <View style={styles.sub_content}>
            <Text style={styles.text}>Họ và tên: Nguyễn Văn Sơn</Text>
            <Text style={styles.text}>Ngày sinh: 15/9/1998</Text>
            <Text style={styles.text}>Lớp: 16A04</Text>
            <Text style={styles.text}>
              Địa chỉ: Ngõ 274 Phố Định Công Hoàng Mai Hà Nội
            </Text>
          </View>
          <View style={styles.ct_lable}>
            <Text style={styles.label}>THÔNG TIN PHỤ HUYNH</Text>
          </View>
          <View style={styles.sub_content}>
            <Text style={styles.text}>Họ và tên: Nguyễn Văn Tuấn</Text>
            <Text style={styles.text}>Số điện thoại:0362594736</Text>
          </View>
          <View style={styles.ct_lable}>
            <Text style={styles.label}>THÔNG TIN GVCN</Text>
          </View>
          <View style={styles.sub_content}>
            <Text style={styles.text}>Họ và tên: Nguyễn Thúy Lan</Text>
            <Text style={styles.text}>Số điện thoại:0362594736</Text>
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
});

export default DetailStudentComponent;
