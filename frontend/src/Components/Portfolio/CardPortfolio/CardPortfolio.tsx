import { SyntheticEvent } from "react"
import DeletePortfolio from "../DeletePortfolio/DeletePortfolio"

interface Props {
    portfolioValue: string
    onPortfolioDelete:(e:SyntheticEvent) => void
}

const CardPorfolio = ({portfolioValue,onPortfolioDelete}: Props) => {
  return <>
    <h4>{portfolioValue}</h4>
    <DeletePortfolio portfolioValue = {portfolioValue} onPortfolioDelete={onPortfolioDelete} />
    </>
}

export default CardPorfolio