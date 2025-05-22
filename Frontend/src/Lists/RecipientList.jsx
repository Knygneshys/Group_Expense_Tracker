import React from "react";

const EURO = import.meta.env.VITE_EURO;

const RecipientList = ({ recipients, members }) => {
  const list = recipients.map((r) => {
    const mem = members.find((m) => m.id == r.id);
    let memName, memSurname;
    if (mem == null) {
      memName = "Current";
      memSurname = "User";
    } else {
      memName = mem.name;
      memSurname = mem.surname;
    }
    return (
      <tr key={r.id}>
        <td>{memName}</td>
        <td>{memSurname}</td>
        <td>
          {r.payment}
          {EURO}
        </td>
      </tr>
    );
  });

  if (list != null) {
    return (
      <table>
        <thead>
          <tr>
            <td>Name</td>
            <td>Surname</td>
            <td>Payment</td>
          </tr>
        </thead>
        <tbody>{list}</tbody>
      </table>
    );
  }
  return <h4>Loading...</h4>;
};

export default RecipientList;
