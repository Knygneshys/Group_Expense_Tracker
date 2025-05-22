import React from "react";
import { useNavigate } from "react-router-dom";

export const NavButtons = ({ pageNr, navUrl }) => {
  const margins = "10px";

  const navigate = useNavigate();
  const goToUrl = () => {
    navigate(`${navUrl}${pageNr}`);
  };

  if (pageNr == 0) {
    return (
      <div>
        <span>{pageNr + 1}</span>
        <button
          className="btn btn-link"
          style={{ marginLeft: margins, marginRight: margins }}
          onClick={() => {
            pageNr++;
            goToUrl();
          }}
        >
          {pageNr + 2}
        </button>
        <button
          className="btn btn-link"
          onClick={() => {
            pageNr += 2;
            goToUrl();
          }}
        >
          {pageNr + 3}
        </button>
      </div>
    );
  } else {
    return (
      <div>
        <button
          className="btn btn-link"
          style={{ marginLeft: margins, marginRight: margins }}
          onClick={() => {
            --pageNr;
            goToUrl();
          }}
        >
          {pageNr}
        </button>
        <span style={{ marginLeft: margins, marginRight: margins }}>
          {pageNr + 1}
        </span>
        <button
          className="btn btn-link"
          onClick={() => {
            pageNr++;
            goToUrl();
          }}
        >
          {pageNr + 2}
        </button>
      </div>
    );
  }

  return <div>NavButtons</div>;
};
