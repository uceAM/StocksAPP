import { SyntheticEvent } from 'react'
import { CompanySearch } from '../../company';
import AddPortfolio from '../Portfolio/AddPortfolio/AddPortfolio';

interface Props {
    id: string;
    result: CompanySearch;
    onPortfolioCreate:(e:SyntheticEvent)=>void
}

const Card = ({id, result,onPortfolioCreate}: Props) => {
  return <div className="card">
        <img alt = "logo" />
        <div className='details'>
            <h2>{result.name} ({result.symbol})</h2>
            <p>{result.currency}</p>
        </div>
        <p>{result.exchangeShortName} - {result.stockExchange}</p>
        <AddPortfolio onPortfolioCreate={onPortfolioCreate} symbol ={result.symbol}/>
    </div>
}

export default Card