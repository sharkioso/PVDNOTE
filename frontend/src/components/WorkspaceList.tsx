import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Plus } from 'lucide-react';

interface Workspace {
  id: number;
  name: string;
  accessLevel: string;
}

interface WorkspaceListProps {
  userId: number;
}

export const WorkspaceList: React.FC<WorkspaceListProps> = ({ userId }) => {
  const [workspaces, setWorkspaces] = useState<Workspace[]>([]);
  const [isCreating, setIsCreating] = useState(false);
  const [newWorkspaceName, setNewWorkspaceName] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    loadWorkspaces();
  }, [userId]);

  const loadWorkspaces = async () => {
    try {
      const response = await fetch(`http://localhost:5248/api/WorkSpace/user/${userId}`);
      if (!response.ok) throw new Error('Failed to fetch workspaces');
      const data = await response.json();
      setWorkspaces(data);
    } catch (error) {
      console.error('Error loading workspaces:', error);
    }
  };

  const handleCreateWorkspace = async () => {
    try {
      const response = await fetch('http://localhost:5248/api/WorkSpace/create', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ 
          UserId: userId,
          Name: newWorkspaceName,
        }),
      });
      if (!response.ok) throw new Error('Failed to create workspace');
      
      setNewWorkspaceName('');
      setIsCreating(false);
      await loadWorkspaces();
    } catch (error) {
      console.error('Error creating workspace:', error);
    }
  };

  const handleWorkspaceClick = (workspaceId: number) => {
    navigate(`/workspace/${workspaceId}`); 
  };

  return (
    <div className="space-y-4 p-6">
      <div className="flex items-center justify-between">
        <h2 className="text-xl font-bold">Мои рабочие пространства</h2>
        <button 
          onClick={() => setIsCreating(true)}
          className="flex items-center px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
        >
          <Plus size={16} className="mr-2" />
          Создать
        </button>
      </div>

      {isCreating && (
        <div className="p-4 border rounded-lg bg-gray-50">
          <input
            type="text"
            value={newWorkspaceName}
            onChange={(e) => setNewWorkspaceName(e.target.value)}
            placeholder="Название workspace"
            className="w-full p-2 mb-2 border rounded"
          />
          <div className="flex space-x-2">
            <button 
              onClick={handleCreateWorkspace}
              className="px-4 py-2 bg-green-500 text-white rounded hover:bg-green-600"
            >
              Создать
            </button>
            <button 
              onClick={() => setIsCreating(false)}
              className="px-4 py-2 bg-gray-200 rounded hover:bg-gray-300"
            >
              Отмена
            </button>
          </div>
        </div>
      )}

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
        {workspaces.map((workspace) => (
          <div 
            key={workspace.id} 
            className="p-4 border rounded-lg hover:shadow-md transition-shadow cursor-pointer"
            onClick={() => handleWorkspaceClick(workspace.id)} // Используем новый обработчик
          >
            <h3 className="font-medium">{workspace.name}</h3>
            <p className="text-sm text-gray-500 mt-1">
              Доступ: {workspace.accessLevel}
            </p>
          </div>
        ))}
      </div>
    </div>
  );
};