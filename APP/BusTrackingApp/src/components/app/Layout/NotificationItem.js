import React, { useEffect } from "react"
import { View,Text, StyleSheet} from "react-native";
import Ionicons from 'react-native-vector-icons/Ionicons';
import moment from 'moment';
import 'moment/locale/vi'  
moment.locale('vi')

export default function NotificationItem(props){
    const { message } = props;
    return (
        <View style={styles.container}>
            <View style={styles.icon_container}>
                {
                    message.typeNotification == 1 ? 
                    <Ionicons name="alert-outline" size={30}></Ionicons> 
                    :
                    message.typeNotification == 2 ?
                    <Ionicons name="bus-outline" size={30}></Ionicons> 
                    :
                    message.typeNotification == 3 ?
                    <Ionicons name="business-outline" size={30}></Ionicons> 
                    :
                    message.typeNotification == 4 ?
                    <Ionicons name="alert-outline" size={30}></Ionicons> 
                    :
                    message.typeNotification == 5 ?
                    <Ionicons name="bus-outline" size={30}></Ionicons> 
                    :
                    message.typeNotification == 6 ?
                    <Ionicons name="walk-outline" size={30}></Ionicons> 
                    :
                    message.typeNotification == 8 ?
                    <Ionicons name="medkit-outline" size={30}></Ionicons> 
                    :
                    message.typeNotification == 9 ?
                    <Ionicons name="log-in-outline" size={30}></Ionicons> 
                    :
                    message.typeNotification == 10 ?
                    <Ionicons name="alert-outline" size={30}></Ionicons> 
                    : 
                    message.typeNotification == 11 ?
                    <Ionicons name="alert-outline" size={30}></Ionicons> 
                    :
                    <Ionicons name="home-outline" size={30}></Ionicons> 
                }
            </View>
            <View style={styles.mess_container}>
                <Text style={{}}>{message.content}</Text>
            </View>
            <View style={styles.time_container}>
                <Text>{moment(message.timeSent).fromNow()}</Text>
            </View>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        flexDirection: 'row',
        flexWrap: 'wrap',
        alignItems: "center",
        height: 'auto',
        paddingLeft: 10,
        paddingRight: 10,
        marginTop: 10, 
    },
    icon_container: {
        height: 45,
        width: 45,
        justifyContent: "center",
        alignItems: "center",
        marginRight: 10,
        borderWidth: 1,
        borderColor: '#FF9800',
        borderRadius: 50
    },
    mess_container: {  
        height: 'auto',
        flexGrow: 1,
        width: 0
    },
    time_container: {
        width: 70,
        marginLeft: 3
    }
})