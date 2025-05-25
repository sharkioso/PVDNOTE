import React, { useState, useEffect } from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Auth from './pages/Auth';
import Dashboard from './pages/Dashboard';
import Canvas from './pages/Canvas';

function App() {
  const [userId, setUserId] = useState<number | null>(null);

  useEffect(() => {
    const storedUserId = localStorage.getItem('userId');
    if (storedUserId) {
      setUserId(parseInt(storedUserId));
    }
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
      {userId ? (
        <Routes>
          <Route 
            path="/dashboard" 
            element={<Dashboard userId={userId} onLogout={handleLogout} />} 
          />
          <Route path="/canvas/:id" element={<Canvas />} />
          <Route path="*" element={<Navigate to="/dashboard" replace />} />
        </Routes>
      ) : (
        <Auth onLogin={handleLogin} />
      )}
    </BrowserRouter>
  );
}

export default App;