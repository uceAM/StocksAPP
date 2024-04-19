import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { CompanyProfile } from '../../company';
import { getCompanyDetails } from '../../api';

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
  },[]);
  
  return (
    <>{company?(
      <div>{company.companyName}</div>
    ):
    <div>No such Company found</div>}</>
  )
}

export default CompanyPage