
using Microsoft.AspNetCore.Mvc;
using PVDNOTE.Backend.Infrastructure.Data;

[ApiController]
[Route("api/[controller]")]
public class ExportController : ControllerBase
{
    private readonly DBContext context;
    private readonly IExportService exportService;

    public ExportController(DBContext context, IExportService exportService)
    {
        this.context = context;
        this.exportService = exportService;
    }

    [HttpGet("pdf/{pageId}")]
    public async Task<IActionResult> ExportToPdf(int pageId)
    {
        try
        {
            var pdfBytes = await exportService.GenerateDocument(pageId, ExportFormat.Pdf);
            return File(pdfBytes, "application/pdf", $"page_{pageId}.pdf");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}