import React from "react";

const EURO = import.meta.env.VITE_EURO;
const fontSizePt = "15pt";
const EqualSplitTypeRecipientList = ({
  recipients,
  allRecipients,
  payerId,
}) => {
  const debt = allRecipients.find((r) => r.id == payerId).debt;
  const filteredRecipients = recipients.filter((r) => r.id != payerId);

  const list = filteredRecipients.map((r) => {
    if (r.id == 0 && payerId != 0) {
      const message =
        debt > 0
          ? `User (you). Member owes you ${debt}${EURO}`
          : `User (you). In debt to member for: ${debt}${EURO}`;
      return (
        <div key={r.id} style={{ fontSize: fontSizePt }}>
          {message}
        </div>
      );
    }

    return (
      <div key={r.id} style={{ fontSize: fontSizePt }}>
        {r.name} {r.surname} {r.debt}
        {EURO}
      </div>
    );
  });
  return (
    <div>
      <h4>Amount will be equally split {filteredRecipients.length} ways to:</h4>
      {list}
    </div>
  );
};

export default EqualSplitTypeRecipientList;
