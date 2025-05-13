import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const AddContactPage: React.FC = () => {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [phone, setPhone] = useState('');
  const [birthDate, setBirthDate] = useState('');
  const navigate = useNavigate();

  const handleAdd = async () => {
    try {
      const categoryId = 1; // tymczasowo — wybór kategorii później
      const token = localStorage.getItem('token');
      if (!token) return alert('Zaloguj się');

      await axios.post(
        `http://localhost:5186/api/contact/${categoryId}`,
        {
          firstName,
          lastName,
          email,
          phone,
          birthDate
        },
        {
          headers: {
            Authorization: `Bearer ${token}`
          }
        }
      );

      alert('Dodano kontakt');
      navigate('/');
    } catch (err) {
      console.error(err);
      alert('Błąd dodawania');
    }
  };

  return (
    <div style={{ padding: '2rem' }}>
      <h2>Dodaj kontakt</h2>
      <input placeholder="Imię" value={firstName} onChange={e => setFirstName(e.target.value)} /><br />
      <input placeholder="Nazwisko" value={lastName} onChange={e => setLastName(e.target.value)} /><br />
      <input placeholder="Email" value={email} onChange={e => setEmail(e.target.value)} /><br />
      <input placeholder="Telefon" value={phone} onChange={e => setPhone(e.target.value)} /><br />
      <input type="date" value={birthDate} onChange={e => setBirthDate(e.target.value)} /><br />
      <button onClick={handleAdd}>Dodaj</button>
    </div>
  );
};

export default AddContactPage;
