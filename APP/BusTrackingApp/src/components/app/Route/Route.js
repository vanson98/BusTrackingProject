import React from 'react';
import {View,Text,Button} from 'react-native';

const RouteComponent = (props)=>{
    const navigation = props.navigation;
    return (
        <View style={{flex:1,alignItems:'center',justifyContent:'center'}}>
            <Text>Các lượt đi</Text>
            <Button 
                title="Go to Details Round"
                onPress={()=>navigation.navigate("RouteMap")}/>
        </View> 
        )
}

export default RouteComponent