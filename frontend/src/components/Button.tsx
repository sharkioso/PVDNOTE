import React from 'react';
import { LucideIcon } from 'lucide-react';

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  children: React.ReactNode;
  icon?: LucideIcon;
}

const Button: React.FC<ButtonProps> = ({ 
  children, 
  className = '',
  icon: Icon,
  ...props 
}) => {
  return (
    <button
      className={`flex items-center justify-center px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition-colors ${className}`}
      {...props}
    >
      {Icon && <Icon className="mr-2" size={16} />}
      {children}
    </button>
  );
};

export default Button;