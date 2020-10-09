import React from 'react';
import {View,Text,Image,StyleSheet,TouchableOpacity, Button} from 'react-native';

const StudentItem = () =>{
    return (
        <View>
            <Image style={}></Image>
            <View>
                <Text>Nguyễn Văn Sơn</Text>
                <Text>Lớp 3A</Text>
            </View>
            <View>
                <Button title="Đã đón"></Button>
                <Button title="Vắng mặt"></Button>
            </View>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flexDirection: 'row',
    },

})