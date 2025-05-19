import { useParams } from "react-router-dom";
import React, { useState, useEffect, useRef } from "react";
import MemberList from "../Lists/MemberList";
import MemberDialogContent from "../Components/MemberDialogContent";
const apiUrl = import.meta.env.VITE_API_URL;

function Group() {
  const { id } = useParams();
  const [groupId, setGroupId] = useState();
  const [group, setGroup] = useState(null);
  const [debt, setDebt] = useState(null);
  const [members, setMembers] = useState([]);
  const [refresh, setRefresh] = useState(0);

  const dialogRef = useRef(null);

  useEffect(() => {
    const parsedId = parseInt(id, 10);
    setGroupId(parsedId);
  }, []);

  useEffect(() => {
    async function getData() {
      if (Number.isInteger(groupId)) {
        const fetchedGroup = await fetchGroup(groupId);
        const fetchedDebt = await fetchDebt(groupId);
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

  function toggleDialog() {
    if (!dialogRef.current) {
      return;
    }
    dialogRef.current.hasAttribute("open")
      ? dialogRef.current.close()
      : dialogRef.current.showModal();
  }

  if (group !== null) {
    return (
      <div>
        <h1>{group.name}</h1>
        <h3>Current group debt: {debt.toFixed(2)}</h3>
        <MemberList members={members} />
        <button onClick={toggleDialog}>Add new member</button>
        <dialog ref={dialogRef}>
          <MemberDialogContent
            toggleDialog={toggleDialog}
            gId={groupId}
            setRefresh={setRefresh}
            ref={dialogRef}
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
}

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

export default Group;
