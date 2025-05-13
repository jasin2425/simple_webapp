import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../Context/useAuth';

const LoginPage: React.FC = () => {
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();
  const { login } = useAuth();

  const handleLogin = async () => {
    try {
      const res = await axios.post('http://localhost:5186/api/account/login', {
        userName,
        password
      });

      const token = res.data.token;
      const userObj = {
        userName: res.data.userName,
        email: res.data.email
      };

      login(userObj, token); // zapis do contextu i localStorage
      navigate('/'); // przekierowanie
    } catch (err) {
      console.error('Błąd logowania:', err);
      alert('Logowanie nieudane.');
    }
  };

  return (
    <div style={{ padding: '2rem' }}>
      <h2>Logowanie</h2>
      <input
        type="text"
        placeholder="Login"
        value={userName}
        onChange={e => setUserName(e.target.value)}
      />
      <br />
      <input
        type="password"
        placeholder="Hasło"
        value={password}
        onChange={e => setPassword(e.target.value)}
      />
      <br />
      <button onClick={handleLogin}>Zaloguj się</button>
    </div>
  );
};

export default LoginPage;
