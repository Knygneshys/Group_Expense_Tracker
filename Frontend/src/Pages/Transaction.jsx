import React, { use, useEffect, useRef, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import TransactionRecipientsList from "../Lists/TransactionRecipientsList";
import Dropdown from "../Components/Dropdown/Dropdown";
import { Header } from "../Components/Header";
import "./Css/transaction.css";

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
  const memberId = params.memberId;

  const splitTypes = ["D", "E", "P"];

  useEffect(() => {
    getData(setMembers, setGroup, groupId, setAmount, memberId);
    setPayerId(0);
  }, []);

  const navigate = useNavigate();
  const goToGroup = () => {
    navigate(`/Group/${group.id}`);
  };

  async function handleSubmit(e) {
    e.preventDefault();
    if (amount > 0) {
      const transaction = {
        groupId: groupId,
        senderId: payerId,
        amount: amount,
        splitType: splitType,
        recipients: recipientPaymentsRef.current.getPayments(),
      };

      await postTransaction(transaction);
      goToGroup();
    }
  }

  if (group !== undefined && members.length > 0) {
    const idList = members.map((mem) => mem.id);
    const memberNameList = members.map((mem) => `${mem.name} ${mem.surname}`);
    const displayedSplitTypes = ["Dynamic", "Equal", "Percentage"];
    const fontSizePt = "15pt";
    return (
      <>
        <Header />
        <div className="pageContent">
          <h1 className="groupHeader">New Transaction in {group.name}</h1>
          <form onSubmit={handleSubmit}>
            <div className="firstHalfOfForm">
              <label style={{ fontSize: fontSizePt }}>Sender:</label>
              <Dropdown
                items={idList}
                setSelectedValue={setPayerId}
                displayedValues={memberNameList}
              />
              <br />
              <label style={{ fontSize: fontSizePt }}>Payment amount:</label>
              <input
                className="form-control"
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
              <label style={{ fontSize: fontSizePt }}>Split type:</label>
              <Dropdown
                items={splitTypes}
                setSelectedValue={setSplitType}
                displayedValues={displayedSplitTypes}
              />
            </div>
            <div style={{ marginTop: "10px" }}>
              <TransactionRecipientsList
                key={`${payerId}-${splitType}`}
                recipients={members}
                ref={recipientPaymentsRef}
                payerId={payerId}
                splitType={splitType}
              />
            </div>
            <button
              type="submit"
              className="btn btn-success"
              style={{ marginTop: "10px" }}
            >
              Make Transaction
            </button>
          </form>
        </div>
      </>
    );
  } else if (group !== undefined && members.length <= 0) {
    return (
      <>
        <Header />
        <h3 className="pageContent">
          The group is empty. Please add some members first.
        </h3>
      </>
    );
  }
  return (
    <>
      <Header />
      <h1 className="pageContent">Loading...</h1>
    </>
  );
};

async function getData(setMembers, setGroup, groupId, setAmount, memberId) {
  const fetchedGroup = await fetchGroup(groupId);
  setGroup(fetchedGroup);
  const user = {
    id: 0,
    name: "Current",
    surname: "User",
    debt: 0,
    groupId: 0,
  };
  const members = [user, ...fetchedGroup.members];
  let amount = members.find((m) => m.id == memberId).debt;
  if (amount < 0) {
    amount *= -1;
  }
  setAmount(amount);
  setMembers(members);
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

async function postTransaction(transaction) {
  try {
    const response = await fetch(`${apiUrl}/api/Transactions`, {
      method: `POST`,
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(transaction),
    });

    if (!response.ok) {
      throw new Error("Transaction failed");
    }

    alert("Sucessfully made the transaction!");
  } catch (error) {
    console.log(error);
    alert("Transaction failed...");
  }
}

export default Transaction;
