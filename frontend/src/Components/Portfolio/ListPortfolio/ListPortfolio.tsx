import { SyntheticEvent } from 'react';
import CardPortfolio from '../CardPortfolio/CardPortfolio'
import {v4 as uuidv4} from 'uuid';

interface Props {
    portfolioValues: string[];
    onPortfolioDelete: (e: SyntheticEvent) => void;
}

const ListPortfolio = ({portfolioValues,onPortfolioDelete}: Props) => {
  return <>
        <h2>My Portfolio</h2>
        <ul>
            {portfolioValues.length> 0 ? portfolioValues.map(value=> <CardPortfolio  key={uuidv4()} portfolioValue = {value} onPortfolioDelete ={onPortfolioDelete}/>):<h3>Empty!</h3>}
            
        </ul>
    </>
}

export default ListPortfolio