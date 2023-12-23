import { useState } from 'react'
import './style.scss'

export const Filter = () => {
    const [showFilter, setShowFilter] = useState(false);
    const toggleFilter = () => {
        setShowFilter(!showFilter);
    };

    return (
        <div className='filter-container'>
            <button onClick={toggleFilter} type="button" className="btn btn-success">
                Bộ lọc
            </button>
            <svg xmlns="http://www.w3.org/2000/svg"
                width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2"
                strokeLinecap="round" strokeLinejoin="round" className="feather feather-filter">
                <polygon points="22 3 2 3 10 12.46 10 19 14 21 14 12.46 22 3" />
            </svg>
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
                        <label>Tình trạng</label>
                        <select>
                            <option>Tất cả</option>
                            <option>Đã cắt tỉa</option>
                            <option>Cần cắt tỉa</option>
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