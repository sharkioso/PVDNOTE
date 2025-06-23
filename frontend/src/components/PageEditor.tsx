import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { ArrowLeft, Plus, Loader2 } from 'lucide-react';
import { motion, AnimatePresence } from 'framer-motion';
import Button from '../components/Button'; // Измененный импорт
import { ExportButton } from '../components/ExportButton';

interface Block {
  id: number;
  content: string;
  type: string;
  order: number;
  pageId: number;
}

interface PageData {
  id: number;
  title: string;
  blocks: Block[];
}

export const PageEditor: React.FC = () => {
  const { id: pageId } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [pageData, setPageData] = useState<PageData | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  const loadPageData = async () => {
    try {
      setIsLoading(true);
      const response = await fetch(`http://localhost:5248/api/page/${pageId}`);
      if (!response.ok) throw new Error('Ошибка загрузки');
      const data: PageData = await response.json();
      setPageData(data);
    } catch (error) {
      console.error('Ошибка:', error);
    } finally {
      setIsLoading(false);
    }
  };

  const handleCreateBlock = async () => {
    if (!pageData) return;
    
    try {
      const response = await fetch('http://localhost:5248/api/block/create', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          content: 'Новый блок',
          type: 'text',
          order: pageData.blocks.length,
          pageId: Number(pageId),
        }),
      });

      if (!response.ok) throw new Error('Ошибка создания блока');
      const newBlock = await response.json();

      setPageData({
        ...pageData,
        blocks: [...pageData.blocks, newBlock]
      });
    } catch (error) {
      console.error('Ошибка:', error);
    }
  };

  const handleUpdateBlock = async (blockId: number, content: string) => {
    if (!pageData) return;

    try {
      const response = await fetch('http://localhost:5248/api/block/update', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ Id: blockId, Content: content }),
      });

      if (!response.ok) throw new Error('Ошибка обновления блока');

      setPageData({
        ...pageData,
        blocks: pageData.blocks.map(b => 
          b.id === blockId ? { ...b, content } : b
        )
      });
    } catch (error) {
      console.error('Ошибка:', error);
    }
  };

  useEffect(() => {
    loadPageData();
  }, [pageId]);

  if (isLoading) {
    return (
      <div className="flex justify-center items-center h-screen">
        <Loader2 className="animate-spin h-12 w-12 text-blue-500" />
      </div>
    );
  }

  if (!pageData) {
    return <div className="p-4">Страница не найдена</div>;
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <header className="bg-white border-b sticky top-0 z-10">
        <div className="container mx-auto px-4 py-3 flex items-center justify-between">
          <button 
            onClick={() => navigate(-1)}
            className="flex items-center text-gray-600 hover:text-gray-900"
          >
            <ArrowLeft size={20} className="mr-2" />
            Назад
          </button>
          
          <div className="flex items-center gap-4">
            <ExportButton />
            <Button
              onClick={handleCreateBlock}
              className="flex items-center gap-2"
            >
              <Plus size={16} className="mr-2" />
              Добавить блок
            </Button>
          </div>
        </div>
      </header>

      <main className="container mx-auto p-4 max-w-3xl">
        <div className="space-y-4">
          <AnimatePresence>
            {pageData.blocks
              .sort((a, b) => a.order - b.order)
              .map(block => (
                <motion.div
                  key={block.id}
                  initial={{ opacity: 0, y: 20 }}
                  animate={{ opacity: 1, y: 0 }}
                  className="bg-white p-4 rounded-lg shadow border"
                >
                  <textarea
                    className="w-full p-3 focus:outline-none resize-none min-h-[100px]"
                    value={block.content}
                    onChange={(e) => handleUpdateBlock(block.id, e.target.value)}
                    placeholder="Начните писать..."
                  />
                </motion.div>
              ))}
          </AnimatePresence>
        </div>
      </main>
    </div>
  );
};