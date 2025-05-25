import React from 'react';
import { Home, Plus, Settings, BookOpen, Bookmark, X } from 'lucide-react';
import Button from './Button';
import Avatar from './Avatar';

interface SidebarProps {
  isOpen: boolean;
  onClose: () => void;
  onCreateCanvas: () => void;
  onLogout: () => void;
  userId?: number;
}

const Sidebar: React.FC<SidebarProps> = ({ 
  isOpen, 
  onClose, 
  onCreateCanvas,
  onLogout,
  userId 
}) => {
  const navItems = [
    { icon: <Home size={18} />, label: 'Главная', href: '#' },
    { icon: <BookOpen size={18} />, label: 'Документы', href: '#' },
    { icon: <Bookmark size={18} />, label: 'Сохраненные', href: '#' },
    { icon: <Settings size={18} />, label: 'Настройки', href: '#' },
  ];

  return (
    <>
      <div
        className={`fixed inset-0 bg-black bg-opacity-50 z-20 transition-opacity duration-300 ${
          isOpen ? 'opacity-100' : 'opacity-0 pointer-events-none'
        }`}
        onClick={onClose}
      />
      <aside
        className={`fixed top-0 left-0 bottom-0 w-64 bg-white z-30 transform transition-transform duration-300 ease-in-out ${
          isOpen ? 'translate-x-0' : '-translate-x-full'
        } md:translate-x-0 md:static md:z-0`}
      >
        <div className="flex flex-col h-full p-4">
          <div className="flex items-center justify-between mb-6 p-2">
            <div className="flex items-center">
              <div className="w-8 h-8 bg-black rounded-md flex items-center justify-center mr-2">
                <BookOpen size={18} className="text-white" />
              </div>
              <h1 className="text-xl font-bold">PVD Note</h1>
            </div>
            <button className="md:hidden" onClick={onClose}>
              <X size={18} />
            </button>
          </div>

          <Button
            variant="primary"
            className="mb-6 flex items-center"
            onClick={onCreateCanvas}
            fullWidth
          >
            <Plus size={16} className="mr-2" />
            Новый документ
          </Button>

          <nav className="flex-grow">
            <ul className="space-y-1">
              {navItems.map((item, index) => (
                <li key={index}>
                  <a
                    href={item.href}
                    className="flex items-center px-3 py-2 text-gray-700 rounded-md hover:bg-gray-100 transition-colors"
                  >
                    <span className="mr-3">{item.icon}</span>
                    {item.label}
                  </a>
                </li>
              ))}
            </ul>
          </nav>

          <div className="mt-auto pt-4 border-t border-gray-200">
            <div className="flex items-center p-2">
              <Avatar size="sm" initials={userId ? `U${userId}` : "AN"} />
              <div className="ml-2">
                <p className="text-sm font-medium">
                  {userId ? `User #${userId}` : "Гость"}
                </p>
                {userId && (
                  <button 
                    onClick={onLogout}
                    className="text-xs text-red-500 hover:text-red-700"
                  >
                    Выйти
                  </button>
                )}
              </div>
            </div>
          </div>
        </div>
      </aside>
    </>
  );
};

export default Sidebar;