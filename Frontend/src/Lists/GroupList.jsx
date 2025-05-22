import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import NewGroupAddition from "../Components/NewGroupInserter";

const apiUrl = import.meta.env.VITE_API_URL;
const EURO = import.meta.env.VITE_EURO;

const GroupList = () => {
  const [groups, setGroups] = useState("empty");
  const [groupDebts, setGroupDebts] = useState("empty");
  const [refresh, setRefresh] = useState(0);

  useEffect(() => {
    async function getData() {
      setGroups("empty");
      setGroupDebts("empty");
      const data = await fetchGroups();
      setGroups(data);

      if (data && Array.isArray(data)) {
        const debtArray = {};
        for (let i = 0; i < data.length; i++) {
          const groupId = data[i].id;
          const debt = await fetchGroupDebts(groupId);
          debtArray[groupId] = debt;
        }
        setGroupDebts(debtArray);
      }
    }
    getData();
  }, [refresh]);

  const navigate = useNavigate();

  const goToGroup = (groupId) => {
    navigate(`/Group/${groupId}`);
  };

  if (groups != "empty" && groupDebts != "empty") {
    const list = groups.map((group) => (
      <tr key={group.id}>
        <td>{group.name}</td>
        <td>
          {groupDebts[group.id].toFixed(2)}
          {EURO}
        </td>
        <td>
          <button onClick={() => goToGroup(group.id)}>Open</button>
        </td>
      </tr>
    ));
    return (
      <div>
        <table className="table table-striped table-hover table-bordered">
          <thead>
            <tr>
              <th>Name</th>
              <th>Debt</th>
            </tr>
          </thead>
          <tbody>{list}</tbody>
        </table>
        <NewGroupAddition refreshDad={setRefresh} />
      </div>
    );
  }

  return <div>Loading...</div>;
};

async function fetchGroups() {
  try {
    const response = await fetch(`${apiUrl}/api/Groups`);

    if (!response.ok) {
      throw new Error("Could not fetch groups");
    }

    const data = await response.json();

    return data;
  } catch (error) {
    console.log(error);
  }
}

async function fetchGroupDebts(id) {
  try {
    const response = await fetch(`${apiUrl}/api/Groups/GroupDebt/${id}`);
    if (!response.ok) {
      throw new Error(`Failed to receive group nr: ${id} debts`);
    }

    const debt = await response.json();
    return debt;
  } catch (error) {
    console.log(error);
  }
}

export default GroupList;
