import React, { SyntheticEvent } from 'react'

interface Props {
    portfolioValue:string;
    onPortfolioDelete:(e:SyntheticEvent) => void
}

const DeletePortfolio = ({portfolioValue,onPortfolioDelete}: Props) => {
  return (
    <form onSubmit={onPortfolioDelete}>
        <input hidden readOnly value={portfolioValue} />
        <button type = "submit" className="block w-full py-3 text-white duration-200 border-2 rounded-lg bg-red-500 hover:text-red-500 hover:bg-white border-red-500">
          X
        </button>
    </form>
  )
}

export default DeletePortfolio