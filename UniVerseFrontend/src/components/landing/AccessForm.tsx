import { useState, useEffect } from 'react'
import { useNavigate, useLocation } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import axios from '../../axios/axios';
import useAuth from '../../hooks/auth/useAuth';
import ErrorFallback from '../fallback/ErrorFallback';
import { z } from 'zod';
import { useSocket } from '../../hooks/useSocket';

const loginSchema = z.object({
  email: z.string().email().trim(),
  password: z.string().min(1, "Password is required")
})
  
type Inputs = z.infer<typeof loginSchema>

const AccessForm = () => {
  const { setAuth } = useAuth();
  const { createHubConnection } = useSocket();

  const {
    register, 
    handleSubmit, 
    formState: {errors}, 
    watch, 
    clearErrors, 
    reset
  } = useForm({
    defaultValues:{
      username: '',
      email: '',
      password: '',
    }
  });

  const [activeButton, setActiveButton] = useState('login');
  const [error, setError] = useState('');
  const [isError, setIsError] = useState(false);

  const navigate = useNavigate();
  const location = useLocation();
  const from = location.state?.from?.pathname || "/home";

  const watchUsername = watch("username");
  const watchEmail = watch("email");
  const watchPassword = watch("password");

  useEffect(() =>{
    setError('');
    setIsError(false);
  },[watchUsername, watchEmail, watchPassword])

  const onSubmit = async (data : Inputs) =>{
    try{
      let url;

      if(activeButton === 'login'){
        url = 'Auth/login';
      }
      else{
        url = 'Auth/register';
      }

      const response = await axios.post(url,
        JSON.stringify(data),
        {
          headers: {'Content-Type': 'application/json'},
          withCredentials: true
        }
      );

      const accessToken = response?.data?.accessToken;
      const user = response?.data?.username;
      const role = response?.data?.role;

      setAuth({username: user, accessToken: accessToken, role: role});
      createHubConnection();
      navigate(from, { replace: true });
    }
    catch(error){
      console.log(error);
      if(!error?.response){
        setError('No response');
        setIsError(true);
      }else if(error.response?.status === 423){
        setError('Your account is not approved');
        setIsError(true);
      }
      else if(error.response?.status === 403){
        setError('Incorrect email or password');
        setIsError(true);
      }else if(error.response?.status === 404){
        setError('Account not found');
        setIsError(true);
      }else if(error.response?.status === 409){
        setError('User already exists');
        setIsError(true);
      }else{
        setError('Error');
        setIsError(true);
      }
    }
  }

  const handleButtonChange = (btn : string) => {
    reset();
    clearErrors();
    setActiveButton(btn);
    setError('');
    setIsError(false);
  }

  return (
    <form onSubmit={handleSubmit(onSubmit)} className='access-form'>
      <div>
        <button 
          type='button'
          onClick={() => handleButtonChange('login')} 
          className={activeButton === 'login' ? 'login-button-active' : 'login-button'}>
            Login
        </button>
        <button 
          type='button'
          onClick={() => handleButtonChange('register')} 
          className={activeButton === 'register' ? 'register-button-active' : 'register-button'}>
            Register
        </button>
      </div>

      <div className='access-form-input-container'>
        {activeButton === 'register' && 
        <>
          <input 
            className={`access-form-input ${errors.username ? 'input-error' : ''}`}
            type="text" 
            placeholder='Username'
            {...register("username", {required: "Username is required"})}
          />
          {errors.username && <span style={{color: "red"}}>{errors.username?.message}</span>}
        </>
        }
        
        <input 
          className={`access-form-input ${errors.email ? 'input-error' : ''}`}
          type="email" 
          placeholder='Email'
          {...register("email", {required: "Email is required"})}
        />
        {errors.email && <span style={{color: "red"}}>{errors.email?.message}</span>}
        <input 
          className={`access-form-input ${errors.password ? 'input-error' : ''}`}
          type="password" 
          placeholder='Password'
          {...register("password", {required: "Password is required"})}
        />
        {errors.password && <span style={{color: "red"}}>{errors.password?.message}</span>}
      </div>

      <button 
        type='submit'
        className='confirm-button'>
        {activeButton === 'login' ? 'Log In' : 'Sign Up'}
      </button>

      {isError && <ErrorFallback error={error}/>}
    </form>
  )
}

export default AccessForm
