import React from 'react';
import './App.css';
import Sidebar from './Components/SideBarSection/Sidebar';
import Body from './Components/BodySection/Body';

const App = () => {
    return (
        <div className='container-fluid m-0 p-0'>
            <Sidebar />
            <Body />
        </div>
    )
}

export default App;