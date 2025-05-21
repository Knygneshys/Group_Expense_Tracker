import React from "react";

export const DropdownList = ({ items, displayedValues }) => {
  const list = items.map((item, index) => (
    <option key={index} value={item}>
      {displayedValues[index]}
    </option>
  ));

  return list;
};
