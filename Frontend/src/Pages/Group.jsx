import { useParams, useNavigate } from "react-router-dom";
import React, { useState, useEffect, useRef } from "react";
import MemberList from "../Lists/MemberList";
import MemberDialogContent from "../Components/Dialog_content/MemberDialogContent";
import TransactionDialogContent from "../Components/Dialog_content/TransactionDialogContent";
import { Header } from "../Components/Header";
import { NavButtons } from "../Components/NavButtons";
const apiUrl = import.meta.env.VITE_API_URL;
const EURO = import.meta.env.VITE_EURO;

const Group = () => {
  document.title = `Group Finance Tracker`;
  const params = useParams();
  const id = params.id;
  const [groupId, setGroupId] = useState();
  const [group, setGroup] = useState(null);
  const [debt, setDebt] = useState(null);
  const [members, setMembers] = useState([]);
  const [refresh, setRefresh] = useState(0);
  const [transactions, setTransactions] = useState([]);
  const memDialogRef = useRef(null);
  const tranasctionDialogRef = useRef(null);

  const { pageNr: pageNrParam } = useParams();
  const parsedPageNr = parseInt(pageNrParam, 10);
  const pageNr = Number.isNaN(parsedPageNr) ? 0 : parsedPageNr;

  const navigate = useNavigate();
  const goToTransaction = (groupId, memberId = 0) => {
    navigate(`/Transaction/${groupId}/${memberId}`);
  };

  const goBackAPage = (pageNr) => {
    navigate(`/Group/${groupId}/${--pageNr}`);
  };

  useEffect(() => {
    const parsedId = parseInt(id, 10);
    setGroupId(parsedId);
  }, []);

  useEffect(() => {
    async function getData() {
      if (Number.isInteger(groupId)) {
        const fetchedGroup = await fetchGroup(groupId);
        const fetchedMembers = await fetchMembers(groupId, pageNr);

        if (fetchedMembers.length <= 0) {
          goBackAPage(pageNr);
        }
        const fetchedDebt = await fetchDebt(groupId);
        const fetchedTransactions = await fetchTransactions(groupId);
        setTransactions(fetchedTransactions);
        setGroup(fetchedGroup);
        setDebt(fetchedDebt);
        if (
          fetchedGroup !== null &&
          JSON.stringify(members) !== JSON.stringify(fetchedMembers)
        ) {
          setMembers(fetchedMembers);
        }
      }
    }
    getData();
  }, [groupId, members, refresh, pageNr]);

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
    const buttonMargins = "15px";
    return (
      <>
        <Header />
        <div className="pageContent">
          <h1 className="groupHeader">{group.name}</h1>
          <h3>
            Current group debt: {debt.toFixed(2)}
            {EURO}
          </h3>
          <div className="listDiv">
            <MemberList
              key={pageNr}
              members={members}
              setRefresh={setRefresh}
              goToTransaction={goToTransaction}
            />
          </div>
          <button onClick={toggleMemDialog} className="btn btn-primary">
            Add new member
          </button>
          <button
            className="btn btn-success"
            style={{ marginLeft: buttonMargins, marginRight: buttonMargins }}
            onClick={() => {
              goToTransaction(groupId, 0);
            }}
          >
            Make transaction
          </button>
          <button onClick={toggleTransactionDialog} className="btn btn-info">
            Show transactions
          </button>
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
          <NavButtons pageNr={pageNr} navUrl={`/Group/${id}/`} />
        </div>
      </>
    );
  } else {
    return (
      <>
        <Header />
        <h1 className="pageContent">Loading...</h1>
      </>
    );
  }
};

async function fetchGroup(id) {
  try {
    const response = await fetch(`${apiUrl}/api/Groups/NoMem/${id}`);
    if (!response.ok) {
      throw new Error(`Failed to fetch group by id: ${id}`);
    }
    const data = await response.json();

    return data;
  } catch (error) {
    console.log(error);
  }
}

async function fetchMembers(groupId, pageNr) {
  try {
    const response = await fetch(`${apiUrl}/api/Members/${groupId}/${pageNr}`);
    if (!response.ok) {
      throw new Error(`Failed to fetch members from group by id: ${groupId}`);
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
