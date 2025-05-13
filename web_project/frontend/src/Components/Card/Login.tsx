// src/components/Login.tsx
import React, { useState } from 'react';
import api from '../../api'; 
const Login: React.FC = () => {
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = async () => {
    try {
      const response = await api.post('/account/login', {
        userName,
        password,
      });
      localStorage.setItem('token', response.data.token);
      alert('Zalogowano!');
    } catch (error) {
      alert('Błąd logowania');
    }
  };

  return (
    <div>
      <h2>Logowanie</h2>
      <input value={userName} onChange={e => setUserName(e.target.value)} placeholder="Nazwa użytkownika" />
      <input type="password" value={password} onChange={e => setPassword(e.target.value)} placeholder="Hasło" />
      <button onClick={handleLogin}>Zaloguj</button>
    </div>
  );
};

export default Login;
