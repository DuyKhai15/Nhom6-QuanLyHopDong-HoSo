using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLHopDongHoSo.API.DTOs.Contracts;
using QLHopDongHoSo.API.Services;
using System.Security.Claims;

namespace QLHopDongHoSo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContractController : ControllerBase
{
    private readonly IContractService _contractService;

    public ContractController(IContractService contractService)
    {
        _contractService = contractService;
    }

    // GET: api/Contract
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contracts = await _contractService.GetAllAsync();

        return Ok(contracts);
    }

    // GET: api/Contract/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var contract = await _contractService.GetByIdAsync(id);

        if (contract == null)
        {
            return NotFound(new
            {
                message = "Không tìm thấy hợp đồng."
            });
        }

        return Ok(contract);
    }

    // POST: api/Contract
    [HttpPost]
    [Authorize(Roles = "Admin,Quản lý")]
    public async Task<IActionResult> Create(
        [FromBody] ContractCreateRequest request)
    {
        // Kiểm tra mã hợp đồng
        if (string.IsNullOrWhiteSpace(request.ContractCode))
        {
            return BadRequest(new
            {
                message = "Mã hợp đồng không được để trống."
            });
        }

        // Kiểm tra tên hợp đồng
        if (string.IsNullOrWhiteSpace(request.ContractName))
        {
            return BadRequest(new
            {
                message = "Tên hợp đồng không được để trống."
            });
        }

        // Kiểm tra trạng thái
        if (string.IsNullOrWhiteSpace(request.Status))
        {
            return BadRequest(new
            {
                message = "Trạng thái không được để trống."
            });
        }
        if (request.Status != "Đang hiệu lực")
        {
            return BadRequest(new
            {
                message = "Trạng thái không hợp lệ. Trạng thái hiện được hỗ trợ là 'Đang hiệu lực'."
            });
        }
        // Kiểm tra ngày bắt đầu và ngày kết thúc
        if (request.StartDate.HasValue &&
            request.EndDate.HasValue &&
            request.StartDate > request.EndDate)
        {
            return BadRequest(new
            {
                message = "Ngày bắt đầu không được lớn hơn ngày kết thúc."
            });
        }

        // Lấy UserId từ JWT
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new
            {
                message = "Không xác định được người dùng đăng nhập."
            });
        }

        try
        {
            var contract = await _contractService.CreateAsync(
                request,
                userId);

            return CreatedAtAction(
                nameof(GetById),
                new { id = contract.ContractId },
                contract);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }

    // PUT: api/Contract/1
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin,Quản lý")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] ContractUpdateRequest request)
    {
        // Kiểm tra mã hợp đồng
        if (string.IsNullOrWhiteSpace(request.ContractCode))
        {
            return BadRequest(new
            {
                message = "Mã hợp đồng không được để trống."
            });
        }

        // Kiểm tra tên hợp đồng
        if (string.IsNullOrWhiteSpace(request.ContractName))
        {
            return BadRequest(new
            {
                message = "Tên hợp đồng không được để trống."
            });
        }

        // Kiểm tra trạng thái
        if (string.IsNullOrWhiteSpace(request.Status))
        {
            return BadRequest(new
            {
                message = "Trạng thái không được để trống."
            });
        }

        // Kiểm tra ngày
        if (request.StartDate.HasValue &&
            request.EndDate.HasValue &&
            request.StartDate > request.EndDate)
        {
            return BadRequest(new
            {
                message = "Ngày bắt đầu không được lớn hơn ngày kết thúc."
            });
        }

        try
        {
            var contract = await _contractService.UpdateAsync(
                id,
                request);

            if (contract == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy hợp đồng."
                });
            }

            return Ok(contract);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }

    // DELETE: api/Contract/1
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _contractService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Không tìm thấy hợp đồng."
            });
        }

        return Ok(new
        {
            message = "Xóa hợp đồng thành công."
        });
    }
}