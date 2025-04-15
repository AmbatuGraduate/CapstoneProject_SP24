import { useState } from "react";
import "./style.scss";

import { BiSearchAlt } from "react-icons/bi";
import React from "react";
type Props = {
  onSubmit: (text: string) => void;
};
export default function SearchBar(props: Props) {
  const [text, setText] = useState("");

  const onChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setText(e.target.value);
    props.onSubmit(e.target.value);
  };

  return (
    <div className="searchBar flex">
      <input
        type="text"
        name="text"
        placeholder="Tìm kiếm"
        value={text}
        onChange={onChange}
      />
      <div onClick={() => props.onSubmit(text)}>
        <BiSearchAlt className="icon" />
      </div>
    </div>
  );
}
