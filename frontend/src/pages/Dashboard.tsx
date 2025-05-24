import React, { useState } from 'react';
import { Plus } from 'lucide-react';
import { useNavigate } from 'react-router-dom';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import Button from '../components/Button';
import DocumentCard from '../components/DocumentCard';

const Dashboard: React.FC = () => {
  const navigate = useNavigate();
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [documents, setDocuments] = useState([
    { id: '1', title: 'Руководство по началу работы', updatedAt: '2 часа назад' },
    { id: '2', title: 'План проекта', updatedAt: 'Вчера' },
    { id: '3', title: 'Заметки со встречи', updatedAt: '3 дня назад' },
    { id: '4', title: 'Идеи и вдохновение', updatedAt: '1 неделю назад' },
    { id: '5', title: 'Список для чтения', updatedAt: '2 недели назад' },
  ]);

  const handleCreateCanvas = () => {
    const newDoc = {
      id: `${documents.length + 1}`,
      title: `Новый документ`,
      updatedAt: 'Только что',
    };
    setDocuments([newDoc, ...documents]);
    navigate(`/canvas/${newDoc.id}`);
  };

  const handleDocumentClick = (id: string) => {
    navigate(`/canvas/${id}`);
  };

  return (
    <div className="flex h-screen bg-gray-50">
      <Sidebar 
        isOpen={sidebarOpen} 
        onClose={() => setSidebarOpen(false)} 
        onCreateCanvas={handleCreateCanvas}
      />
      
      <div className="flex-1 flex flex-col overflow-hidden">
        <Header onMenuClick={() => setSidebarOpen(true)} />
        
        <main className="flex-1 overflow-y-auto p-4 md:p-6">
          <div className="mb-8">
            <div className="flex items-center justify-between mb-4">
              <h2 className="text-xl font-bold">Недавние документы</h2>
              <Button
                variant="outline"
                size="sm"
                onClick={handleCreateCanvas}
                className="flex items-center"
              >
                <Plus size={16} className="mr-1" />
                Новый документ
              </Button>
            </div>
            
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
              {documents.map((doc) => (
                <DocumentCard
                  key={doc.id}
                  title={doc.title}
                  updatedAt={doc.updatedAt}
                  onClick={() => handleDocumentClick(doc.id)}
                />
              ))}
            </div>
          </div>
          
          <div className="mb-8">
            <h2 className="text-xl font-bold mb-4">Быстрые действия</h2>
            <div className="grid grid-cols-1 sm:grid-cols-3 gap-4">
              <div 
                className="p-4 border border-gray-200 rounded-lg bg-white hover:border-gray-300 hover:shadow-sm transition-all cursor-pointer"
                onClick={handleCreateCanvas}
              >
                <h3 className="font-medium mb-1">Создать документ</h3>
                <p className="text-sm text-gray-500">Начать с чистого листа</p>
              </div>
              <div className="p-4 border border-gray-200 rounded-lg bg-white hover:border-gray-300 hover:shadow-sm transition-all cursor-pointer">
                <h3 className="font-medium mb-1">Импорт документа</h3>
                <p className="text-sm text-gray-500">Загрузить с устройства</p>
              </div>
              <div className="p-4 border border-gray-200 rounded-lg bg-white hover:border-gray-300 hover:shadow-sm transition-all cursor-pointer">
                <h3 className="font-medium mb-1">Шаблоны</h3>
                <p className="text-sm text-gray-500">Начать с шаблона</p>
              </div>
            </div>
          </div>
        </main>
      </div>
    </div>
  );
};

export default Dashboard;