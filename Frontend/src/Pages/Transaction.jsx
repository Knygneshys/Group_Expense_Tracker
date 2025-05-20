import React from "react";
import { useParams } from "react-router-dom";

const Transaction = () => {
  const params = useParams();

  return (
    <div>
      {params.groupId} and {params.memberId}
    </div>
  );
};

export default Transaction;
