import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

interface Block {
  id: number;
  content: string;
  type: string;
  order: number;
}

export const PageEditor: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [page, setPage] = useState<{
    id: number;
    title: string;
    blocks: Block[];
  } | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    loadPage();
  }, [id]);

  const loadPage = async () => {
    try {
      const response = await fetch(`http://localhost:5248/api/Page/${id}`);
      if (!response.ok) throw new Error('Failed to fetch page');
      const data = await response.json();
      setPage(data);
    } catch (error) {
      console.error('Error loading page:', error);
    } finally {
      setIsLoading(false);
    }
  };

  const handleBlockChange = async (blockId: number, newContent: string) => {
    try {
      await fetch('http://localhost:5248/api/Page/block/update', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          Id: blockId,
          Content: newContent
        }),
      });
    } catch (error) {
      console.error('Error updating block:', error);
    }
  };

  const addNewBlock = async () => {
    try {
      const response = await fetch('http://localhost:5248/api/Page/block/create', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          Content: '',
          Type: 'text',
          Order: page?.blocks.length || 0,
          PageId: id
        }),
      });
      
      if (response.ok) {
        await loadPage();
      }
    } catch (error) {
      console.error('Error adding block:', error);
    }
  };

  if (isLoading) return <div className="p-6">Loading...</div>;
  if (!page) return <div className="p-6">Page not found</div>;

  return (
    <div className="p-6 max-w-4xl mx-auto">
      <h1 className="text-3xl font-bold mb-8">{page.title}</h1>
      
      <div className="space-y-4">
        {page.blocks.map((block) => (
          <div key={block.id} className="group">
            {block.type === 'text' && (
              <textarea
                className="w-full p-4 border rounded-lg min-h-[100px] focus:outline-none focus:ring-2 focus:ring-blue-200 resize-none"
                value={block.content}
                onChange={(e) => {
                  const newContent = e.target.value;
                  setPage(prev => ({
                    ...prev!,
                    blocks: prev!.blocks.map(b => 
                      b.id === block.id ? { ...b, content: newContent } : b
                    )
                  }));
                  handleBlockChange(block.id, newContent);
                }}
                placeholder="Начните писать..."
              />
            )}
          </div>
        ))}
      </div>

      <button
        onClick={addNewBlock}
        className="mt-6 px-4 py-2 bg-gray-100 rounded-lg hover:bg-gray-200"
      >
        + Добавить блок
      </button>
    </div>
  );
};