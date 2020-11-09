import React from 'react';
import {View,Text,Button,Image, StyleSheet} from 'react-native';
import { TouchableOpacity } from 'react-native-gesture-handler';
import { shallowEqual, useSelector, useDispatch } from 'react-redux';
import { signOut } from '../../../actions/UserActions';

const ProfileComponent = (props)=>{
    const navigation = props.navigation;
    const dispatch = useDispatch();

    const logOut = () => {
        dispatch(signOut())
    }

    return (
        <View style={styles.container}>
            <View style={styles.ct_avartar}>
                <Image
                    source={require('../../../share/assets/image/user-profile.png')}
                    style={styles.avatar}
                />
            </View>
            <View style={styles.ct_lable}>
                <Text style={styles.label}>TÀI KHOẢN CỦA TÔI</Text>
            </View>   
            <View style={styles.ct_action}>
                <TouchableOpacity style={styles.action} >
                    <Text style={{fontSize:15,fontWeight:'bold'}} onPress={()=>navigation.navigate('UpdateProfile')}>Cập nhật thông tin</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.action}>
                    <Text style={{fontSize:15,fontWeight:'bold'}} onPress={()=>navigation.navigate('UpdatePassword')}>Đổi mật khẩu</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.action} onPress={()=>logOut()}>
                    <Text style={{fontSize:15,fontWeight:'bold'}}>Đăng xuất</Text>
                </TouchableOpacity>
            </View>
        </View> 
    )
}

const styles = StyleSheet.create({
    container: {
        flex:1,
        flexDirection:'column',
        paddingLeft: 10,
        paddingRight: 10
    },
    ct_avartar: {
        height: 130,
        justifyContent: 'center',
        alignItems: 'center',
      },
      avatar: {
        width: 100,
        height: 100,
        backgroundColor: 'yellow',
        borderRadius: 50
      },
      ct_action: {
        paddingLeft: 10,
      },
      action: {
        height:45,
        borderBottomWidth: 1,
        borderColor: '#aeb0ac',
        paddingTop:12
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
      }
})

export default ProfileComponent

