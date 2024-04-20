import React from 'react'
import { v4 } from 'uuid';

interface Props {
  configs:any
  data:any
}

const RatioList = ({configs,data}: Props) => {
  const renderedRows = configs.map((config: any) => {
    return (
      <li key={v4()}className="py-6 sm:py-6">
        <div className="flex items-center space-x-4">
          <div className="flex-1 min-w-0">
            <p className="text-l font-extrabold text-slate-600 truncate">
              {config.label}
            </p>
            <p className="text-sm text-gray-500 truncate">
            {config.subtitle && config.subtitle}
            </p>
          </div>
          <div className="inline-flex items-center text-base font-semibold text-black">
          {config.render(data)}
          </div>
        </div>
      </li>
    );
  });
    return (
        <div className="bg-white shadow rounded-lg ml-4 mt-4 mb-4 p-4 sm:p-6 w-full">
          <ul className="divide-y divide-gray-200">{renderedRows}</ul>
        </div>
    );
}

export default RatioList