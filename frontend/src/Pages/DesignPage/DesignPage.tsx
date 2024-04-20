import React from 'react'
import Table from '../../Components/Table/Table'
import RatioList from '../../Components/RatioList/RatioList'
import { TestDataCompany } from '../../Components/Table/TestData';

interface Props  {
}
const data = TestDataCompany[0];

type CompanyData = typeof data;

const configs = [
    {
        label:"Company Name",
        render:(companyData:CompanyData) => companyData.companyName,
        subtitle:"This is the name of the company"
    },
];

const DesignPage = (props: Props) => {
  return <>
    <RatioList configs={configs} data = {data}/>
    <Table />
    </>
}

export default DesignPage