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
import { GiTreeBranch } from "react-icons/gi";
import { MdOutlineMap } from "react-icons/md";
import { BsBusFrontFill } from "react-icons/bs";
import { PiTrashSimpleBold } from "react-icons/pi";

// import router
import { NavLink } from 'react-router-dom';

const Sidebar = () => {
    return (
        <div className='sideBar '>

            <div className='logoDiv flex'>
                <img src={logo} alt='Image_name' />
            </div>
            <div className='menuDiv p-0'>
                <hr className='line' />
                <ul className="menuLists grid">

                    <li className="listItem">
                        <NavLink to="/" className='menuLink flex'>
                            <TbLayoutDashboard className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý báo cáo
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink to='/manage-tree' className='menuLink flex'>
                            <LuTrees className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý cây xanh
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink to="/" className='menuLink flex'>
                            <GiTreeBranch className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý cắt tỉa
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink to="/" className='menuLink flex'>
                            <MdOutlineMap className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý tuyến đường
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink to="/" className='menuLink flex'>
                            <BsBusFrontFill className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý xe thu gom
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink to='/' className='menuLink flex'>
                            <FaTreeCity className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý vệ sinh đô thị
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink to='/' className='menuLink flex'>
                            <PiTrashSimpleBold className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý thu gom rác
                            </span>
                        </NavLink>

                    </li>

                    <li className="listItem">
                        <NavLink to='/' className='menuLink flex'>
                            <GrUserManager className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý nhân sự
                            </span>
                        </NavLink>
                    </li>
                </ul>
            </div>

            <div className="sideBarCard">
                <BsQuestionCircle className='icon' />
                <div className='cardContent d-none d-xl-block'>
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