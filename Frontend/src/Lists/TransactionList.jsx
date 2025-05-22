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
      let sender;
      if (t.senderId == 0) {
        sender = "User";
      } else {
        const mem = members.find((m) => m.id == t.senderId);
        sender = `${mem.name} ${mem.surname}`;
      }
      return (
        <div key={t.id}>
          <table>
            <thead>
              <th>Sender</th>
              <th>Amount</th>
              <th>Split type</th>
              <th>Recipient count</th>
            </thead>
            <tbody>
              <tr>
                <td>{t.date}</td>
                <td>
                  <i>{sender}</i>
                </td>
                <td>{t.amount}</td>
                <td>{t.recipients.length}</td>
              </tr>
            </tbody>
          </table>
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
