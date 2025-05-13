import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const AddContactPage: React.FC = () => {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [phone, setPhone] = useState('');
  const [birthDate, setBirthDate] = useState('');
  const [categories, setCategories] = useState<any[]>([]);
  const [subcategories, setSubcategories] = useState<any[]>([]);
  const [categoryId, setCategoryId] = useState<number>(0);
  const [subcategoryId, setSubcategoryId] = useState<number | null>(null);
  const [customSubcategory, setCustomSubcategory] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    axios.get('http://localhost:5186/api/category')
      .then(res => setCategories(res.data))
      .catch(err => console.error('Błąd kategorii:', err));

    axios.get('http://localhost:5186/api/subcategory')
      .then(res => setSubcategories(res.data))
      .catch(err => console.error('Błąd subkategorii:', err));
  }, []);

  const selectedCategoryName = categories.find(c => c.id === categoryId)?.name;

  const handleAdd = async () => {
    try {
      const token = localStorage.getItem('token');
      if (!token) return alert('Zaloguj się');

      const dataToSend: any = {
        firstName,
        lastName,
        email,
        phone,
        birthDate
      };

      if (selectedCategoryName === 'służbowy' && subcategoryId) {
        dataToSend.subcategoryId = subcategoryId;
      }

      if (selectedCategoryName === 'inny' && customSubcategory) {
        dataToSend.customSubcategory = customSubcategory;
      }

      await axios.post(
        `http://localhost:5186/api/contact/${categoryId}`,
        dataToSend,
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

      <h4>Kategoria</h4>
      <select value={categoryId} onChange={e => setCategoryId(Number(e.target.value))}>
        <option value={0}>-- wybierz --</option>
        {categories.map(cat => (
          <option key={cat.id} value={cat.id}>{cat.name}</option>
        ))}
      </select>

      {selectedCategoryName === 'służbowy' && (
        <>
          <h4>Podkategoria</h4>
          <select value={subcategoryId ?? ''} onChange={e => setSubcategoryId(Number(e.target.value))}>
            <option value="">-- wybierz --</option>
            {subcategories.map(sub => (
              <option key={sub.id} value={sub.id}>{sub.name}</option>
            ))}
          </select>
        </>
      )}

      {selectedCategoryName === 'inny' && (
        <>
          <h4>Podkategoria (własna)</h4>
          <input
            type="text"
            value={customSubcategory}
            onChange={e => setCustomSubcategory(e.target.value)}
          />
        </>
      )}

      <br />
      <button onClick={handleAdd}>Dodaj</button>
    </div>
  );
};

export default AddContactPage;
