using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class OperationController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public OperationController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var operations = await _unitOfWork.Repository<Operation>().GetAllAsync();
        return Ok(operations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var operation = await _unitOfWork.Repository<Operation>().GetByIdAsync(id);
        if (operation == null)
        {
            return NotFound();
        }
        return Ok(operation);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] OperationCreateDto operationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var operation = new Operation
        {
            Id = Guid.NewGuid(),
            Barcode = operationDto.Barcode,
            Description = operationDto.Description,
            OperationDate = operationDto.OperationDate
        };

        await _unitOfWork.Repository<Operation>().AddAsync(operation);
        await _unitOfWork.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = operation.Id }, operation);
    }

    [HttpPost]
    public async Task<IActionResult> AddRange([FromBody] IEnumerable<OperationCreateDto> operationDtos)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var operations = new List<Operation>();

        foreach (var dto in operationDtos)
        {
            operations.Add(new Operation
            {
                Id = Guid.NewGuid(),
                Barcode = dto.Barcode,
                Description = dto.Description,
                OperationDate = dto.OperationDate
            });
        }

        await _unitOfWork.Repository<Operation>().AddRangeAsync(operations);
        await _unitOfWork.SaveChangesAsync();
        return Ok(operations);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] OperationUpdateDto operationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var operation = await _unitOfWork.Repository<Operation>().GetByIdAsync(operationDto.Id);
        if (operation == null)
        {
            return NotFound();
        }

        operation.Barcode = operationDto.Barcode;
        operation.Description = operationDto.Description;
        operation.OperationDate = operationDto.OperationDate;

        _unitOfWork.Repository<Operation>().Update(operation);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var operation = await _unitOfWork.Repository<Operation>().GetByIdAsync(id);
        if (operation == null)
        {
            return NotFound();
        }

        _unitOfWork.Repository<Operation>().Remove(operation);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRange([FromBody] IEnumerable<Guid> ids)
    {
        var operations = new List<Operation>();

        foreach (var id in ids)
        {
            var operation = await _unitOfWork.Repository<Operation>().GetByIdAsync(id);
            if (operation != null)
            {
                operations.Add(operation);
            }
        }

        if (operations.Count == 0)
        {
            return NotFound();
        }

        _unitOfWork.Repository<Operation>().RemoveRange(operations);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
