import React, { useEffect, useState } from "react"
import {View,Text,Button,Image, StyleSheet, TextInput} from 'react-native';
import {useSelector} from 'react-redux';
import AsyncStorage from '@react-native-community/async-storage';

export default function UpdateAccount(props){
    const user = useSelector((state)=>state.user)
    const [token,setToken]=useState("sonkk")
    
    
    return (
        <View>
            <Text>Thư điện tử: </Text>
            <TextInput value={user.email} style={{ height: 40, borderColor: 'gray', borderWidth: 1 }}/>
            <Text>Mã tài khoản: </Text>
            <Text>{user.userId}</Text>
            <Text>Họ và tên: </Text>
            <Text>{user.fullName}</Text>
            <Text>Quyền: </Text>
            <Text>{user.roles}</Text>
            <Text>Token: </Text>
            <Text>{user.userToken}</Text>
        </View>
        
    );
}