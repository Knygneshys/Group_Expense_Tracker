import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

const apiUrl = import.meta.env.VITE_API_URL;

const Transaction = () => {
  const params = useParams();
  const [members, setMembers] = useState([]);
  const groupId = params.groupId;

  useEffect(() => {
    getData(setMembers, groupId);
  }, [members]);

  return (
    <>
      <h1>New Transaction</h1>
      <div>
        {groupId} and {params.memberId}
      </div>
    </>
  );
};

async function getData(setMembers, groupId) {
  const fetchedMembers = await fetchMembers(groupId);
  console.log(fetchedMembers);
}

async function fetchMembers(groupId) {
  try {
    const fetchUrl = `${apiUrl}/api/Members/ByGroup/${groupId}`;
    const response = await fetch(fetchUrl);
    if (!response.ok) {
      throw error("Failed to fetch members by their group id");
    }
    const members = await response.json();

    return members;
  } catch (error) {
    conmsole.log(error);
  }
}

export default Transaction;
