import React, { useState } from 'react';
import { Search } from 'lucide-react';

interface SearchBarProps {
  placeholder?: string;
  onSearch?: (query: string) => void;
  className?: string;
}

const SearchBar: React.FC<SearchBarProps> = ({
  placeholder = 'Поиск...',
  onSearch,
  className = '',
}) => {
  const [query, setQuery] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (onSearch) onSearch(query);
  };

  return (
    <form
      className={`relative ${className}`}
      onSubmit={handleSubmit}
    >
      <div className="relative">
        <Search 
          className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 h-4 w-4" 
        />
        <input
          type="text"
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          placeholder={placeholder}
          className="w-full py-2 pl-10 pr-4 bg-gray-100 border border-transparent rounded-md focus:outline-none focus:ring-2 focus:ring-black focus:border-transparent focus:bg-white transition-colors"
        />
      </div>
    </form>
  );
};

export default SearchBar;