import React, { useEffect, useState } from "react";

const TransactionRecipientsList = ({ recipients }) => {
  const [payments, setPayments] = useState();

  useEffect(() => {
    const initialPayments = recipients.map((r) => ({
      recipientId: r.id,
      amount: 0,
    }));
    setPayments(initialPayments);
  }, [recipients]);

  const handlePaymentChange = (index, amount) => {
    const newPayments = [...payments];
    newPayments[index].amount = amount;
    setPayments(newPayments);

    console.log(payments);
  };

  const list = recipients.map((r, index) => (
    <div key={r.id}>
      <label>
        {r.name} {r.surname}. Current debt: {r.debt}.
      </label>
      <input
        name="amount"
        type="number"
        required
        min={0}
        placeholder="0"
        onChange={(e) =>
          handlePaymentChange(index, parseFloat(e.target.value) || 0)
        }
      ></input>
    </div>
  ));

  return list;
};

export default TransactionRecipientsList;
