import React,{useEffect} from 'react';
import {Provider} from 'react-redux';
import AppNavigation from './navigation';
import RNBootSplash from 'react-native-bootsplash';
import { persistor,store } from './store';

function App() {
 
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
