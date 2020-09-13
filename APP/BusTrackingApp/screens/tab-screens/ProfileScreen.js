import React,{useContext} from 'react';
import {View,Text,Button} from 'react-native';
import {AuthContext} from '../../components/context';

const ProfileScreen = ({navigation})=>{
    var {signOut} =useContext(AuthContext);
    return (
        <View style={{flex:1,alignItems:'center',justifyContent:'center'}}>
            <Text>Thông tin cá nhân</Text>
            <Button 
                title="Đăng xuất"
                onPress={()=>signOut()}/>
        </View> 
    )
}

export default ProfileScreen