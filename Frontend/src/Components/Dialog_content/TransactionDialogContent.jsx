import { forwardRef } from "react";
import { TransactionList } from "../../Lists/TransactionList";

const TransactionDialogContent = forwardRef(
  ({ transactions, toggleDialog }, ref) => {
    let transactionList;
    if (transactions.length > 0) {
      transactionList = <TransactionList transactions={transactions} />;
    } else {
      transactionList = <h4>This group has no transactions</h4>;
    }
    return (
      <>
        <div>
          <h3>Group's transactions</h3>
        </div>
        {transactionList}
        <button onClick={toggleDialog} class="btn btn-primary">
          Close
        </button>
      </>
    );
  }
);

export default TransactionDialogContent;
