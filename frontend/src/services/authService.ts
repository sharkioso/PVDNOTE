export const authService = {
  async register(login: string, password: string): Promise<number> {
    const response = await fetch('http://localhost:5248/api/auth/register', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ login, password }),
    });

    if (!response.ok) {
      const error = await response.json();
      throw new Error(error.message || 'Ошибка регистрации');
    }

    const data = await response.json();
    return data.id;
  },

  async login(login: string, password: string): Promise<number> {
    const response = await fetch('http://localhost:5248/api/auth/login', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ login, password }),
    });

    if (!response.ok) {
      const error = await response.json();
      throw new Error(error.message || 'Ошибка входа');
    }

    const data = await response.json();
    return data.id;
  },

  async logout(): Promise<void> {
    // В реальном проекте здесь был бы запрос на бекенд
    return Promise.resolve();
  }
};