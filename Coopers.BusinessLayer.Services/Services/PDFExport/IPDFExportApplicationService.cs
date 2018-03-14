using System.IO;

namespace Coopers.BusinessLayer.Services.Services
{
    public interface IPDFExportApplicationService
    {
        byte[] GeneratePDF(string Template);
    }
}
