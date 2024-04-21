import React, { useEffect, useState } from "react";
import { CompanyCashFlow } from "../../company";
import { useOutletContext } from "react-router";
import { getCashFlow } from "../../api";
import Table from "../Table/Table";

interface Props {}

const config = [
  {
    label: "Date",
    render: (company: CompanyCashFlow) => company.date,
  },
  {
    label: "Operating Cashflow",
    render: (company: CompanyCashFlow) => company.operatingCashFlow,
  },
  {
    label: "Investing Cashflow",
    render: (company: CompanyCashFlow) =>
      company.netCashUsedForInvestingActivites,
  },
  {
    label: "Financing Cashflow",
    render: (company: CompanyCashFlow) =>
      company.netCashUsedProvidedByFinancingActivities,
  },
  {
    label: "Cash At End of Period",
    render: (company: CompanyCashFlow) => company.cashAtEndOfPeriod,
  },
  {
    label: "CapEX",
    render: (company: CompanyCashFlow) => company.capitalExpenditure,
  },
  {
    label: "Issuance Of Stock",
    render: (company: CompanyCashFlow) => company.commonStockIssued,
  },
  {
    label: "Free Cash Flow",
    render: (company: CompanyCashFlow) => company.freeCashFlow,
  },
];

const CashflowStatement = (props: Props) => {
  const [companyCashflow, setCompanyCashflow] = useState<CompanyCashFlow[]>();
  let ticker = useOutletContext<string>();
  const getCompanyCashflow = async () => {
    const result = await getCashFlow(ticker);
    setCompanyCashflow(result?.data);
  };
  useEffect(() => {
    getCompanyCashflow();
  }, [ticker]);
  return companyCashflow ? (
    <Table configs={config} data={companyCashflow} />
  ) : (
    <h1>No company Found</h1>
  );
};

export default CashflowStatement;