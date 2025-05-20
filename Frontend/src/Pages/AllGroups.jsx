import { useState } from "react";
import reactLogo from "../assets/react.svg";
import viteLogo from "/vite.svg";
import "./Css/AllGroups.css";
import GroupList from "../Lists/GroupList.jsx";

const AllGroups = () => {
  document.title = `Group Finance Tracker`;
  return (
    <>
      <h1>Groups</h1>
      <GroupList />
    </>
  );
};

export default AllGroups;
