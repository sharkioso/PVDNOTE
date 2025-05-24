import React, { useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { ArrowLeft, Save } from 'lucide-react';
import { MDXEditor, MDXEditorMethods } from '@mdxeditor/editor';
import Button from '../components/Button';

const Canvas: React.FC = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [content, setContent] = useState('# Начните писать...');

  const handleSave = () => {
    console.log('Сохранение контента:', content);
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
            Вернуться к документам
          </button>
          <Button
            variant="primary"
            size="sm"
            onClick={handleSave}
            className="flex items-center"
          >
            <Save size={16} className="mr-2" />
            Сохранить
          </Button>
        </div>
      </header>

      <main className="max-w-4xl mx-auto px-4 py-8">
        <MDXEditor
          markdown={content}
          onChange={setContent}
          contentEditableClassName="prose max-w-none"
        />
      </main>
    </div>
  );
};

export default Canvas;