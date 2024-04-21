import React from "react";
import { CompanySecFiling } from "../../../company";
import { Link } from "react-router-dom";

interface Props {
  secFiling: CompanySecFiling;
}

const SecFilingItem = ({ secFiling }: Props) => {
  const year = new Date(secFiling.fillingDate).getFullYear();
  return (
    <Link
        to={secFiling.finalLink}
        type="button"
        className="inline-flex items-center px-5 py-2 text-sm font-medium text-white bg-lightGreen border border-gray-200 rounded hover:bg-gray-100 hover:text-blue-700 focus:z-10 focus:ring-2 focus:ring-blue-700 focus:text-blue-700 dark:bg-gray-700 dark:border-gray-600 dark:text-white dark:hover:text-white dark:hover:bg-gray-600 dark:focus:ring-blue-500 dark:focus:text-white"
        >
      SEC-{secFiling.symbol}-{year}
    </Link>
  );
};

export default SecFilingItem;
