import { useState } from "react";
import reactLogo from "../assets/react.svg";
import viteLogo from "/vite.svg";
import "./Css/AllGroups.css";
import GroupList from "../Lists/GroupList.jsx";
import { Header } from "../Components/Header.jsx";

const AllGroups = () => {
  document.title = `Group Finance Tracker`;
  return (
    <>
      <Header />
      <div className="pageContent">
        <h1>Groups</h1>
        <GroupList />
      </div>
    </>
  );
};

export default AllGroups;
