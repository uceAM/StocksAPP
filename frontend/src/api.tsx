import axios from "axios"
import { CompanySearch } from "./company";

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