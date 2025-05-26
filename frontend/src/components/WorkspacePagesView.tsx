// src/components/WorkspacePagesView.tsx
import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { ArrowLeft, Plus } from 'lucide-react';

const WorkspacePagesView: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [pages, setPages] = useState<any[]>([]);
  const [newPageTitle, setNewPageTitle] = useState('');

  useEffect(() => {
    const loadPages = async () => {
      try {
        const response = await fetch(`http://localhost:5248/api/WorkSpace/${id}`);
        if (!response.ok) throw new Error('Ошибка загрузки');
        const data = await response.json();
        setPages(data.pages || []);
      } catch (error) {
        console.error('Ошибка:', error);
      }
    };
    loadPages();
  }, [id]);

  const createPage = async () => {
    try {
      const response = await fetch('http://localhost:5248/api/Page/create', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          Title: newPageTitle,
          WorkSpaceId: id
        }),
      });
      if (!response.ok) throw new Error('Ошибка создания');
      const newPage = await response.json();
      setPages([...pages, newPage]);
      setNewPageTitle('');
    } catch (error) {
      console.error('Ошибка:', error);
    }
  };

  return (
    <div className="min-h-screen bg-white">
      <header className="border-b border-gray-200 bg-white sticky top-0 z-10">
        <div className="flex items-center justify-between px-4 py-2">
          <button
            onClick={() => navigate('/dashboard')}
            className="flex items-center text-gray-600 hover:text-gray-900"
          >
            <ArrowLeft size={20} className="mr-2" />
            Назад
          </button>
        </div>
      </header>

      <main className="max-w-4xl mx-auto p-6">
        <div className="flex items-center gap-2 mb-6">
          <input
            type="text"
            value={newPageTitle}
            onChange={(e) => setNewPageTitle(e.target.value)}
            placeholder="Название страницы"
            className="flex-1 p-2 border rounded"
          />
          <button
            onClick={createPage}
            className="flex items-center px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
          >
            <Plus size={16} className="mr-2" />
            Создать страницу
          </button>
        </div>

        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
          {pages.map(page => (
            <div
              key={page.id}
              className="p-4 border rounded-lg hover:shadow-md cursor-pointer"
              onClick={() => navigate(`/canvas/${page.id}`)}
            >
              <h3 className="font-medium">{page.title}</h3>
            </div>
          ))}
        </div>
      </main>
    </div>
  );
};

export default WorkspacePagesView;