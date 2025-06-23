using PVDNOTE.Backend.Core.Entities;

public interface IExportService
{
    Task<byte[]> GenerateDocument(int pageId, ExportFormat format);
}