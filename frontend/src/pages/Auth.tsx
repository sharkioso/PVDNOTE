import React, { useState } from 'react';
import { BookOpen } from 'lucide-react';
import Input from '../components/Input';
import Button from '../components/Button';

interface AuthProps {
  onLogin: () => void;
}

const Auth: React.FC<AuthProps> = ({ onLogin }) => {
  const [isLogin, setIsLogin] = useState(true);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [name, setName] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onLogin();
  };

  return (
    <div className="min-h-screen flex flex-col justify-center items-center bg-gray-50 p-4">
      <div className="w-full max-w-md">
        <div className="mb-8 text-center">
          <div className="w-12 h-12 bg-black rounded-lg flex items-center justify-center mx-auto mb-4">
            <BookOpen size={24} className="text-white" />
          </div>
          <h1 className="text-3xl font-bold">PVD Note</h1>
          <p className="text-gray-600 mt-2">Рабочее пространство для ваших заметок и задач</p>
        </div>

        <div className="bg-white p-8 shadow-sm rounded-lg">
          <div className="mb-6">
            <h2 className="text-2xl font-bold text-center">
              {isLogin ? 'Вход в аккаунт' : 'Создание аккаунта'}
            </h2>
          </div>

          <form onSubmit={handleSubmit}>
            {!isLogin && (
              <Input
                label="Полное имя"
                type="text"
                value={name}
                onChange={(e) => setName(e.target.value)}
                placeholder="Иван Петров"
                required
                fullWidth
              />
            )}
            
            <Input
              label="Email"
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="you@example.com"
              required
              fullWidth
            />
            
            <Input
              label="Пароль"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="••••••••"
              required
              fullWidth
            />

            <Button
              type="submit"
              variant="primary"
              fullWidth
              className="mt-6"
            >
              {isLogin ? 'Войти' : 'Создать аккаунт'}
            </Button>
          </form>

          <div className="mt-6 text-center">
            <button
              className="text-sm text-gray-600 hover:text-black"
              onClick={() => setIsLogin(!isLogin)}
            >
              {isLogin
                ? "Нет аккаунта? Зарегистрируйтесь"
                : 'Уже есть аккаунт? Войти'}
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Auth;