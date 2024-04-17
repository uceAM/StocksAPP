import { SyntheticEvent } from 'react'

type Props = {
  onSearchSubmit: (e:SyntheticEvent) => void
  search:string | undefined
  getSearchData: (val:string) => void
};

const Search = ({onSearchSubmit,search,getSearchData}: Props) => {

  return (
    <>
      <form onSubmit={onSearchSubmit}>
        <input value ={search} onChange={e=>getSearchData(e.target.value)}></input>
        {/* <button onClick={(e) => clickMe(e)}>Button</button> */}
        </form>
    </>
  )
}

export default Search