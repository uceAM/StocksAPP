import { ClipLoader } from 'react-spinners'
import "./Spinner.css"
interface Props {
    isLoading?: boolean
}

const Spinner = ({isLoading = true}: Props) => {
  return <>
    <div id = "spinner">
        <ClipLoader 
            color ="green"
            loading={isLoading}
            size={35}
            aria-label="loading spinner"
            data-testid ="loader"
        />
    </div>
    </>
}

export default Spinner