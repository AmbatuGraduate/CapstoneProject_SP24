import { useState } from "react";
import './searchbar.scss';

import { BiSearchAlt } from "react-icons/bi";

export default function SearchBar() {
    const [results, setResults] = useState([]);

    return (
        <div className="searchBar flex">
            <input type="text" placeholder='Tìm kiếm' />
            <BiSearchAlt className='icon' />
        </div>
    )
}