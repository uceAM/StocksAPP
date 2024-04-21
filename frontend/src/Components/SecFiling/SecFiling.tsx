import React, { useEffect, useState } from "react";
import { CompanySecFiling } from "../../company";
import { getSecFiling } from "../../api";
import SecFilingItem from "./SecFilingItem/SecFilingItem";
import { v4 } from "uuid";
import Spinner from "../Spinner/Spinner";

interface Props {
  ticker: string;
}

const SecFiling = ({ ticker }: Props) => {
  const [companySec, setCompanySec] = useState<CompanySecFiling[]>();
  const getSec = async () => {
    const result = await getSecFiling(ticker);
    setCompanySec(result?.data);
  };
  useEffect(() => {
    getSec();
  }, [ticker]);
  return Array.isArray(companySec) ? (
    <div className="inline-flex rounded-md space-x-reverse shadow-sm m-4">
      {companySec.slice(0, 5).map((val) => (
        <SecFilingItem key={v4()} secFiling={val} />
      ))}
    </div>
  ) : (
    <Spinner />
  );
};

export default SecFiling;
