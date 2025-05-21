import React, { use, useEffect, useRef, useState } from "react";
import { useParams } from "react-router-dom";
import TransactionRecipientsList from "../Lists/TransactionRecipientsList";
import Dropdown from "../Components/Dropdown/Dropdown";

const apiUrl = import.meta.env.VITE_API_URL;

const Transaction = () => {
  const params = useParams();
  const [members, setMembers] = useState([]);
  const [group, setGroup] = useState();
  const [splitType, setSplitType] = useState("D");
  const [amount, setAmount] = useState(0);
  const [payerId, setPayerId] = useState(0);
  const recipientPaymentsRef = useRef(null);

  const groupId = params.groupId;
  const splitTypes = ["Dynamic", "Equal", "Percentage"];

  useEffect(() => {
    getData(setMembers, setGroup, groupId);
    setPayerId(params.memberId);
  }, []);

  useEffect(() => {
    console.log(splitType);
  }, [splitType]);

  function handleSubmit(e) {
    e.preventDefault();
    console.log("PAVYKO!");
    const transaction = {
      groupId: groupId,
      senderId: payerId,
      amount: amount,
      splitType: splitType,
      recipients: recipientPaymentsRef.current.getPayments(),
    };

    console.log(transaction);
  }

  if (group !== undefined && members.length > 0) {
    const idList = members.map((mem) => mem.id);
    const memberIds = [0, ...idList];
    const memberNameList = members.map((mem) => `${mem.name} ${mem.surname}`);
    const displayedValues = [`You`, ...memberNameList];

    return (
      <>
        <h1>New Transaction in {group.name}</h1>
        <form onSubmit={handleSubmit}>
          <div>
            <label>Sender:</label>
            <Dropdown
              items={memberIds}
              setSelectedValue={setPayerId}
              displayedValues={displayedValues}
            />
            <label>Payment amount: </label>
            <input
              name="amount"
              type="number"
              step={0.01}
              min={0}
              placeholder={0}
              value={amount}
              onChange={(e) => setAmount(parseFloat(e.target.value) || 0)}
              required
            ></input>
            <br />
            <label>Split type: </label>
            <Dropdown
              items={splitTypes}
              setSelectedValue={setSplitType}
              displayedValues={splitTypes}
            />
          </div>
          <div>
            <TransactionRecipientsList
              recipients={members}
              ref={recipientPaymentsRef}
            />
          </div>
          <button type="submit">Make Transaction</button>
        </form>
      </>
    );
  } else if (group !== undefined && members.length <= 0) {
    return <h3>The group is empty. Please add some members first.</h3>;
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
