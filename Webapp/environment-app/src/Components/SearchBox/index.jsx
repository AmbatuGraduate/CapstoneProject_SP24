import { useState } from "react";
import { MdSearch } from "react-icons/md";

import './style.scss'

export const SearchBox = () => {
    const [text, setText] = useState("");

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
            <form onSubmit={onSubmit} className="search-box">
                <input
                    type="text"
                    name="text"
                    placeholder="Tìm kiếm..."
                    value={text}
                    onChange={onChange}
                    className="search-input"
                />
                <button type="submit" className="search-button">
                  <MdSearch />
                </button>
            </form>
    );
}
