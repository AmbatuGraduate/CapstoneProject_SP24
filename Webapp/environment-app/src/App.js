import React from 'react';
import './App.css';
import Sidebar from './Components/SideBarSection';
import Body from './Components/BodySection';


const App = () => {
    return (
        <div className='container-fluid m-0 p-0'>
            <Sidebar />
            <Body />
        </div>
    )
}

export default App;
