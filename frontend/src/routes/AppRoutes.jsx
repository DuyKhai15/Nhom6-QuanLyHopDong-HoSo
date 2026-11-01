import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import PrivateRoute from './PrivateRoute';
import MainLayout from '../components/Layout/MainLayout';
import LoginPage from '../pages/Login/LoginPage';
import AccountPage from '../pages/Accounts/AccountPage';
import ContractPage from '../pages/Contracts/ContractPage';
import DashboardPage from '../pages/Dashboard/DashboardPage';

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route element={<PrivateRoute />}>
        <Route element={<MainLayout />}>
          <Route path="/" element={<Navigate to="/dashboard" replace />} />
          <Route path="/dashboard" element={<DashboardPage />} />
          <Route path="/accounts" element={<AccountPage />} />
          <Route path="/contracts" element={<ContractPage />} />
        </Route>
      </Route>
    </Routes>
  );
};

export default AppRoutes;
