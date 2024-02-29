import React from 'react';
import { Router } from '../../router';
import Top from './TopSection'

import './style.scss'

const Body = () => {
    return (
        <div className='mainContent'>
            <Top />
            <div className="bottom ">
                <Router />

            </div>
        </div>
    );
};

export default Body;