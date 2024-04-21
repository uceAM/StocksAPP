import React, { useEffect, useState } from 'react'
import { CompanyIncomeStatement } from '../../company';
import { getIncomeStatement } from '../../api';
import { useOutletContext } from 'react-router';
import Table from '../Table/Table';
import Spinner from '../Spinner/Spinner';
import { formatLargeMonetaryNumber, formatRatio } from "../../Helpers/NumberFormatting";

interface Props {}
const configs = [
  {
    label: "Date",
    render: (company: CompanyIncomeStatement) => company.date,
  },
  {
    label: "Revenue",
    render: (company: CompanyIncomeStatement) => formatLargeMonetaryNumber(company.revenue),
  },
  {
    label: "Cost Of Revenue",
    render: (company: CompanyIncomeStatement) => formatLargeMonetaryNumber(company.costOfRevenue),
  },
  {
    label: "Depreciation",
    render: (company: CompanyIncomeStatement) => formatLargeMonetaryNumber(company.depreciationAndAmortization),
  },
  {
    label: "Operating Income",
    render: (company: CompanyIncomeStatement) => formatLargeMonetaryNumber(company.operatingIncome),
  },
  {
    label: "Income Before Taxes",
    render: (company: CompanyIncomeStatement) => formatLargeMonetaryNumber(company.incomeBeforeTax),
  },
  {
    label: "Net Income",
    render: (company: CompanyIncomeStatement) => formatLargeMonetaryNumber(company.netIncome),
  },
  {
    label: "Net Income Ratio",
    render: (company: CompanyIncomeStatement) => formatRatio(company.netIncomeRatio),
  },
  {
    label: "Earnings Per Share",
    render: (company: CompanyIncomeStatement) => formatRatio(company.eps),
  },
  {
    label: "Earnings Per Diluted",
    render: (company: CompanyIncomeStatement) => formatRatio(company.epsdiluted),
  },
  {
    label: "Gross Profit Ratio",
    render: (company: CompanyIncomeStatement) => formatRatio(company.grossProfitRatio),
  },
  {
    label: "Opearting Income Ratio",
    render: (company: CompanyIncomeStatement) => formatRatio(company.operatingIncomeRatio),
  },
  {
    label: "Income Before Taxes Ratio",
    render: (company: CompanyIncomeStatement) => formatRatio(company.incomeBeforeTaxRatio),
  },
];


const IncomeStatements = (props: Props) => {
  const [companyIncome, setCompanyIncome] = useState<CompanyIncomeStatement[]>();
  const getIncome= async ()=>{
    const result = await getIncomeStatement(ticker);
    setCompanyIncome(result?.data)
  }
let ticker = useOutletContext<string>();
useEffect(()=>{
  getIncome();
},[ticker])
  return ( companyIncome?(
    <Table configs={configs} data={companyIncome} />
  ):(
    <Spinner />
  )
  )
}

export default IncomeStatements