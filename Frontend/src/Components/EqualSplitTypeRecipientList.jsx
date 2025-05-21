import React from "react";

const EURO = import.meta.env.VITE_EURO;

const EqualSplitTypeRecipientList = ({ recipients, payerId }) => {
  const list = recipients.map((r) => {
    if (r.id == payerId) {
      const message =
        r.debt > 0
          ? `User (you). Member owes you ${r.debt}${EURO}`
          : `User (you). In debt to member for: ${r.debt}${EURO}`;
      return <div key={0}>{message}</div>;
    }

    return (
      <div key={r.id}>
        {r.name} {r.surname} {r.debt}
        {EURO}
      </div>
    );
  });
  return (
    <div>
      <h4>Amount will be equally split {recipients.length} ways to:</h4>
      {list}
    </div>
  );
};

export default EqualSplitTypeRecipientList;
