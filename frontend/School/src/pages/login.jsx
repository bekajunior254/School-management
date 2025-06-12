import { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

export default function Login() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [errorMsg, setErrorMsg] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (username.trim() === '' || password.trim() === '') {
      setErrorMsg('Username and password are required.');
      return;
    }

    try {
      const response = await axios.post('http://localhost:5000/api/Auth/login', {
        username,
        password,
      });

      //Store token in localStorage
      localStorage.setItem('token', response.data.token);

      //  Redirect to dashboard
      navigate('/dashboard');
    } catch (error) {
      console.error('Login error:', error);
      setErrorMsg('Invalid credentials or server error.');
    }
  };

  return (
    <div className="max-w-md mx-auto p-6 bg-white rounded shadow mt-20">
      <h2 className="text-4xl font-bold mb-6 text-center">Login</h2>

      <form onSubmit={handleSubmit} className="space-y-6">
        {errorMsg && (
          <div className="bg-red-100 text-red-700 p-2 rounded">{errorMsg}</div>
        )}

        <div>
          <label htmlFor="username" className="block font-semibold mb-2">
            Username
          </label>
          <input
            id="username"
            type="text"
            className="w-full border rounded px-3 py-2"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            autoFocus
          />
        </div>

        <div>
          <label htmlFor="password" className="block font-semibold mb-2">
            Password
          </label>
          <input
            id="password"
            type="password"
            className="w-full border rounded px-3 py-2"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>

        <button
          type="submit"
          className="w-full bg-blue-600 text-white py-3 rounded hover:bg-blue-700 transition"
        >
          Login
        </button>
      </form>
    </div>
  );
}
