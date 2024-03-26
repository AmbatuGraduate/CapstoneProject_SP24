import 'react-native-gesture-handler';
import * as Font from 'expo-font';
import React, { useState, useEffect } from 'react';
import Routes from './navigations/drawer';
import * as SplashScreen from 'expo-splash-screen'; // loading screen
import { RootSiblingParent } from 'react-native-root-siblings'; // show toast all app


/*************************************************************
**__________________ MAIN SCREEN OF APP ____________________**
**                  CREATED BY: LE ANH QUAN                 **
*************************************************************/





SplashScreen.preventAutoHideAsync();
// ----------------------------------
// App funcion
export default function App() {
  const [fontsLoaded, setFontsLoaded] = useState(false);

  useEffect(() => {
    async function prepareFont() {
      try {
        await Font.loadAsync({
          'nunito-regular': require('./assets/fonts/static/NunitoSans_7pt-Regular.ttf'),
          'nunito-bold': require('./assets/fonts/static/NunitoSans_7pt-SemiBold.ttf'),
          'quolibet': require('./assets/fonts/static/QuodlibetSans-Regular.ttf'),
        });
      } catch (e) {
        console.warn(e);
      } finally {
        setFontsLoaded(true);
        SplashScreen.hideAsync();
      }
    }
    prepareFont();
  }, []);

  if (!fontsLoaded) {
    return null;
  }
  return (
    <RootSiblingParent>
      <Routes />
    </RootSiblingParent>

  )

}
