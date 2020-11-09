import React, {useState } from "react"
import {View,Text, StyleSheet, TextInput} from 'react-native';
import {useSelector} from 'react-redux';
import { TouchableOpacity } from "react-native-gesture-handler";
import { useDispatch } from 'react-redux';
import { updateAccount } from '../../../../actions/UserActions';

export default function UpdateAccount(props){
    const user = useSelector((state)=>state.user)
    const navigator = props.navigation;
    const dispatch = useDispatch();

    const [userUpdate,setUserUpdate]=useState({
        email: user.email,
        fullName: user.fullName,
        phoneNumber: user.phoneNumber,
        userValid: true,
    })

    const update = () => {
        dispatch(updateAccount(user.userId,userUpdate.fullName,userUpdate.email,userUpdate.phoneNumber,user.userToken))
    }

    const emailChange = (val) => {
        if (val.length !== 0) {
          setUserUpdate({
            ...userUpdate,
            email: val,
            userValid: true,
          });
        } else {
          setUserUpdate({
            ...userUpdate,
            email: val,
            userValid: false,
          });
        }
    };

    const nameChange = (val) => {
    if (val.length !== 0) {
        setUserUpdate({
            ...userUpdate,
            fullName: val,
            userValid: true,
        });
    } else {
        setUserUpdate({
            ...userUpdate,
            fullName: val,
            userValid: false,
        });
    }
    };

    const phoneChange = (val) => {
        if (val.length !== 0) {
            setUserUpdate({
                ...userUpdate,
                phoneNumber: val,
                userValid: true,
            });
        } else {
            setUserUpdate({
                ...userUpdate,
                phoneNumber: val,
                userValid: false,
            });
        }
        };
    
    return (
        <View style={styles.container}>
            <View style={styles.field_container}>
                <Text style={styles.lable}>Thư điện tử: </Text>
                <TextInput 
                    value={userUpdate.email} 
                    style={styles.textInput}
                    onChangeText={(val) => emailChange(val)}
                    />
            </View>
            <View style={styles.field_container}>
                <Text style={styles.lable}>Họ và tên: </Text>
                <TextInput 
                    value={userUpdate.fullName} 
                    style={styles.textInput}
                    onChangeText={(val) => nameChange(val)}
                    />
            </View>
            <View style={styles.field_container}>
                <Text style={styles.lable}>Số điện thoại: </Text>
                <TextInput 
                    value={userUpdate.phoneNumber} 
                    style={styles.textInput}
                    onChangeText={(val) => phoneChange(val)}
                    />
            </View>
            <View style={styles.button_container}>
                <TouchableOpacity 
                    style={[styles.button,{backgroundColor:'grey'}]}
                    onPress={()=>navigator.goBack()}>
                    <Text style={{fontSize: 18,color: '#FFF'}}>Hủy</Text>
                </TouchableOpacity>
                <TouchableOpacity 
                    style={[styles.button,(!userUpdate.userValid) ? styles.blur : styles.notBlur]}
                    disabled={!userUpdate.userValid}
                    onPress={()=>{update()}}
                    >
                    <Text style={{fontSize: 18,color: '#FFF'}}>Cập nhật</Text>
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