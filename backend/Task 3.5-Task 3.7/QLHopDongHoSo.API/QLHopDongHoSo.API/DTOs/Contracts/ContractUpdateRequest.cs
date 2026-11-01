namespace QLHopDongHoSo.API.DTOs.Contracts;

public class ContractUpdateRequest
{
    public string ContractCode { get; set; } = string.Empty;

    public string ContractName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Status { get; set; } = string.Empty;
}