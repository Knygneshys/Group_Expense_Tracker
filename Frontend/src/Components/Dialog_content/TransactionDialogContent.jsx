import { forwardRef } from "react";
import { TransactionList } from "../../Lists/TransactionList";

const TransactionDialogContent = forwardRef(
  ({ transactions, toggleDialog }, ref) => {
    return (
      <>
        <div>
          <h3>Groups transactions</h3>
        </div>
        <TransactionList transactions={transactions} />
        <button onClick={toggleDialog}>Close</button>
      </>
    );
  }
);

export default TransactionDialogContent;
