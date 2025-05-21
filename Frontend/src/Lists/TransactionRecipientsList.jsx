import React, {
  forwardRef,
  useEffect,
  useImperativeHandle,
  useState,
} from "react";
import EqualSplitTypeRecipientList from "../Components/EqualSplitTypeRecipientList";
import SplitTypeInput from "../Components/Inputs/SplitTypeInput";
import SplitTypeRecipientList from "../Components/SplitTypeRecipientList";

const EURO = import.meta.env.VITE_EURO;

const TransactionRecipientsList = forwardRef(
  ({ recipients, payerId, splitType }, ref) => {
    const [payments, setPayments] = useState([]);
    const [filteredRecipients, setFilteredRecipients] = useState([]);

    useEffect(() => {
      const filtered = recipients.filter((r) => r.id != payerId);
      setFilteredRecipients(filtered);
      const initialPayments = filtered.map((r) => ({
        recipientId: r.id,
        payment: 0,
      }));
      setPayments(initialPayments);
    }, [recipients, payerId]);

    useImperativeHandle(ref, () => ({
      getPayments: () => payments,
    }));

    const handlePaymentChange = (index, amount) => {
      const newPayments = [...payments];
      newPayments[index].payment = amount;
      setPayments(newPayments);
      ref = payments;
    };

    let list;
    switch (splitType) {
      case "E":
        list = (
          <EqualSplitTypeRecipientList
            recipients={filteredRecipients}
            allRecipients={recipients}
            payerId={payerId}
          />
        );
        break;
      case "P":
        list = (
          <SplitTypeRecipientList
            recipients={filteredRecipients}
            allRecipients={recipients}
            placeholder={`% of the payment amount`}
            handleValueChange={handlePaymentChange}
            payerId={payerId}
          />
        );
        break;
      default:
        list = (
          <SplitTypeRecipientList
            recipients={filteredRecipients}
            allRecipients={recipients}
            placeholder={`0${EURO}`}
            handleValueChange={handlePaymentChange}
            payerId={payerId}
          />
        );
    }

    return list;
  }
);

export default TransactionRecipientsList;
