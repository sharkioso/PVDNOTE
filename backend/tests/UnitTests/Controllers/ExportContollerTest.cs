using Microsoft.AspNetCore.Mvc;
using Moq;
using PVDNOTE.Backend.Infrastructure.Data;
using Xunit;

namespace Backend.Tests.UnitTests.Controllers
{
    public class ExportControllerTests
    {
        private readonly Mock<IExportService> _mockExportService;
        private readonly ExportController _controller;

        public ExportControllerTests()
        {
            _mockExportService = new Mock<IExportService>();
            _controller = new ExportController(null, _mockExportService.Object);
        }

        [Fact]
        public async Task ExportToPdf_ReturnsFileResult_WhenSuccessful()
        {

            var testPageId = 1;
            var testPdfBytes = new byte[] { 0x25, 0x50, 0x44, 0x46 }; // PDF magic number
            _mockExportService.Setup(x => x.GenerateDocument(testPageId, ExportFormat.Pdf))
                .ReturnsAsync(testPdfBytes);


            var result = await _controller.ExportToPdf(testPageId);


            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("application/pdf", fileResult.ContentType);
            Assert.Equal($"page_{testPageId}.pdf", fileResult.FileDownloadName);
            Assert.Equal(testPdfBytes, fileResult.FileContents);
        }

        [Fact]
        public async Task ExportToPdf_ReturnsBadRequest_WhenServiceThrows()
        {

            var testPageId = 1;
            _mockExportService.Setup(x => x.GenerateDocument(testPageId, ExportFormat.Pdf))
                .ThrowsAsync(new Exception("Generation failed"));


            var result = await _controller.ExportToPdf(testPageId);


            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Generation failed", badRequestResult.Value);
        }

        [Fact]
        public async Task ExportToPdf_CallsServiceWithCorrectParameters()
        {

            var testPageId = 1;
            var testPdfBytes = new byte[] { 0x25, 0x50, 0x44, 0x46 };
            _mockExportService.Setup(x => x.GenerateDocument(testPageId, ExportFormat.Pdf))
                .ReturnsAsync(testPdfBytes);
   
            await _controller.ExportToPdf(testPageId);

            _mockExportService.Verify(
                x => x.GenerateDocument(testPageId, ExportFormat.Pdf),
                Times.Once);
        }
    }
}