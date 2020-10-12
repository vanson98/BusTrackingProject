
import React from 'react';
import { View,Text,StatusBar,TouchableOpacity } from 'react-native';
import LinearGradient from 'react-native-linear-gradient'
import Ionicons from 'react-native-vector-icons/Ionicons';
import * as Animatable from 'react-native-animatable';
import {styles} from './style';

const SplashComponent = ({navigation})=>{
    return (
        <View style={styles.container}>
            <StatusBar backgroundColor='#FFC107' barStyle='light-content'></StatusBar>
            <View style={styles.header}>
                <Animatable.Image 
                    animation="bounceIn"
                    duraton="1500"
                    source={require('../../../share/assets/image/logo.png') }
                    style={styles.logo}
                    resizeMode="stretch"
                />
            </View>
            <Animatable.View 
                style={styles.footer}
                animation="fadeInUpBig"
            >
                <Text style={styles.title}>Chào mừng đến với hệ thống đưa đón học sinh</Text>
                <Text style={styles.text}>Đăng nhập vào hệ thống</Text>
                <View style={styles.button}>
                    <TouchableOpacity onPress={()=>navigation.navigate('LoginScreen')}>
                        <LinearGradient
                        colors={["#FFA000","#FFC107"]}
                        style={styles.signIn}
                        >
                            <Text style={styles.textSign}>Bắt đầu</Text>
                            <Ionicons 
                                name="chevron-forward-outline" 
                                color="#fff" 
                                size={18}
                                style={{marginRight: -10}}
                            ></Ionicons>
                        </LinearGradient>
                    </TouchableOpacity>
                </View>
            </Animatable.View>
        </View>
    )
}

export default SplashComponent;

