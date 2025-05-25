import React, { useState } from 'react';
import { Plus } from 'lucide-react';
import { useNavigate } from 'react-router-dom';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import Button from '../components/Button';
import DocumentCard from '../components/DocumentCard';

interface DashboardProps {
  userId: number;
  onLogout: () => void;
}

interface Document {
  id: string;
  title: string;
  updatedAt: string;
}

const Dashboard: React.FC<DashboardProps> = ({ userId, onLogout }) => {
  const navigate = useNavigate();
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [documents, setDocuments] = useState<Document[]>([
    { id: '1', title: 'Руководство по началу работы', updatedAt: '2 часа назад' },
    { id: '2', title: 'План проекта', updatedAt: 'Вчера' },
    { id: '3', title: 'Заметки со встречи', updatedAt: '3 дня назад' },
    { id: '4', title: 'Идеи и вдохновение', updatedAt: '1 неделю назад' },
    { id: '5', title: 'Список для чтения', updatedAt: '2 недели назад' },
  ]);

  const handleCreateCanvas = () => {
    const newDoc = {
      id: `${documents.length + 1}`,
      title: `Новый документ ${documents.length + 1}`,
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
        onLogout={onLogout}
        userId={userId}
      />
      
      <div className="flex-1 flex flex-col overflow-hidden">
        <Header 
          onMenuClick={() => setSidebarOpen(true)} 
          userId={userId}
        />
        
        <main className="flex-1 overflow-y-auto p-4 md:p-6">
          {/* ... (остальной код без изменений) ... */}
        </main>
      </div>
    </div>
  );
};

export default Dashboard;