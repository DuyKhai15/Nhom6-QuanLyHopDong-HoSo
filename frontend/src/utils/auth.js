import { jwtDecode } from 'jwt-decode';

export const getToken = () => localStorage.getItem('token');
export const setToken = (token) => localStorage.setItem('token', token);
export const removeToken = () => localStorage.removeItem('token');

export const getUserInfo = () => {
  const token = getToken();
  if (!token) return null;
  if (token === 'demo-token') return { name: 'Admin User', role: 'admin' };
  try {
    return jwtDecode(token);
  } catch (error) {
    return null;
  }
};

export const isAuthenticated = () => {
  const token = getToken();
  if (!token) return false;
  if (token === 'demo-token') return true;
  try {
    const decoded = jwtDecode(token);
    return decoded.exp * 1000 > Date.now();
  } catch (error) {
    return false;
  }
};
