import React from 'react';
import {View,Text,Button} from 'react-native';
import { shallowEqual, useSelector, useDispatch } from 'react-redux';
import { signOut } from '../../../actions/UserActions';

const ProfileComponent = ({navigation})=>{
    const dispatch = useDispatch();

    const logOut = () => {
        dispatch(signOut())
    }

    return (
        <View style={{flex:1,alignItems:'center',justifyContent:'center'}}>
            <Text>Thông tin cá nhân</Text>
            <Button 
                title="Đăng xuất"
                onPress={()=>logOut()}
                />
        </View> 
    )
}


export default ProfileComponent