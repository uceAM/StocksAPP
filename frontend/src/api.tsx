import axios from "axios"
import { CompanyIncomeStatement, CompanyKeyMetrics, CompanyProfile, CompanySearch } from "./company";


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
        const data = axios.get<CompanyIncomeStatement[]>(`https://financialmodelingprep.com/api/v3/income-statement/${query}?limit=5&apikey=${process.env.REACT_APP_API}`);
        console.log('calling3');
        return data;
    } catch(error:any) {
        console.log('API error'+ error.message);
    }
}