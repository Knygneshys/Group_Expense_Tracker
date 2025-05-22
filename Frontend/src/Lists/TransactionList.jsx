import React, { useEffect, useState } from "react";
import RecipientList from "./RecipientList";

const apiUrl = import.meta.env.VITE_API_URL;

export const TransactionList = ({ transactions }) => {
  const [members, setMembers] = useState(null);

  useEffect(() => {
    async function getData() {
      const members = await fetchMembers(transactions[0].groupId);
      setMembers(members);
    }
    getData();
  }, []);

  if (members !== undefined) {
    const list = transactions.map((t) => {
      console.log(t);
      return (
        <div key={t.id}>
          <h4>
            {t.date} Amount: {t.amount} Split type: {t.splitType} Recipients:{" "}
            {t.recipients.length}
          </h4>
          <RecipientList recipients={t.recipients} members={members} />
        </div>
      );
    });
    return <div>{list}</div>;
  }
  return <h2>Loading...</h2>;
};

async function fetchMembers(groupID) {
  try {
    const fetchUrl = `${apiUrl}/ByGroup/WithDeleted/${groupID}`;
    const response = await fetch(fetchUrl);
    if (!response.ok) {
      throw error(`Failed to fetch members by groupId: ${groupID}`);
    }
    const group = await response.json();

    return group;
  } catch (error) {
    conmsole.log(error);
  }
}
