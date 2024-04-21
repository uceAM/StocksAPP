import axios from "axios"
import { CompanyBalanceSheet, CompanyCashFlow, CompanyIncomeStatement, CompanyKeyMetrics, CompanyProfile, CompanySearch, CompanySecFiling } from "./company";


export const searchCompanies = async (query:string) => {
    try{
        const data = await axios.get<CompanySearch[]>(
            `https://financialmodelingprep.com/api/v3/search?query=${query}&limit=10&exchange=NASDAQ&apikey=${process.env.REACT_APP_API}`
        );
        return data;
    } catch(error){
        if(axios.isAxiosError(error)){
            console.log("Error:",error.message);
            return error.message;
        } else{
            console.log("unexpected error", error);
            return ("an unexpected API error occured")
        }
    }
}

export const getCompanyDetails = async (query:string)=>{
    try{
    const result = await axios.get<CompanyProfile[]>(`https://financialmodelingprep.com/api/v3/profile/${query}?apikey=${process.env.REACT_APP_API}`);
    console.log('calling1');
    return result;
    } catch (error:any){
        console.log(`api error:${error.message}`);
    }

}

export const getKeyMetrics = async (query:string)=>{
    try{
        const data= await axios.get<CompanyKeyMetrics[]>(`https://financialmodelingprep.com/api/v3/key-metrics-ttm/${query}?apikey=${process.env.REACT_APP_API}`);
        console.log('calling2');
        return data;
    } catch(error:any) {
        console.log('unknown API Error' + error.message);
    }
}
export const getIncomeStatement = async (query:string) =>{
    try{
        const data = axios.get<CompanyIncomeStatement[]>(`https://financialmodelingprep.com/api/v3/income-statement/${query}?limit=5?period=annual&apikey=${process.env.REACT_APP_API}`);
        console.log('calling3');
        return data;
    } catch(error:any) {
        console.log('API error'+ error.message);
    }
}

export const getBalanceSheet = async(query:string)=> {
    try {
        const data = axios.get<CompanyBalanceSheet[]>(`https://financialmodelingprep.com/api/v3/balance-sheet-statement/${query}?limit=5?&apikey=${process.env.REACT_APP_API}`);
        console.log('calling4');
        return data;
    } catch (error:any) {
        console.log('API ERROR'+ error.message);
    }
}

export const getCashFlow = async (query:string) => {
    try {
        const data = axios.get<CompanyCashFlow[]>(`https://financialmodelingprep.com/api/v3/cash-flow-statement/${query}?limit=5&apikey=${process.env.REACT_APP_API}`);
        console.log('calling4');
        return data;
    } catch (error:any) {
        console.log('API ERROR'+ error.message);
    }
}

export const getSecFiling = async(query:string) =>{
    try {
        const data = axios.get<CompanySecFiling[]>(`https://financialmodelingprep.com/api/v3/sec_filings/${query}?type=10-k&page=0&apikey=${process.env.REACT_APP_API}`);
        console.log('calling5')
        return data;
   } catch (error:any) {
        console.log('API ERROR'+ error.message);
    }
}