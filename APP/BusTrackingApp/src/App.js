import React,{useEffect} from 'react';
import {Provider} from 'react-redux';
import AppNavigation from './navigation';
import RNBootSplash from 'react-native-bootsplash';
import { persistor,store } from './store';

function App() {
  // const user = useSelector(getUser);
  // if (user.isLoading) {
  //   return (
  //     <Provider store={store}>
  //       <View style={{flex: 1, justifyContent: 'center', alignItems: 'center'}}>
  //         <ActivityIndicator size="large" color="#bc2b78" />
  //       </View>
  //     </Provider>
  //   );
  // }
  useEffect(() => {
    persistor(RNBootSplash.hide);
  }, []);

  return (
    <Provider store={store}>
      <AppNavigation />
    </Provider>
  );
}

export default App;
