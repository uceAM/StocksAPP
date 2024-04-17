import { SyntheticEvent, useState } from 'react';
import './App.css';
import CardList from './Components/CardList/CardList';
import Search from './Components/Search/Search';
import { CompanySearch } from './company';
import { searchCompanies } from './api';
import ListPortfolio from './Components/Portfolio/ListPortfolio/ListPortfolio';
import Navbar from './Components/Navbar/Navbar';

function App() {
  const [search,setSearch] = useState<string>("");
  const [searchResult,setSearchResult] = useState<CompanySearch[]>([])
  const [serverError,setServerError] = useState<string>("");
  const [portfolioValues,setPortfolioValues] = useState<string[]>([]);
  const getSearchData =(val:string)=>{
      setSearch(val);
      // clickMe;
      // console.log(val);
  };
  const onSearchSubmit =  async (e:SyntheticEvent) =>{
    e.preventDefault();
    const result = await searchCompanies(search);
    if (typeof result === 'string'){
      setServerError(result);
    } else if (Array.isArray(result.data)){
      setSearchResult(result.data);
      setServerError("");
      console.log(searchResult);
    }
  };
  const onPortfolioCreate = (e:any)=> {
    e.preventDefault();
    let exists =portfolioValues.find(value=> value === e.target[0].value)
    if(exists) return;
    setPortfolioValues([...portfolioValues,e.target[0].value]);
  }
  const onPortfolioDelete = (e:any) => {
    e.preventDefault();
    setPortfolioValues(portfolioValues.filter(value=>value !== e.target[0].value));
  };
  return <>
    <Navbar/>
    <Search onSearchSubmit = {onSearchSubmit} search = {search} getSearchData ={getSearchData}/>
    <ListPortfolio portfolioValues = {portfolioValues} onPortfolioDelete ={onPortfolioDelete}/>
    <CardList searchResults ={searchResult} onPortfolioCreate={onPortfolioCreate}/>
    {serverError && <h1>{serverError}</h1>}
  </>;
}

export default App;
