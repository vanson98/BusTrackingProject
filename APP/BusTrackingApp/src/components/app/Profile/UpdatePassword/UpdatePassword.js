import React, {useState } from "react"
import {View,Text, StyleSheet, TextInput} from 'react-native';
import {useSelector} from 'react-redux';
import { TouchableOpacity } from "react-native-gesture-handler";
import { useDispatch } from 'react-redux';
import { updatePassword } from '../../../../actions/UserActions';

export default function UpdatePassword(props) {
    const user = useSelector((state)=>state.user)
    const navigator = props.navigation;
    const dispatch = useDispatch();
    const [userUpdate,setUserUpdate]=useState({
        currentPass: null,
        newPass: null,
        renewPass: null,
        userValid: false
    })

    const update = () => {
        dispatch(updatePassword(user.userId,userUpdate.currentPass,userUpdate.newPass,user.userToken))
    }

    const oldPassChange = (val) => {
        if (val.length !== 0) {
          setUserUpdate({
            ...userUpdate,
            currentPass: val,
            userValid: true,
          });
        } else {
          setUserUpdate({
            ...userUpdate,
            currentPass: val,
            userValid: false,
          });
        }
    };

    const newPassChange = (val) => {
    if (val.length !== 0) {
        setUserUpdate({
            ...userUpdate,
            newPass: val,
            userValid: true,
        });
    } else {
        setUserUpdate({
            ...userUpdate,
            newPass: val,
            userValid: false,
        });
    }
    };

    const rePassChange = (val) => {
        if (val.length !== 0) {
            setUserUpdate({
                ...userUpdate,
                renewPass: val,
                userValid: true,
            });
        } else {
            setUserUpdate({
                ...userUpdate,
                renewPass: val,
                userValid: false,
            });
        }
        };
    
  return (
    <View style={styles.container}>
      <View style={styles.field_container}>
        <Text style={styles.lable}>Mật khẩu cũ: </Text>
        <TextInput
          value={userUpdate.email}
          style={styles.textInput}
          secureTextEntry={true}
          onChangeText={(val) => oldPassChange(val)}
        />
      </View>
      <View style={styles.field_container}>
        <Text style={styles.lable}>Mật khẩu mới: </Text>
        <TextInput
          value={userUpdate.fullName}
          style={styles.textInput}
          secureTextEntry={true}
          onChangeText={(val) => newPassChange(val)}
        />
      </View>
      <View style={styles.field_container}>
        <Text style={styles.lable}>Nhập lại mật khẩu: </Text>
        <TextInput
          value={userUpdate.phoneNumber}
          secureTextEntry={true}
          style={styles.textInput}
          onChangeText={(val) => rePassChange(val)}
        />
      </View>
      <View style={styles.button_container}>
        <TouchableOpacity
          style={[styles.button, {backgroundColor: 'grey'}]}
          onPress={() => navigator.goBack()}>
          <Text style={{fontSize: 18, color: '#FFF'}}>Hủy</Text>
        </TouchableOpacity>
        <TouchableOpacity
          style={[
            styles.button,
            !userUpdate.userValid ? styles.blur : styles.notBlur,
          ]}
          disabled={!userUpdate.userValid}
          onPress={() => {
            update();
          }}>
          <Text style={{fontSize: 18, color: '#FFF'}}>Cập nhật</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
}


const styles = StyleSheet.create({
    container: {
        flexDirection: 'column',
        padding: 15
    },
    field_container: {
        flexDirection: 'row',
        alignItems: 'center',
        marginTop: 10
    },
    button_container: {
        flexDirection: 'row',
        justifyContent: 'space-around',
        marginTop: 40
    },
    textInput: {
        height: 40, 
        borderColor: 'gray', 
        borderWidth: 1,
        flexGrow: 1,
        height: 40,
        paddingLeft: 15,
        borderRadius: 20
    },
    lable: {
        width: 100
    },
    button: {
        width: 120,
        height: 40,
        backgroundColor: "#FF9800",
        borderRadius: 15,
        flexDirection: 'row',
        justifyContent: 'center',
        alignItems: 'center'
    },
    blur: {
        opacity: 0.2
    },
    notBlur: {
        opacity: 1
    }
})