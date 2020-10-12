import React from 'react';
import {View,Text,Image,StyleSheet,TouchableOpacity, Button} from 'react-native';

const StudentItem = (props) =>{
    const { student }= props;
    const router = props.router;
    const { typeCheck } = props;
    return (
        <View style={styles.container}>
            <Image 
                source={require("../../../share/assets/image/student.png")} 
                style={styles.avartar}
            />
            <View style={styles.content}>
                <View style={styles.profile}>
                    <TouchableOpacity onPress={()=>router.navigate('DetailStudent')}>
                        <Text style={{fontSize:16,fontWeight:'bold'}}>
                            {student.name}
                        </Text>
                    </TouchableOpacity>
                    <Text>{student.class}</Text>
                </View>
                <View style={styles.action}>
                    {   typeCheck==0?
                        <Button title="Đã đón" color="#FF9800"></Button> :
                        <Button title="Đã trả" color="#FF9800"></Button>
                    }
                       <Button title="Vắng mặt" color="#F44336"></Button>
                </View>
            </View>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flexDirection: 'row',
        height: 120,
        width: "100%",
        backgroundColor: "#FFF",
        marginBottom: 8,
        alignItems: "center"
    },
    content: {
        flexDirection:'row',
        paddingRight: 65,
        flexGrow: 1
    },
    profile:{
        flexDirection: 'column',
        flexGrow: 1,
        height: 90,
        borderLeftWidth: 1,
        borderColor: '#d1d1d1',
        paddingLeft:10,
    },
    action:{
        display: "flex",
        flexDirection: 'column',
        width:100,
        justifyContent: "space-around"
    },
    avartar: {
        width: 50,
        height: 50,
        marginTop: "auto",
        marginBottom: "auto",
        marginLeft: 5,
        marginRight: 5,
        borderRightWidth: 2,
        overflow: "hidden",
        borderColor: "#9c9c9c",
    },
    
})

export default StudentItem;