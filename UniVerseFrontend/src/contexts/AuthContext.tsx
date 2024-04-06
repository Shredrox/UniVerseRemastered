import { ReactNode, createContext, useState } from "react"
import AuthContextType from "../interfaces/auth/AuthContextType";
import Auth from "../interfaces/auth/Auth";

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider = ({children} : AuthProviderProps) => {
  const [auth, setAuth] = useState<Auth | null>(null);

  const contextValue : AuthContextType = {
    auth,
    setAuth
  }

  return (
    <AuthContext.Provider value={contextValue}>
      {children}
    </AuthContext.Provider>
  )
}

export default AuthContext
