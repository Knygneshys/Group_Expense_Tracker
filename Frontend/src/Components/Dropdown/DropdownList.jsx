import React from "react";

export const DropdownList = ({ items }) => {
  const list = items.map((i, index) => <option key={index}>{i}</option>);
  return list;
};
