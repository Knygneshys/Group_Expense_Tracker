import React from "react";
import SplitTypeInput from "./Inputs/SplitTypeInput";

const SplitTypeRecipientList = ({
  recipients,
  placeholder,
  handleValueChange,
  payerId,
}) => {
  return recipients.map((r, index) => {
    if (r.id != payerId) {
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
    }

    return <div key={r.id}></div>; // returns empty fragment when one of the group's members is initiating the payment
  });
};

export default SplitTypeRecipientList;
