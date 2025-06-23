using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Infrastructure.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class PdfExportService : IExportService
{
    private readonly DBContext context;
    public PdfExportService(DBContext context)
    {
        this.context = context;
        QuestPDF.Settings.License = LicenseType.Community;
    }


    public Task<byte[]> GenerateDocumentAsync(string title, Block[] blocks, ExportFormat format)
    {
        var document = QuestPDF.Fluent.Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.Content()
                    .Column(column =>
                    {
                        column.Item().Text(title).FontSize(20).Bold();

                        foreach (var block in blocks)
                        {
                            column.Item().PaddingTop(10).Text(block.Content);
                        }
                    });
            });
        });

        return Task.FromResult(document.GeneratePdf());
    }

    public async Task<byte[]> GenerateDocument(int padeId, ExportFormat foramt)
    {
        var pageg = await context.Pages
            .Include(p => p.Blocks)
            .FirstOrDefaultAsync(p => p.Id == padeId);

        var document = QuestPDF.Fluent.Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.Content()
                    .Column(column =>
                    {
                        column.Item().Text(pageg.Title).FontSize(20).Bold();
                        foreach (var block in pageg.Blocks)
                            column.Item().PaddingTop(10).Text(block.Content);
                    });
            });
        });

        return document.GeneratePdf();
    }
}
