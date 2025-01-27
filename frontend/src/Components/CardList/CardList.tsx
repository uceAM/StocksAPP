import { SyntheticEvent } from 'react'
import { CompanySearch } from './../../company';
import Card from '../Card/Card'
import {v4 as uuidv4} from 'uuid';

interface Props  {
  searchResults : CompanySearch[];
  onPortfolioCreate:(e:SyntheticEvent)=>void
}

const CardList = ({searchResults,onPortfolioCreate}: Props) => {
  return (
    <div>
    {searchResults.length > 0 ? (
        searchResults.map((result)=> {
          return <Card id = {result.symbol} key={uuidv4()} result = {result} onPortfolioCreate={onPortfolioCreate} />
        })
      ):(
        <p className="mb-3 mt-3 text-xl font-semibold text-center md:text-xl">
        No Result
      </p>
      )}
    </div>
  ) 
}

export default CardList