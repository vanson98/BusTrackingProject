import React from 'react';
import {View,Text,Button} from 'react-native';

export default class NotificationScreen extends React.Component{
    constructor(props){
        super(props);
        this.navi = props.navigation;
    }
    render(){
        
        return (  
        <View style={{flex:1,alignItems:'center',justifyContent:'center'}}>
            <Text>Thong bao</Text>
        </View> 
        )
    }
}

