import React, { useState } from 'react';
import { authService } from '../services/authService';
import Input from '../components/Input';
import Button from '../components/Button';

interface AuthProps {
  onLogin: (userId: number) => void; // Передаём ID пользователя
}

const Auth: React.FC<AuthProps> = ({ onLogin }) => {
  const [isLogin, setIsLogin] = useState(true);
  const [login, setLogin] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    try {
      const userId = isLogin
        ? await authService.login(login, password)
        : await authService.register(login, password);

      onLogin(userId); // Передаём ID в родительский компонент
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Ошибка авторизации');
    }
  };

  return (
    <div className="auth-container">
      <form onSubmit={handleSubmit}>
        <h2>{isLogin ? 'Вход' : 'Регистрация'}</h2>
        
        {error && <div className="error-message">{error}</div>}

        <Input
          label="Логин"
          value={login}
          onChange={(e) => setLogin(e.target.value)}
          required
        />

        <Input
          label="Пароль"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />

        <Button type="submit">
          {isLogin ? 'Войти' : 'Зарегистрироваться'}
        </Button>

        <button 
          type="button" 
          onClick={() => setIsLogin(!isLogin)}
          className="toggle-mode"
        >
          {isLogin ? 'Нет аккаунта? Зарегистрироваться' : 'Уже есть аккаунт? Войти'}
        </button>
      </form>
    </div>
  );
};

export default Auth;