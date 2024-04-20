import React from 'react'
import Table from '../../Components/Table/Table'
import RatioList from '../../Components/RatioList/RatioList'
import { TestDataCompany, testIncomeStatementData } from '../../TestData';

interface Props  {
}
const listData = TestDataCompany[0];

type CompanyData = typeof listData;

const listConfig = [
    {
        label:"Company Name",
        render:(companyData:CompanyData) => companyData.companyName,
        subtitle:"This is the name of the company"
    },
];

const tableData = testIncomeStatementData;

type Company = (typeof tableData)[0];

const tableConfig = [
  {
    label: "Date",
    render: (company: Company) => company.calendarYear,
  },
  {
    label: "Gross Profit",
    render: (company: Company) => company.grossProfit,
  },
];

const DesignPage = (props: Props) => {
  return <>
    <RatioList configs={listConfig} data = {listData}/>
    <Table configs={tableConfig} data = {tableData}/>
    </>
}

export default DesignPage