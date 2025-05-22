import GroupList from "../Lists/GroupList.jsx";
import { Header } from "../Components/Header.jsx";
import { useParams } from "react-router-dom";
import { NavButtons } from "../Components/NavButtons.jsx";

const AllGroups = () => {
  document.title = `Group Finance Tracker`;
  const { pageNr: pageNrParam } = useParams();
  const parsedPageNr = parseInt(pageNrParam, 10);
  const pageNr = Number.isNaN(parsedPageNr) ? 0 : parsedPageNr;

  return (
    <>
      <Header />
      <div className="pageContent">
        <h1 className="groupHeader">Groups</h1>
        <div className="listDiv">
          <GroupList pageNr={pageNr} key={pageNr} />
        </div>
        <NavButtons pageNr={pageNr} navUrl={`/GroupList/`} />
      </div>
    </>
  );
};

export default AllGroups;
