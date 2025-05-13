import { Routes, Route } from 'react-router-dom';
import ContactList from './Components/Card/ContactList';
import ContactDetails from './Components/Card/ContactDetails';
  import RegisterPage from './Pages/RegisterPage';
import LoginPage from './Pages/LoginPage';
import AddContactPage from './Pages/AddContactPage';
import EditContactPage from './Pages/EditContactPage';



function App() {
  return (

<Routes>
  <Route path="/contact/:id" element={<ContactDetails />} />
  <Route path="/register" element={<RegisterPage />} />
  <Route path="/login" element={<LoginPage />} /> {}
  <Route path="/add" element={<AddContactPage />} />
  <Route path="/" element={<ContactList />} />
  <Route path="/edit/:id" element={<EditContactPage />} />


</Routes>

  );
}

export default App;
