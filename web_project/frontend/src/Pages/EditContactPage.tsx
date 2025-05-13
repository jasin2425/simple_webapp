import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';
import { useAuth } from '../Context/useAuth';

const EditContactPage: React.FC = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { token, isLoggedIn } = useAuth();

  const [form, setForm] = useState({
    firstName: '',
    lastName: '',
    email: '',
    phone: '',
    birthDate: '',
  });

  useEffect(() => {
    axios.get(`http://localhost:5186/api/contact/${id}`)
      .then(res => {
        const { firstName, lastName, email, phone, birthDate } = res.data;
        setForm({
          firstName,
          lastName,
          email,
          phone,
          birthDate: birthDate.slice(0, 10),
        });
      })
      .catch(err => console.error('Błąd pobierania kontaktu:', err));
  }, [id]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm(prev => ({ ...prev, [e.target.name]: e.target.value }));
  };

  const handleUpdate = async () => {
    if (!token) return alert('Zaloguj się');

    try {
      await axios.put(`http://localhost:5186/api/contact/${id}`, form, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      });

      alert('Zapisano zmiany!');
      navigate('/');
    } catch (err) {
      console.error('Błąd zapisu:', err);
      alert('Nie udało się zaktualizować.');
    }
  };

  if (!isLoggedIn()) return <p>Zaloguj się, aby edytować kontakt.</p>;

  return (
    <div style={{ padding: '2rem' }}>
      <h2>Edytuj kontakt</h2>
      <input name="firstName" placeholder="Imię" value={form.firstName} onChange={handleChange} /><br />
      <input name="lastName" placeholder="Nazwisko" value={form.lastName} onChange={handleChange} /><br />
      <input name="email" placeholder="Email" value={form.email} onChange={handleChange} /><br />
      <input name="phone" placeholder="Telefon" value={form.phone} onChange={handleChange} /><br />
      <input name="birthDate" type="date" value={form.birthDate} onChange={handleChange} /><br />

      <button onClick={handleUpdate}>Zapisz</button>
    </div>
  );
};

export default EditContactPage;
