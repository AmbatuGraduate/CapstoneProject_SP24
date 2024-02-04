
import { useState } from 'react'
import './style.scss'

// Imported icon
import { FiFilter } from "react-icons/fi";


export const RouteFilter = () => {
    const [showFilter, setShowFilter] = useState(false);
    const toggleFilter = () => {
        setShowFilter(!showFilter);
    };

    return (
        <div className='filter-container flex'>
            <button onClick={toggleFilter} type="button" className="btn btn-success">
                <span className='smallText d-none d-sm-inline'>
                    Bộ lọc
                </span>
                <FiFilter className='icon' />
            </button>
            {
                showFilter ?
                    <div className="filter-menu">
                        <label>Quận</label>
                        <select>
                            <option>Tất cả </option>
                            <option>Hải Châu</option>
                            <option>Thanh Khê</option>
                            <option>Sơn Trà</option>
                            <option>Ngũ Hành Sơn</option>
                            <option>Cẩm Lệ</option>
                            <option>Liên Chiểu</option>
                        </select>
                        <label>Loại tuyến đường</label>
                        <select>
                            <option>Tất cả</option>
                            <option>Dân sinh</option>
                            <option>Kinh doanh</option>
                        </select>
                        <div className="filter-menu-buttons">
                            <button className="filter-button reset">
                                Đặt lại
                            </button>
                            <button className="filter-button apply">
                                Duyệt
                            </button>
                        </div>
                    </div>
                    : ""
            }
        </div>
    )
}