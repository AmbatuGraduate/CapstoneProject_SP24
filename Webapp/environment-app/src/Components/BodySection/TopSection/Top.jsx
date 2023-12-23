import React from 'react';
import './top.scss'

// imported Icon
import { IoNotificationsOutline } from "react-icons/io5";

// imported Image
import img from '../../../Assets/adminImg.jpg'
import SearchBar from '../../SearchBar/SearchBar';




const Top = () => {
    return (
        <div className='topSection'>
            <div className='headerSection flex'>
                <div className='title'>
                    <h1>Chào mừng tới với Ambatu</h1>
                    <p>Xin chào Thăng</p>
                </div>

                {/* <div className="searchBar flex">
                    <input type="text" placeholder='Tìm kiếm' />
                    <BiSearchAlt className='icon' />
                </div> */}
                <div className='searchBar flex'>
                    <SearchBar />
                </div>

                <div className="adminDiv flex">
                    <IoNotificationsOutline className='icon' />
                    <div className="adminImage">
                        <button ><img src={img} alt="Admin Image" /></button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Top