import React from "react";
import { DropdownList } from "./DropdownList";

const Dropdown = ({ items, setSelectedValue, displayedValues }) => {
  return (
    <select
      onChange={(e) => {
        setSelectedValue(e.target.value);
      }}
    >
      <DropdownList items={items} displayedValues={displayedValues} />
    </select>
  );
};

export default Dropdown;
