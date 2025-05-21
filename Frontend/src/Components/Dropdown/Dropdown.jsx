import React from "react";
import { DropdownList } from "./DropdownList";

const Dropdown = ({ items, setSelectedValue }) => {
  return (
    <select
      onChange={(e) => {
        setSelectedValue(e.target.value);
      }}
    >
      <DropdownList items={items} />
    </select>
  );
};

export default Dropdown;
