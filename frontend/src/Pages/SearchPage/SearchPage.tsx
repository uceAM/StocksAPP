import { SyntheticEvent, useState } from "react";
import CardList from "../../Components/CardList/CardList";
import Search from "../../Components/Search/Search";
import { CompanySearch } from "../../company";
import { searchCompanies } from "../../api";
import ListPortfolio from "../../Components/Portfolio/ListPortfolio/ListPortfolio";
import Spinner from "../../Components/Spinner/Spinner";

function SearchPage() {
  const[searching, setSearching]= useState<boolean>(false);
  const [search, setSearch] = useState<string>("");
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [serverError, setServerError] = useState<string>("");
  const [portfolioValues, setPortfolioValues] = useState<string[]>([]);
  const getSearchData = (val: string) => {
    setSearch(val);
    // clickMe;
    // console.log(val);
  };
  const onSearchSubmit = async (e: SyntheticEvent) => {
    setSearching(true);
    e.preventDefault();
    const result = await searchCompanies(search);
    if (typeof result === "string") {
      setServerError(result);
    } else if (Array.isArray(result.data)) {
      setSearchResult(result.data);
      setSearching(false);
      setServerError("");
      console.log(searchResult);
    }
  };
  const onPortfolioCreate = (e: any) => {
    e.preventDefault();
    let exists = portfolioValues.find((value) => value === e.target[0].value);
    if (exists) return;
    setPortfolioValues([...portfolioValues, e.target[0].value]);
  };
  const onPortfolioDelete = (e: any) => {
    e.preventDefault();
    setPortfolioValues(
      portfolioValues.filter((value) => value !== e.target[0].value)
    );
  };
  return (
    <>
      <Search
        onSearchSubmit={onSearchSubmit}
        search={search}
        getSearchData={getSearchData}
      />
      <ListPortfolio
        portfolioValues={portfolioValues}
        onPortfolioDelete={onPortfolioDelete}
      />
      {searching ? (
        <Spinner />
      ) : (
        <CardList
          searchResults={searchResult}
          onPortfolioCreate={onPortfolioCreate}
        />
      )}

      {serverError && <h1>{serverError}</h1>}
    </>
  );
}

export default SearchPage;
