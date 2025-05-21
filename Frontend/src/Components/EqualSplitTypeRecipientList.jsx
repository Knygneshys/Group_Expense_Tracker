import React from "react";

const EURO = import.meta.env.VITE_EURO;

const EqualSplitTypeRecipientList = ({ recipients, payerId }) => {
  const groupMembers = recipients.length;
  const payerIsNotGroupMember = payerId == 0; // payerId == 0 means that this is the user paying, not someone from the group
  const count = payerIsNotGroupMember ? groupMembers : groupMembers - 1;
  const list = recipients.map((r) => {
    if (r.id == payerId) {
      return <div key={r.id}></div>;
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
      <h4>Amount will be equally split {count} ways to:</h4>
      {list}
    </div>
  );
};

export default EqualSplitTypeRecipientList;
