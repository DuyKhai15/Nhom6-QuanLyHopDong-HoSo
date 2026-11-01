using Microsoft.EntityFrameworkCore;
using QLHopDongHoSo.API.Data;
using QLHopDongHoSo.API.DTOs.Contracts;
using QLHopDongHoSo.API.Models;

namespace QLHopDongHoSo.API.Services;

public class ContractService : IContractService
{
    private readonly ApplicationDbContext _context;

    public ContractService(ApplicationDbContext context)
    {
        _context = context;
    }

    // =========================
    // GET ALL
    // =========================
    public async Task<List<ContractResponse>> GetAllAsync()
    {
        return await _context.Contracts
            .Include(c => c.Creator)
            .Select(c => new ContractResponse
            {
                ContractId = c.ContractId,
                ContractCode = c.ContractCode,
                ContractName = c.ContractName,
                Description = c.Description,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                Status = c.Status,
                CreatedBy = c.CreatedBy,
                CreatedByUsername = c.Creator != null
                    ? c.Creator.Username
                    : null,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            })
            .ToListAsync();
    }

    // =========================
    // GET BY ID
    // =========================
    public async Task<ContractResponse?> GetByIdAsync(int id)
    {
        return await _context.Contracts
            .Include(c => c.Creator)
            .Where(c => c.ContractId == id)
            .Select(c => new ContractResponse
            {
                ContractId = c.ContractId,
                ContractCode = c.ContractCode,
                ContractName = c.ContractName,
                Description = c.Description,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                Status = c.Status,
                CreatedBy = c.CreatedBy,
                CreatedByUsername = c.Creator != null
                    ? c.Creator.Username
                    : null,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            })
            .FirstOrDefaultAsync();
    }

    // =========================
    // CREATE
    // =========================
    public async Task<ContractResponse> CreateAsync(
        ContractCreateRequest request,
        int userId)
    {
        // Kiểm tra mã hợp đồng đã tồn tại
        var existingContract = await _context.Contracts
            .AnyAsync(c => c.ContractCode == request.ContractCode);

        if (existingContract)
        {
            throw new InvalidOperationException(
                "Mã hợp đồng đã tồn tại.");
        }

        var contract = new Contract
        {
            ContractCode = request.ContractCode,
            ContractName = request.ContractName,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status,
            CreatedBy = userId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Contracts.Add(contract);

        await _context.SaveChangesAsync();

        return await GetByIdAsync(contract.ContractId)
            ?? throw new Exception(
                "Không thể lấy hợp đồng vừa tạo.");
    }

    // =========================
    // UPDATE
    // =========================
    public async Task<ContractResponse?> UpdateAsync(
        int id,
        ContractUpdateRequest request)
    {
        var contract = await _context.Contracts
            .FirstOrDefaultAsync(c => c.ContractId == id);

        if (contract == null)
        {
            return null;
        }

        // Kiểm tra mã hợp đồng bị trùng
        // nhưng loại trừ chính hợp đồng đang được sửa
        var existingContract = await _context.Contracts
            .AnyAsync(c =>
                c.ContractCode == request.ContractCode &&
                c.ContractId != id);

        if (existingContract)
        {
            throw new InvalidOperationException(
                "Mã hợp đồng đã tồn tại.");
        }

        contract.ContractCode = request.ContractCode;
        contract.ContractName = request.ContractName;
        contract.Description = request.Description;
        contract.StartDate = request.StartDate;
        contract.EndDate = request.EndDate;
        contract.Status = request.Status;
        contract.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    // =========================
    // DELETE
    // =========================
    public async Task<bool> DeleteAsync(int id)
    {
        var contract = await _context.Contracts
            .FirstOrDefaultAsync(c => c.ContractId == id);

        if (contract == null)
        {
            return false;
        }

        _context.Contracts.Remove(contract);

        await _context.SaveChangesAsync();

        return true;
    }
}