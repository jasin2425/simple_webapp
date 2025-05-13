import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useAuth } from '../../Context/useAuth';
import { Link } from 'react-router-dom';
 
type Contact = {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
};

const ContactList: React.FC = () => {
  const [contacts, setContacts] = useState<Contact[]>([]);
  const { user, isLoggedIn, logout } = useAuth();  

  useEffect(() => {
    axios.get('http://localhost:5186/api/contact')
      .then(res => setContacts(res.data))
      .catch(err => console.error('Błąd pobierania kontaktów:', err));
  }, []);


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
          </li>
        ))}
      </ul>
    </div>
    
  );
};

export default ContactList;
