import React from 'react'
import { TestDataCompany } from '../Table/TestData'

interface Props {}

const data = TestDataCompany[0];

type CompanyData = typeof data;

const configs = [
    {
        label:"Company Name",
        render:(companyData:CompanyData) => companyData.companyName,
        subtitle:"This is the name of the company"
    },
];

const renderedRows = configs.map((config: any) => {
  return (
    <li className="py-6 sm:py-6">
      <div className="flex items-center space-x-4">
        <div className="flex-1 min-w-0">
          <p className="text-sm font-medium text-gray-900 truncate">
            {config.label}
          </p>
          <p className="text-sm text-gray-500 truncate">
          {config.subtitle && config.subtitle}
          </p>
        </div>
        <div className="inline-flex items-center text-base font-semibold text-gray-900">
        {config.render(data)}
        </div>
      </div>
    </li>
  );
}); 

const RatioList = (props: Props) => {
    return (
        <div className="bg-white shadow rounded-lg ml-4 mt-4 mb-4 p-4 sm:p-6 w-full">
          <ul className="divide-y divide-gray-200">{renderedRows}</ul>
        </div>
    );
}

export default RatioList