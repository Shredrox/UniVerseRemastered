import { MdErrorOutline } from "react-icons/md";

interface ErrorFallbackProps{
  error: string;
}

const ErrorFallback = ({error} : ErrorFallbackProps) => {
  return (
    <div className="error-fallback-container">
      <div className='error-fallback'>
        <span><MdErrorOutline className="error-icon"/>{error}</span>
      </div>
    </div>
  )
}

export default ErrorFallback
