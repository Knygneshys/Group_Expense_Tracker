import React from "react";
import { DropdownList } from "./DropdownList";

const Dropdown = ({ items, setSelectedValue, displayedValues }) => {
  return (
    <select
      style={{ marginLeft: "7px" }}
      class="btn btn-secondary btn-sm dropdown-toggle"
      onChange={(e) => {
        setSelectedValue(e.target.value);
      }}
    >
      <DropdownList items={items} displayedValues={displayedValues} />
    </select>
  );
};

export default Dropdown;
