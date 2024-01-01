import { createDrawerNavigator } from "@react-navigation/drawer";
import { NavigationContainer } from '@react-navigation/native';
import HomeStackRouting from "./homeStack";
import ProfileStackRouting from "./profileStack";


/*************************************************************
**____CONFIGUURE ALL ROUTES THAT USE DRAWER NAVIGATION _____**
**                  CREATED BY: LE ANH QUAN                 **
*************************************************************/

const Drawer = createDrawerNavigator();
function Routes() {
    return (
        <NavigationContainer>
            <Drawer.Navigator screenOptions={{
                headerShown: false,
            }}>
                <Drawer.Screen name="Home" component={HomeStackRouting} />
                <Drawer.Screen name="Profile" component={ProfileStackRouting} />
            </Drawer.Navigator>
        </NavigationContainer>
    );
}


export default Routes;