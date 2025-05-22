import React, { useEffect, useState } from "react";
import RecipientList from "./RecipientList";

const apiUrl = import.meta.env.VITE_API_URL;
const EURO = import.meta.env.VITE_EURO;

export const TransactionList = ({ transactions }) => {
  const [members, setMembers] = useState([]);

  useEffect(() => {
    async function getData() {
      const members = await fetchMembers(transactions[0].groupId);
      setMembers(members);
    }
    getData();
  }, []);

  if (members !== undefined) {
    const list = transactions.map((t) => {
      let sender;
      if (t.senderId == 0) {
        sender = "User";
      } else {
        const mem = members.find((m) => m.id == t.senderId);
        if (mem != null) {
          sender = `${mem.name} ${mem.surname}`;
        } else {
          sender = `Sender ID: ${t.senderId}`;
        }
      }
      let splitType;
      switch (t.splitType) {
        case "E":
          splitType = "Equal";
          break;
        case "P":
          splitType = "Percentage";
          break;
        default:
          splitType = "Dynamic";
      }

      return (
        <div key={t.id}>
          <table>
            <thead>
              <tr>
                <th>Date</th>
                <th>Sender</th>
                <th>Amount</th>
                <th>Split type</th>
                <th>Recipient count</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>{t.date}</td>
                <td>
                  <i>{sender}</i>
                </td>
                <td>
                  {t.amount}
                  {EURO}
                </td>
                <td>{splitType}</td>
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
    const fetchUrl = `${apiUrl}/api/Members/ByGroup/${groupID}`;

    const response = await fetch(fetchUrl);
    if (!response.ok) {
      throw error(`Failed to fetch members by groupId: ${groupID}`);
    }
    const members = await response.json();

    return members;
  } catch (error) {
    console.log(error);
  }
}
