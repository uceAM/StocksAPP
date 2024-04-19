import axios from "axios"
import { CompanyProfile, CompanySearch } from "./company";

interface searchResponse{
    data:CompanySearch[];
}

export const searchCompanies = async (query:string) => {
    try{
        const data = await axios.get<searchResponse>(
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
    return result;
    } catch (error:any){
        console.log(`api error:${error.message}`);
    }

}