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
      let message = `${r.name} ${r.surname}. Current debt: ${r.debt}${EURO}.`;
      if (r.id == 0) {
        const debt = allRecipients.find((r) => r.id == payerId).debt;
        message =
          debt > 0
            ? `User (you). Member owes you ${debt}${EURO}`
            : `User (you). In debt to member for: ${debt}${EURO}`;
      }
      return (
        <div key={r.id} style={{ margin: "7px" }} className="row mb-3">
          <div className="col-12 text-center mb-2">
            <label className="form-label">{message}</label>
          </div>
          <div className="col-12 d-flex justify-content-center">
            <div style={{ width: "200px" }}>
              <SplitTypeInput
                handleValueChange={handleValueChange}
                index={index}
                placeholder={placeholder}
              />
            </div>
          </div>
        </div>
      );
    }

    return <div key={r.id}></div>; // returns empty fragment when one of the group's members is initiating the payment
  });
};

export default SplitTypeRecipientList;
