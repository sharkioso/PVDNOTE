import React from 'react';
import { File, MoreHorizontal } from 'lucide-react';

interface DocumentCardProps {
  title: string;
  updatedAt: string;
  onClick?: () => void;
}

const DocumentCard: React.FC<DocumentCardProps> = ({
  title,
  updatedAt,
  onClick,
}) => {
  return (
    <div
      className="group p-4 border border-gray-200 rounded-lg hover:border-gray-300 hover:shadow-sm transition-all cursor-pointer bg-white"
      onClick={onClick}
    >
      <div className="flex items-start justify-between">
        <div className="flex items-center">
          <File size={18} className="text-gray-400 mr-2" />
          <h3 className="font-medium text-gray-800 line-clamp-1">{title}</h3>
        </div>
        <button className="opacity-0 group-hover:opacity-100 transition-opacity p-1 rounded-md hover:bg-gray-100">
          <MoreHorizontal size={16} />
        </button>
      </div>
      <p className="mt-2 text-xs text-gray-500">Updated {updatedAt}</p>
    </div>
  );
};

export default DocumentCard;