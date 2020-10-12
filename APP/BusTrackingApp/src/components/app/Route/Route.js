import React, { useState } from 'react';
import {StyleSheet, TouchableOpacity} from 'react-native';
import { FlatList } from 'react-native-gesture-handler';
import StudentItem from '../Layout/StudentItem';
import Ionicons from 'react-native-vector-icons/Ionicons';

const RouteComponent = (props)=>{
    const navigation = props.navigation;
    var typeCheck = props.typeCheck;
    const [students, setStudent]=useState(
        
            [
                {
                    id: 1,
                    name: "Vương Xuân Vinh",
                    class: "16A04"
                },
                {
                    id: 2,
                    name: "Nguyễn Văn Sơn",
                    class: "12A2"
                },
                {
                    id: 3,
                    name: "Nguyễn Thu Huyền",
                    class: "12A4"
                },
                {
                    id: 4,
                    name: "Vương Xuân Vinh",
                    class: "16A04"
                },
                {
                    id: 5,
                    name: "Nguyễn Văn Sơn",
                    class: "12A2"
                },
                {
                    id: 6,
                    name: "Nguyễn Thu Huyền",
                    class: "12A4"
                }
            ]
    )

    React.useLayoutEffect(()=>{
        navigation.setOptions({
            headerRight: ()=>(
                <TouchableOpacity style={styles.headerRight}>
                    <Ionicons name="map" color={'#FFF'} size={26} />
                </TouchableOpacity>
            ),
            title: typeCheck==0? "Lượt đi" : "Lượt về"
        },[navigation])

    })

    return (
        <FlatList 
            style={styles.container}
            data={students}
            renderItem={({item})=>
                <StudentItem student={item} router={navigation} typeCheck={typeCheck}/>
            }
            keyExtractor={item=>item.id.toString()}
        >
        </FlatList>
        // <View style={{flex:1,alignItems:'center',justifyContent:'center'}}>
        //     <Text>Các lượt đi</Text>
        //     <Button 
        //         title="Go to Details Round"
        //         onPress={()=>navigation.navigate("RouteMap")}/>
        // </View> 
    )
}

const styles = StyleSheet.create({
    container: {
        width: "100%",
        height: 500,
        backgroundColor: '#e3dfde'
    },
    headerRight: {
        width: 50,
        height: '100%',
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'center'
    }
})

export default RouteComponent