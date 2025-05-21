import React from "react";
import SplitTypeInput from "./Inputs/SplitTypeInput";

const EURO = import.meta.env.VITE_EURO;

const SplitTypeRecipientList = ({
  recipients,
  placeholder,
  handleValueChange,
  payerId,
}) => {
  return recipients.map((r, index) => {
    if (r.id != payerId && r.id != 0) {
      return (
        <div key={r.id}>
          <label>
            {r.name} {r.surname}. Current debt: {r.debt}.
          </label>
          <SplitTypeInput
            handleValueChange={handleValueChange}
            index={index}
            placeholder={placeholder}
          />
        </div>
      );
    } else if (r.id != payerId && r.id == 0) {
      const debt = recipients.find((r) => r.id == payerId).debt;
      const message =
        debt > 0
          ? `User (you). Member owes you ${debt}${EURO}`
          : `User (you). In debt to member for: ${debt}${EURO}`;
      return <div key={r.id}>{message}</div>;
    }

    return <div key={r.id}></div>; // returns empty fragment when one of the group's members is initiating the payment
  });
};

export default SplitTypeRecipientList;
