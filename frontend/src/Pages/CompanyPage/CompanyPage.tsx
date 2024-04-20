import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { CompanyProfile } from '../../company';
import { getCompanyDetails } from '../../api';
import Sidebar from '../../Components/Sidebar/Sidebar';
import CompanyDashboard from '../../Components/CompanyDashboard/CompanyDashboard';
import Tile from '../../Components/Tile/Tile';

interface Props {}

const CompanyPage = (props: Props) => {
  let {ticker} = useParams();
  const [company,setCompany] = useState<CompanyProfile>();
  useEffect(()=>{
    const getCompanyProfile = async() => {
      const companyDetails= await getCompanyDetails(ticker!);
      setCompany(companyDetails?.data[0]);
    }
    getCompanyProfile();
    console.log('UseEffect');
  },[ticker]);
  
  return (
    <>{company?(
      <div className="w-full relative flex ct-docs-disable-sidebar-content overflow-x-hidden">
        <Sidebar />
        <CompanyDashboard> <Tile title='Company Name' details={company.companyName}></Tile></CompanyDashboard>
        </div>
    ):
    <div>No such Company found</div>}</>
  )
}

export default CompanyPage