import React from "react";
import "./CSS/header.css";
import { Link, NavLink } from "react-router-dom";

export const Header = () => {
  return (
    <div className="customHeader">
      <h1 className="mainHeader">Group Finance Tracker</h1>
      <NavLink to="/GroupList/0" className="groupLink">
        Go to groups
      </NavLink>
    </div>
  );
};
