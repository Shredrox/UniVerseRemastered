import Auth from "./Auth";

export default interface AuthContextType{
  auth: Auth | null;
  setAuth: React.Dispatch<React.SetStateAction<Auth | null>>;
}
