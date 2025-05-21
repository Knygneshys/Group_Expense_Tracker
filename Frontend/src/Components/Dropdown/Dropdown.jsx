import React from "react";
import { DropdownList } from "./DropdownList";

const Dropdown = ({ items }) => {
  return (
    <select>
      <DropdownList items={items} />
    </select>
  );
};

export default Dropdown;
