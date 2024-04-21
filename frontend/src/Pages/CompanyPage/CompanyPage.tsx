import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { CompanyProfile } from '../../company';
import { getCompanyDetails } from '../../api';
import Sidebar from '../../Components/Sidebar/Sidebar';
import CompanyDashboard from '../../Components/CompanyDashboard/CompanyDashboard';
import Tile from '../../Components/Tile/Tile';
import Spinner from '../../Components/Spinner/Spinner';

interface Props {}

const CompanyPage = (props: Props) => {
  let {ticker} = useParams<string>();
  const [company,setCompany] = useState<CompanyProfile>();
  const getCompanyProfile = async() => {
    const companyDetails= await getCompanyDetails(ticker!);
    setCompany(companyDetails?.data[0]);
  }
  useEffect(()=>{
    getCompanyProfile();
  },[ticker]); //check
  
  return (
    <>
      {company? (
        <div className="w-full relative flex ct-docs-disable-sidebar-content overflow-x-hidden">
          <Sidebar />
          <CompanyDashboard ticker={ticker!}>
            <Tile title="Company Name" details={company.companyName}></Tile>
            <Tile title="CEO" details={company.ceo}></Tile>
            <Tile title="Price" details={company.price.toString()}></Tile>
            <Tile title="Changes" details={company.changes.toString()}></Tile>
          </CompanyDashboard>
        </div>
      ) : (
        <Spinner />
      )}
    </>
  );
}

export default CompanyPage