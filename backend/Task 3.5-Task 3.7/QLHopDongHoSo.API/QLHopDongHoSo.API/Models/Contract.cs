namespace QLHopDongHoSo.API.Models;

public class Contract
{
    public int ContractId { get; set; }

    public string ContractCode { get; set; } = string.Empty;

    public string ContractName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public User? Creator { get; set; }
}