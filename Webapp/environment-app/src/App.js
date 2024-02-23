import React from 'react';
import './App.css';
import Sidebar from './Components/SideBarSection';
import Body from './Components/BodySection';
import { GoogleOAuthProvider } from '@react-oauth/google';


const App = () => {
    return (
        <GoogleOAuthProvider clientId='1083724780407-f10bbbl6aui68gfglabjalr9ae0627jj.apps.googleusercontent.com'>
            <div className='container-fluid m-0 p-0'>
                <Sidebar />
                <Body />
            </div>
        </GoogleOAuthProvider>

    )
}

export default App;
