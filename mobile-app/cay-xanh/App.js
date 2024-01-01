import 'react-native-gesture-handler';
import { StyleSheet, Text, View } from 'react-native';
import * as Font from 'expo-font';
import React, { useState } from 'react';
import AppLoading from 'expo-app-loading';
import Home from './screens/home';
import Routes from './navigations/drawer';


/*************************************************************
**__________________ MAIN SCREEN OF APP ____________________**
**                  CREATED BY: LE ANH QUAN                 **
*************************************************************/




// ----------------------------------
// Get font
const getFonts = () => Font.loadAsync({
  'nunito-regular': require('./assets/fonts/static/NunitoSans_7pt-Regular.ttf'),
  'nunito-bold': require('./assets/fonts/static/NunitoSans_7pt-SemiBold.ttf'),
});

// ----------------------------------
// App funcion
export default function App() {
  // Ensure font is loaded before app
  const [fontsLoaded, setFontsLoaded] = useState(false);

  if (fontsLoaded) {
    return (
      <Routes />
    );
  } else {
    return (
      <AppLoading
        startAsync={getFonts}
        onFinish={() => setFontsLoaded(true)}
        onError={console.log('')}
      />
    )
  }

}

// ----------------------------------
// Style
const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
