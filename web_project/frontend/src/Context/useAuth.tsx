import React, { createContext, useContext, useEffect, useState } from 'react';
import axios from 'axios';

type UserProfile = {
  userName: string;
  email: string;
};

type UserContextType = {
  user: UserProfile | null;
  token: string | null;
  isLoggedIn: () => boolean;
  login: (user: UserProfile, token: string) => void;
  logout: () => void;
};

const UserContext = createContext<UserContextType>({} as UserContextType);

export const UserProvider = ({ children }: { children: React.ReactNode }) => {
  const [user, setUser] = useState<UserProfile | null>(null);
  const [token, setToken] = useState<string | null>(null);

  useEffect(() => {
    const savedToken = localStorage.getItem('token');
    const savedUser = localStorage.getItem('user');

    if (savedToken && savedUser) {
      setToken(savedToken);
      setUser(JSON.parse(savedUser));
      axios.defaults.headers.common['Authorization'] = `Bearer ${savedToken}`;
    }
  }, []);

  const login = (user: UserProfile, token: string) => {
    setUser(user);
    setToken(token);
    localStorage.setItem('token', token);
    localStorage.setItem('user', JSON.stringify(user));
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  };

  const logout = () => {
    setUser(null);
    setToken(null);
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    delete axios.defaults.headers.common['Authorization'];
  };

  const isLoggedIn = () => !!token;

  return (
    <UserContext.Provider value={{ user, token, isLoggedIn, login, logout }}>
      {children}
    </UserContext.Provider>
  );
};

export const useAuth = () => useContext(UserContext);
