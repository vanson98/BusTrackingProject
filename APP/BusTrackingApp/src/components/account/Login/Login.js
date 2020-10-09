import React, {useState} from 'react';
import {
  View,
  Dimensions,
  StyleSheet,
  Text,
  TextInput,
  Image,
  StatusBar,
  TouchableOpacity,
  Platform,
  Alert
} from 'react-native';
import LinearGradient from 'react-native-linear-gradient';
import Ionicons from 'react-native-vector-icons/Ionicons';
import * as Animatable from 'react-native-animatable';
import { styles } from './style';
import { shallowEqual, useSelector, useDispatch } from 'react-redux';
import { signIn} from '../../../actions/UserActions';
 
const LoginComponent = ({navigation}) => {
    const dispatch = useDispatch();

    // ============= State ===============
    const [data, setData] = useState({
      username: '',
      password: '',
      check_textInputChange: false,
      secureTextEntry: true
    });
  
    //============= get context =============
   
    
    //============= function =============
    const textInputChange = (val) => {
      if (val.length !== 0) {
        setData({
          ...data,
          username: val,
          check_textInputChange: true,
        });
      } else {
        setData({
          ...data,
          username: val,
          check_textInputChange: false,
        });
      }
    };
  
    const handlePasswordChange = (val) => {
      setData({
        ...data,
        password: val,
      });
    };
  
    const updateSecureTextEntry = () => {
      setData({
        ...data,
        secureTextEntry: !data.secureTextEntry,
      });
    };
  
    const loginHandle = (username,password) => {
      dispatch(signIn(username,password))
    }
  
    
    return (
      <View style={styles.container}>
        <StatusBar backgroundColor="#FFC107" barStyle="light-content"></StatusBar>
          
        <View style={styles.header}>
          <Text style={styles.text_header}>Xin chào!</Text>
        </View>
  
        <Animatable.View animation="fadeInUpBig" style={styles.footer}>
          <Text style={styles.text_footer}>Tên đăng nhập</Text>
          <View style={styles.action}>
            <Ionicons name="person-outline" size={20} />
            <TextInput
              placeholder="Tên đăng nhập"
              style={styles.textInput}
              autoCapitalize="none"
              onChangeText={(val) => textInputChange(val)}
            />
            {data.check_textInputChange ? (
              <Animatable.View animation="bounceIn">
                <Ionicons name="checkmark-outline" color="green" size={20} />
              </Animatable.View>
            ) : null}
          </View>
          <Text style={[styles.text_footer, {marginTop: 20}]}>Mật khẩu</Text>
          <View style={styles.action}>
            <Ionicons name="lock-closed-outline" size={20} />
            <TextInput
              placeholder="Mật khẩu"
              secureTextEntry={data.secureTextEntry}
              style={[styles.textInput]}
              autoCapitalize="none"
              onChangeText={(val) => handlePasswordChange(val)}
            />
            <TouchableOpacity onPress={updateSecureTextEntry}>
              {!data.secureTextEntry ? (
                <Ionicons name="eye-off-outline" color="green" size={20} />
              ) : (
                <Ionicons name="eye-outline" color="green" size={20} />
              )}
            </TouchableOpacity>
          </View>
          <View style={styles.button}>
            <TouchableOpacity onPress={()=>{loginHandle(data.username,data.password)}} style={styles.signIn}>
              <LinearGradient
                colors={['#FFA000', '#FFC107']}
                style={styles.signIn}
                >
                <Text style={styles.textSign}>Đăng nhập</Text>
              </LinearGradient>
            </TouchableOpacity>
          </View>
        </Animatable.View>
      </View>
    );
};

export default LoginComponent;