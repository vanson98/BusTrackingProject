
import React from 'react';
import { View, Dimensions, StyleSheet,Text,Image,StatusBar,TouchableOpacity } from 'react-native';
import LinearGradient from 'react-native-linear-gradient'
import Ionicons from 'react-native-vector-icons/Ionicons';
import * as Animatable from 'react-native-animatable';

const SplashScreen = ({navigation})=>{
    return (
        <View style={styles.container}>
            <StatusBar backgroundColor='#FFC107' barStyle='light-content'></StatusBar>
            <View style={styles.header}>
                <Animatable.Image 
                    animation="bounceIn"
                    duraton="1500"
                    source={require('../assets/logo.png') }
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
                    <TouchableOpacity onPress={()=>navigation.navigate('SignInScreen')}>
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

export default SplashScreen;

const {height} = Dimensions.get("screen");
const height_logo = height * 0.28;

const styles = StyleSheet.create({
  container: {
    flex: 1, 
    backgroundColor: '#FFC107'
  },
  header: {
      flex: 2,
      justifyContent: 'center',
      alignItems: 'center'
  },
  footer: {
      flex: 1,
      backgroundColor: '#fff',
      borderTopLeftRadius: 30,
      borderTopRightRadius: 30,
      paddingVertical: 50,
      paddingHorizontal: 30
  },
  logo: {
      width: height_logo,
      height: height_logo
  },
  title: {
      color: '#05375a',
      fontSize: 30,
      fontWeight: 'bold'
  },
  text: {
      color: 'grey',
      marginTop:5
  },
  button: {
      alignItems: 'flex-end',
      marginTop: 30
  },
  signIn: {
      width: 120,
      height: 40,
      justifyContent: 'center',
      alignItems: 'center',
      borderRadius: 50,
      flexDirection: 'row'
  },
  textSign: {
      color: 'white',
      fontWeight: 'bold'
  }
});