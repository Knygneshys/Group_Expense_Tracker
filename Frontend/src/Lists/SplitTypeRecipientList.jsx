import React from "react";
import SplitTypeInput from "../Components/SplitTypeInput";

const EURO = import.meta.env.VITE_EURO;

const SplitTypeRecipientList = ({
  recipients,
  allRecipients,
  placeholder,
  handleValueChange,
  payerId,
}) => {
  return recipients.map((r, index) => {
    if (r.id != payerId) {
      let message = `${r.name} ${r.surname}. Current debt: ${r.debt}.`;
      if (r.id == 0) {
        const debt = allRecipients.find((r) => r.id == payerId).debt;
        message =
          debt > 0
            ? `User (you). Member owes you ${debt}${EURO}`
            : `User (you). In debt to member for: ${debt}${EURO}`;
      }
      return (
        <div key={r.id}>
          <label>{message}</label>
          <SplitTypeInput
            handleValueChange={handleValueChange}
            index={index}
            placeholder={placeholder}
          />
        </div>
      );
    }

    return <div key={r.id}></div>; // returns empty fragment when one of the group's members is initiating the payment
  });
};

export default SplitTypeRecipientList;
