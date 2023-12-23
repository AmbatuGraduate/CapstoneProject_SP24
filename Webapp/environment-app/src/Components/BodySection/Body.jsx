import React from 'react';
import { Router } from '../../router';
import Top from './TopSection/Top'

import './body.scss'

const Body = () => {
    return (
        <div className='mainContent'>
            <Top />
            <div className="bottom flex">
                <Router />
             
            </div>
        </div>
    );
};

export default Body;