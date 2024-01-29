import React from 'react';
import './style.scss';

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
import { GrMapLocation } from "react-icons/gr";
import { GiTowTruck } from "react-icons/gi";


// import router
import { NavLink } from 'react-router-dom';


const Sidebar = () => {
    return (
        <div className='sideBar '>

            <div className='logoDiv flex'>
                <img src={logo} alt='Image_name' />

            </div>
            <hr className='line' />
            <div className='menuDiv p-0'>

                <ul className="menuLists grid">

                    <li className="listItem" >
                        <NavLink exact to="/" className='menuLink flex' activeClassName='active'>
                            <TbLayoutDashboard className='icon' />
                            <span className='smallText d-none d-md-inline '>
                                Quản lý báo cáo
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink exact to='/manage-employee' className='menuLink flex' activeClassName='active'>
                            <GrUserManager className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý nhân sự
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink exact to='/manage-tree' className='menuLink flex ' activeClassName='active'>
                            <LuTrees className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý cây xanh
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink exact to="/" className='menuLink flex' activeClassName='active'>
                            <GiTowTruck className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý xe cắt tỉa
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink exact to="/manage-route" className='menuLink flex' activeClassName='active'>
                            <MdOutlineMap className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý tuyến đường
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink exact to="/manage-garbage-truck" className='menuLink flex' activeClassName='active'>
                            <BsBusFrontFill className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý xe thu gom
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink exact to="/" className='menuLink flex' activeClassName='active'>
                            <GrMapLocation className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý bãi thu gom
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink exact to="/" className='menuLink flex' activeClassName='active'>
                            <GiTreeBranch className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý lịch cắt tỉa
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink exact to='/' className='menuLink flex' activeClassName='active'>
                            <FaTreeCity className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý lịch vệ sinh đô thị
                            </span>
                        </NavLink>
                    </li>

                    <li className="listItem">
                        <NavLink exact to='/' className='menuLink flex' activeClassName='active'>
                            <PiTrashSimpleBold className='icon' />
                            <span className='smallText d-none d-md-inline'>
                                Quản lý lịch thu gom rác
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
                    <p>Nếu bạn gặp vấn đề khi sử dụng Ambatu, xin hãy liên hệ với chúng tôi để nhận hỗ trợ.</p>
                    <button className='btn'>Liên hệ trung tâm</button>
                </div>
            </div>
        </div>
    );
};

export default Sidebar;