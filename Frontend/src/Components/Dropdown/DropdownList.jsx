import React from "react";

export const DropdownList = ({ items }) => {
  const list = items.map((i) => <option key={i}>{i}</option>);
  return list;
};
