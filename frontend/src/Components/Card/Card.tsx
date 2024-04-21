import { SyntheticEvent } from 'react'
import { CompanySearch } from '../../company';
import AddPortfolio from '../Portfolio/AddPortfolio/AddPortfolio';
import { Link } from 'react-router-dom';

interface Props {
    id: string;
    result: CompanySearch;
    onPortfolioCreate:(e:SyntheticEvent)=>void
}

const Card = ({id, result,onPortfolioCreate}: Props) => {
  return <div
  className="flex flex-col items-center justify-between w-full p-6 bg-slate-100 rounded-lg md:flex-row"
  key={id}
  id={id}
>
  <Link to= {`/company/${result.symbol}/company-profile`} className="font-bold text-center text-black md:text-left">
    {result.name} ({result.symbol})
  </Link>
  <p className="text-black">{result.currency}</p>
  <p className="font-bold text-black">
    {result.exchangeShortName} - {result.stockExchange}</p>
        <AddPortfolio onPortfolioCreate={onPortfolioCreate} symbol ={result.symbol}/>
    </div>
}

export default Card