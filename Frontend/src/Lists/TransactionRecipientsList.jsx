import React from "react";

const TransactionRecipientsList = ({ recipients }) => {
  const list = recipients.map((r) => (
    <div key={r.id}>
      <label>
        {r.name} {r.surname}. Current debt: {r.debt}.
      </label>
      <input
        type="number"
        required
        min={0}
        placeholder="Payment amount"
      ></input>
    </div>
  ));

  return list;
};

export default TransactionRecipientsList;
