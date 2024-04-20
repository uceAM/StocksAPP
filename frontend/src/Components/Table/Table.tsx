import React from "react";
import { testIncomeStatementData } from "./TestData";
import { v4 } from "uuid";

interface Props {}

const data = testIncomeStatementData;

type Company = (typeof data)[0];

const configs = [
  {
    label: "Date",
    render: (company: Company) => company.calendarYear,
  },
  {
    label: "Gross Profit",
    render: (company: Company) => company.grossProfit,
  },
];
const renderedHeaders = (
  <tr>
    {configs.map((val: any) => {
      return (
        <th
          className="p-4 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
          key={v4()}
        >
          {val.label}
        </th>
      );
    })}
  </tr>
);

const renderedRows = data.map((company: Company) => {
  return (
    <tr key={v4()}>
      {configs.map((val: any) => {
        return (
          <td className="p-3" key={v4()}>
            {val.render(company)}
          </td>
        );
      })}
    </tr>
  );
});

const Table = (props: Props) => {
  return (
    <div className="bg-white shadow rounded-lg p-4 sm:p-6 xl:p-8 ">
      <table className="min-w-full divide-y divide-gray-200 m-5">
        <thead className="bg-gray-50">{renderedHeaders}</thead>
        <tbody>{renderedRows}</tbody>
      </table>
    </div>
  );
};

export default Table;
