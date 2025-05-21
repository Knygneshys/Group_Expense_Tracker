import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import TransactionRecipientsList from "../Lists/TransactionRecipientsList";
import Dropdown from "../Components/Dropdown/Dropdown";

const apiUrl = import.meta.env.VITE_API_URL;

const Transaction = () => {
  const params = useParams();
  const [members, setMembers] = useState([]);
  const [group, setGroup] = useState();
  const [splitType, setSplitType] = useState("D");

  const groupId = params.groupId;
  const splitTypes = ["Dynamic", "Equal", "Percentage"];

  useEffect(() => {
    getData(setMembers, setGroup, groupId);
  }, []);

  useEffect(() => {
    console.log(splitType);
  }, [splitType]);

  if (group !== undefined) {
    return (
      <>
        <h1>New Transaction in {group.name}</h1>
        <form>
          <div>
            <label>Payment amount: </label>
            <input type="number" min="0" placeholder="0" required></input>
            <br />
            <label>Split type: </label>
            <Dropdown items={splitTypes} setSelectedValue={setSplitType} />
          </div>
          <div>
            <TransactionRecipientsList recipients={members} />
          </div>
          <button type="submit">Make Transaction</button>
        </form>
      </>
    );
  }

  return <h1>Loading...</h1>;
};

async function getData(setMembers, setGroup, groupId) {
  const fetchedGroup = await fetchGroup(groupId);
  setGroup(fetchedGroup);
  setMembers(fetchedGroup.members);
}

async function fetchGroup(groupID) {
  try {
    const fetchUrl = `${apiUrl}/api/Groups/${groupID}`;
    const response = await fetch(fetchUrl);
    if (!response.ok) {
      throw error(`Failed to fetch group by groupId: ${groupID}`);
    }
    const group = await response.json();

    return group;
  } catch (error) {
    conmsole.log(error);
  }
}

export default Transaction;
