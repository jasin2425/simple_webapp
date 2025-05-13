import React, { useState } from 'react';
import axios from 'axios';

const RegisterPage: React.FC = () => {
  const [email, setEmail] = useState('');
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');

  const handleRegister = async () => {
    try {
      const res = await axios.post('http://localhost:5186/api/account/register', {
        email,
        userName,
        password,
      });

      const token = res.data.token;
      localStorage.setItem('token', token);
      alert('Zarejestrowano! Token zapisany.');
    } catch (err) {
      console.error('Błąd rejestracji:', err);
      alert('Nie udało się zarejestrować.');
    }
  };

  return (
    <div style={{ padding: '2rem' }}>
      <h2>Rejestracja</h2>
      <input type="email" placeholder="Email" value={email} onChange={e => setEmail(e.target.value)} />
      <br />
      <input type="text" placeholder="Login" value={userName} onChange={e => setUserName(e.target.value)} />
      <br />
      <input type="password" placeholder="Hasło" value={password} onChange={e => setPassword(e.target.value)} />
      <br />
      <button onClick={handleRegister}>Zarejestruj się</button>
    </div>
  );
};

export default RegisterPage;
