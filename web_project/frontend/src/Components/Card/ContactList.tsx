import React, { useEffect, useState } from 'react';

type Contact = {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  birthDate: string;
};

const ContactList: React.FC = () => {
  const [contacts, setContacts] = useState<Contact[]>([]);

  useEffect(() => {
    fetch('http://localhost:5186/api/contact')
      .then((res) => res.json())
      .then((data) => {
        console.log("Dane z backendu:", data);
        setContacts(data);
      })
      .catch((err) => console.error('Błąd:', err));
  }, []);

  return (
    <div>
      <ul>
        {contacts.map((contact) => (
          <li key={contact.id}>
            <strong>{contact.firstName} {contact.lastName}</strong><br />
            Email: {contact.email}<br />
            Telefon: {contact.phone}<br />
            Data urodzenia: {new Date(contact.birthDate).toLocaleDateString()}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ContactList;
