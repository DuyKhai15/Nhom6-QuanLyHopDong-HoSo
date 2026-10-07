namespace QLHopDongHoSo.API.Models;

public class Document
{
    public int DocumentId { get; set; }

    public int ContractId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public string? FileType { get; set; }

    public long? FileSize { get; set; }

    public int UploadedBy { get; set; }

    public DateTime UploadedAt { get; set; }

    public Contract? Contract { get; set; }

    public User? Uploader { get; set; }
}