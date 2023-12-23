import React from 'react';
import './sidebar.scss';

// Imported Images
import logo from '../../Assets/logo.jpg';

// Imported Icons
import { TbLayoutDashboard } from "react-icons/tb";
import { LuTrees } from "react-icons/lu";
import { FaTreeCity } from "react-icons/fa6";
import { GrUserManager } from "react-icons/gr";
import { BsQuestionCircle } from "react-icons/bs";

// import router
import { NavLink } from 'react-router-dom';

const Sidebar = () => {
    return (
        <div className='sideBar gird'>

            <div className='logoDiv flex'>
                <img src={logo} alt='Image_name' />
            </div>
            <hr className='lineBlock' />
            <div className='menuDiv'>
                <h3 className="divTitle">
                    Điều hướng
                </h3>
                <ul className="menuLists grid">

                    <li className="listItem">
                        <NavLink to="/" className='nemuLink flex'>
                            <TbLayoutDashboard className='icon' />
                            <span className='smallText'>
                                Quản lý
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink to='/manage-tree' className='nemuLink flex'>
                            <LuTrees className='icon' />
                            <span className='smallText'>
                                Quản lý cây xanh
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink to='/' className='nemuLink flex'>
                            <FaTreeCity className='icon' />
                            <span className='smallText'>
                                Quản lý vệ sinh đô thị
                            </span>
                        </NavLink>

                    </li>
                    <li className="listItem">
                        <NavLink to='/' className='nemuLink flex'>
                            <GrUserManager className='icon' />
                            <span className='smallText'>
                                Quản lý nhân sự
                            </span>
                        </NavLink>
                    </li>
                </ul>
            </div>

            <div className="sideBarCard">
                <BsQuestionCircle className='icon' />
                <div className='cardContent'>
                    <div className="circle1"></div>
                    <div className="circle2"></div>

                    <h3>Trung tâm trợ giúp</h3>
                    <p>Nếu bạn gặp vấn đề khi sử dụng Ambatu, xin hãy liên hệ với chúng tôi để giải đáp.</p>
                    <button className='btn'>Liên hệ trung tâm</button>
                </div>
            </div>
        </div>
    );
};

export default Sidebar;