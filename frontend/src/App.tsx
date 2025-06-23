// src/App.tsx
import React, { useState, useEffect } from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Auth from './pages/Auth';
import Dashboard from './pages/Dashboard';
import { PageEditor } from './components/PageEditor';
import WorkspacePagesView from './components/WorkspacePagesView';
import { Toaster } from 'react-hot-toast';

function App() {
  const [userId, setUserId] = useState<number | null>(null);

  useEffect(() => {
    const storedUserId = localStorage.getItem('userId');
    if (storedUserId) setUserId(parseInt(storedUserId));
  }, []);

  const handleLogin = (id: number) => {
    setUserId(id);
    localStorage.setItem('userId', id.toString());
  };

  const handleLogout = () => {
    setUserId(null);
    localStorage.removeItem('userId');
  };

  return (
    <BrowserRouter>
      <Toaster position="bottom-right" />
      <Routes>
        <Route path="/" element={<Navigate to={userId ? "/dashboard" : "/auth"} replace />} />
        <Route path="/auth" element={<Auth onLogin={handleLogin} />} />
        <Route path="/dashboard" element={<Dashboard userId={userId!} onLogout={handleLogout} />} />
        <Route path="/canvas/:id" element={<PageEditor />} />
        <Route path="/workspace/:id" element={<WorkspacePagesView />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;