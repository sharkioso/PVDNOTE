import { useState } from "react";
import { useParams } from "react-router-dom";
import { Download, FileText, File, ChevronDown } from "lucide-react";

export const ExportButton = () => {
  const { id: pageId } = useParams<{ id: string }>();
  const [isOpen, setIsOpen] = useState(false);

  const handleExport = async (format: "pdf" | "docx" | "html") => {
    try {
      const response = await fetch(`http://localhost:5248/api/export/${format}/${pageId}`);
      if (!response.ok) throw new Error("Export failed");

      const blob = await response.blob();
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement("a");
      link.href = url;
      link.download = `document_${pageId}.${format}`;
      link.click();
      window.URL.revokeObjectURL(url);
    } catch (error) {
      console.error("Export error:", error);
      alert("Failed to export document");
    }
  };

  return (
    <div className="relative inline-block">
      <button
        onClick={() => setIsOpen(!isOpen)}
        className="flex items-center gap-2 px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition-colors"
      >
        <Download size={18} />
        <span>Export</span>
        <ChevronDown size={16} className={`transition-transform ${isOpen ? "rotate-180" : ""}`} />
      </button>

      {isOpen && (
        <div className="absolute right-0 mt-2 w-48 bg-white border border-gray-200 rounded-lg shadow-lg z-50">
          <button
            onClick={() => handleExport("pdf")}
            className="flex items-center w-full px-4 py-2 text-gray-800 hover:bg-gray-100"
          >
            <FileText size={16} className="mr-2" />
            PDF
          </button>
          <button
            onClick={() => handleExport("docx")}
            className="flex items-center w-full px-4 py-2 text-gray-800 hover:bg-gray-100"
          >
            <File size={16} className="mr-2" />
            DOCX
          </button>
        </div>
      )}
    </div>
  );
};