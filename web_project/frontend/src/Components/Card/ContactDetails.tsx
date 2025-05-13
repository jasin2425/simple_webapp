import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';

type Contact = {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  birthDate: string;
  categoryName: string;
  subcategoryName?: string;
};

const ContactDetails: React.FC = () => {
  const { id } = useParams();
  const [contact, setContact] = useState<Contact | null>(null);

  useEffect(() => {
    axios.get(`http://localhost:5186/api/contact/${id}`)
      .then(res => setContact(res.data))
      .catch(err => console.error("Błąd szczegółów:", err));
  }, [id]);

  if (!contact) return <p>Ładowanie szczegółów...</p>;

  return (
    <div style={{ padding: '2rem' }}>
      <h2>Szczegóły kontaktu</h2>
      <p><strong>Imię:</strong> {contact.firstName}</p>
      <p><strong>Nazwisko:</strong> {contact.lastName}</p>
      <p><strong>Email:</strong> {contact.email}</p>
      <p><strong>Telefon:</strong> {contact.phone}</p>
      <p><strong>Data urodzenia:</strong> {contact.birthDate.slice(0,10)}</p>
      <p><strong>Kategoria:</strong> {contact.categoryName}</p>
      {contact.subcategoryName && (
        <p><strong>Podkategoria:</strong> {contact.subcategoryName}</p>
      )}
    </div>
  );
};

export default ContactDetails;
