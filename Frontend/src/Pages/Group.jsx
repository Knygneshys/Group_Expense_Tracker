import { useParams, useNavigate } from "react-router-dom";
import React, { useState, useEffect, useRef } from "react";
import MemberList from "../Lists/MemberList";
import MemberDialogContent from "../Components/Dialog_content/MemberDialogContent";
import TransactionDialogContent from "../Components/Dialog_content/TransactionDialogContent";

const apiUrl = import.meta.env.VITE_API_URL;
const EURO = import.meta.env.VITE_EURO;

const Group = () => {
  const { id } = useParams();
  const [groupId, setGroupId] = useState();
  const [group, setGroup] = useState(null);
  const [debt, setDebt] = useState(null);
  const [members, setMembers] = useState([]);
  const [refresh, setRefresh] = useState(0);
  const [transactions, setTransactions] = useState([]);
  const memDialogRef = useRef(null);
  const tranasctionDialogRef = useRef(null);

  const navigate = useNavigate();
  const goToTransaction = (groupId, memberId = 0) => {
    navigate(`/Transaction/${groupId}/${memberId}`);
  };

  useEffect(() => {
    const parsedId = parseInt(id, 10);
    setGroupId(parsedId);
  }, []);

  useEffect(() => {
    async function getData() {
      if (Number.isInteger(groupId)) {
        const fetchedGroup = await fetchGroup(groupId);
        const fetchedDebt = await fetchDebt(groupId);
        const fetchedTransactions = await fetchTransactions(groupId);
        setTransactions(fetchedTransactions);
        setGroup(fetchedGroup);
        setDebt(fetchedDebt);
        if (
          fetchedGroup !== null &&
          JSON.stringify(members) !== JSON.stringify(fetchedGroup.members)
        ) {
          setMembers(fetchedGroup.members);
        }
      }
    }
    getData();
  }, [groupId, members, refresh]);

  function toggleMemDialog() {
    if (!memDialogRef.current) {
      return;
    }
    memDialogRef.current.hasAttribute("open")
      ? memDialogRef.current.close()
      : memDialogRef.current.showModal();
  }

  function toggleTransactionDialog() {
    if (!tranasctionDialogRef.current) {
      return;
    }
    tranasctionDialogRef.current.hasAttribute("open")
      ? tranasctionDialogRef.current.close()
      : tranasctionDialogRef.current.showModal();
  }

  if (group !== null) {
    return (
      <div>
        <h1>{group.name}</h1>
        <h3>
          Current group debt: {debt.toFixed(2)}
          {EURO}
        </h3>
        <MemberList
          members={members}
          setRefresh={setRefresh}
          goToTransaction={goToTransaction}
        />
        <button onClick={toggleMemDialog}>Add new member</button>
        <button
          onClick={() => {
            goToTransaction(groupId, 0);
          }}
        >
          Make transaction
        </button>
        <button onClick={toggleTransactionDialog}>Show transactions</button>
        <dialog ref={memDialogRef}>
          <MemberDialogContent
            toggleDialog={toggleMemDialog}
            gId={groupId}
            setRefresh={setRefresh}
            ref={memDialogRef}
          />
        </dialog>
        <dialog ref={tranasctionDialogRef}>
          <TransactionDialogContent
            key={refresh}
            transactions={transactions}
            toggleDialog={toggleTransactionDialog}
            ref={tranasctionDialogRef}
          />
        </dialog>
      </div>
    );
  } else {
    return (
      <>
        <h1>Loading...</h1>
      </>
    );
  }
};

async function fetchGroup(id) {
  try {
    const response = await fetch(`${apiUrl}/api/Groups/${id}`);
    if (!response.ok) {
      throw new Error(`Failed to fetch group by id: ${id}`);
    }
    const data = await response.json();

    return data;
  } catch (error) {
    console.log(error);
  }
}

async function fetchDebt(id) {
  try {
    const response = await fetch(`${apiUrl}/api/Groups/GroupDebt/${id}`);
    if (!response.ok) {
      throw new Error("Failed to fetch group debt");
    }
    const data = await response.json();

    return data;
  } catch (error) {
    console.log(error);
  }
}

async function fetchTransactions(groupId) {
  try {
    const response = await fetch(`${apiUrl}/api/Transactions/${groupId}`);
    if (!response.ok) {
      throw new Error(`Failed to fetch group id's: ${groupId} transactions`);
    }
    const data = await response.json();

    return data;
  } catch (error) {
    console.log(error);
  }
}

export default Group;
