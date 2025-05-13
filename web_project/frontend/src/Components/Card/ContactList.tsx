import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useAuth } from '../../Context/useAuth';
import { Link, useNavigate } from 'react-router-dom';

type Contact = {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
};

const ContactList: React.FC = () => {
  const [contacts, setContacts] = useState<Contact[]>([]);
  const { user, isLoggedIn, logout, token } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    axios.get('http://localhost:5186/api/contact')
      .then(res => setContacts(res.data))
      .catch(err => console.error('Błąd pobierania kontaktów:', err));
  }, []);

  const handleDelete = async (id: number) => {
    if (!token) return;

    if (!window.confirm("Na pewno usunąć ten kontakt?")) return;

    try {
      await axios.delete(`http://localhost:5186/api/contact/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      });

      setContacts(prev => prev.filter(c => c.id !== id));
      alert("Kontakt usunięty.");
    } catch (err) {
      console.error("Błąd usuwania:", err);
      alert("Nie udało się usunąć kontaktu.");
    }
  };

  return (
    <div style={{ padding: '2rem' }}>
      <div style={{ marginBottom: '1rem' }}>
        {isLoggedIn() ? (
          <div>
            Witaj, <strong>{user?.userName}</strong> |{' '}
            <button onClick={logout}>Wyloguj</button>
          </div>
        ) : (
          <div>
            <Link to="/login">Zaloguj się</Link> | <Link to="/register">Zarejestruj się</Link>
          </div>
        )}
      </div>

      {isLoggedIn() && (
        <div style={{ marginBottom: '1rem' }}>
          <Link to="/add">+ Dodaj kontakt</Link>
        </div>
      )}

      <h2>Lista kontaktów</h2>
      <ul>
  {contacts.map(contact => (
    <li key={contact.id}>
      <Link to={`/contact/${contact.id}`}>
        {contact.firstName} {contact.lastName}
      </Link>

      {isLoggedIn() && (
        <>
          <button
            onClick={() => handleDelete(contact.id)}
            style={{ marginLeft: '1rem', color: 'red' }}
          >
            Usuń
          </button>

          <Link
            to={`/edit/${contact.id}`}
            style={{ marginLeft: '1rem', color: 'blue' }}
          >
            Edytuj
          </Link>
        </>
      )}
    </li>
  ))}
</ul>

    </div>
  );
};

export default ContactList;
