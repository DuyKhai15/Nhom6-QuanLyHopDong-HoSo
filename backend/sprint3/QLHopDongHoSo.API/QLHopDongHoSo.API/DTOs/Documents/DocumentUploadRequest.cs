using Microsoft.AspNetCore.Http;

namespace QLHopDongHoSo.API.DTOs.Documents;

public class DocumentUploadRequest
{
    public int ContractId { get; set; }

    public IFormFile? File { get; set; }
}