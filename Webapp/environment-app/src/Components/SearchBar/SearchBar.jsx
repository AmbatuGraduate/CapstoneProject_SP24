import { useState } from "react";
import './searchbar.scss';

import { BiSearchAlt } from "react-icons/bi";

export default function SearchBar() {
    const [text, setText] = useState([]);

    const onSubmit = e => {
        e.preventDefault();
        if (text === "") {
            alert("Please enter something!");
        } else {
            alert(text);
            setText("");
        }
    };

    const onChange = e => setText(e.target.value);

    return (
        <div className="searchBar flex">
            <input type="text" name="text" placeholder='Tìm kiếm' value={text} onChange={onChange} />
            <BiSearchAlt className='icon' type="submit" onSubmit={onSubmit} />
        </div>
    )
}