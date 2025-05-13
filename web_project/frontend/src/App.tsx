import { Routes, Route } from 'react-router-dom';
import ContactList from './Components/Card/ContactList';
import ContactDetails from './Components/Card/ContactDetails';
  import RegisterPage from './Pages/RegisterPage';
import LoginPage from './Pages/LoginPage';

function App() {
  return (

<Routes>
  <Route path="/" element={<ContactList />} />
  <Route path="/contact/:id" element={<ContactDetails />} />
  <Route path="/register" element={<RegisterPage />} />
  <Route path="/login" element={<LoginPage />} /> {}
</Routes>

  );
}

export default App;
