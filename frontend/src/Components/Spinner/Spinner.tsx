import { ClipLoader } from 'react-spinners'
import "./Spinner.css"
interface Props {
    isLoading?: boolean
    size?:number
    colour?:string
}

const Spinner = ({isLoading = true,size = 35,colour="green"}: Props) => {
  return <>
    <div id = "spinner">
        <ClipLoader 
            color ={colour}
            loading={isLoading}
            size={size}
            aria-label="loading spinner"
            data-testid ="loader"
        />
    </div>
    </>
}

export default Spinner