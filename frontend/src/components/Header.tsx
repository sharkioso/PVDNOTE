import React from 'react';
import { Menu, Search } from 'lucide-react';
import SearchBar from './SearchBar';
import Avatar from './Avatar';

interface HeaderProps {
  onMenuClick: () => void;
}

const Header: React.FC<HeaderProps> = ({ onMenuClick }) => {
  return (
    <header className="sticky top-0 z-10 bg-white border-b border-gray-200">
      <div className="flex items-center justify-between px-4 py-3">
        <button
          className="md:hidden p-2 rounded-md hover:bg-gray-100"
          onClick={onMenuClick}
        >
          <Menu size={20} />
        </button>
        
        <div className="hidden md:block flex-grow max-w-md">
          <SearchBar placeholder="Search documents..." />
        </div>
        
        <div className="flex items-center space-x-4">
          <button className="p-2 rounded-md hover:bg-gray-100 md:hidden">
            <Search size={20} />
          </button>
          <Avatar initials="JD" size="sm" />
        </div>
      </div>
    </header>
  );
};

export default Header;