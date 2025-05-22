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
        <div className="d-flex justify-content-between align-items-center mb-3">
          <div></div>
          <h3 className="mb-0">Group's transactions</h3>
          <button onClick={toggleDialog} className="btn btn-primary">
            Close
          </button>
        </div>
        {transactionList}
        <button onClick={toggleDialog} className="btn btn-primary">
          Close
        </button>
      </>
    );
  }
);

export default TransactionDialogContent;
