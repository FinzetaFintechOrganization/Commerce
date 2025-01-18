using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class CargoDetailController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CargoDetailController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cargoDetails = await _unitOfWork.Repository<CargoDetail>().GetAllAsync();
        return Ok(cargoDetails);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var cargoDetail = await _unitOfWork.Repository<CargoDetail>().GetByIdAsync(id);
        if (cargoDetail == null)
        {
            return NotFound();
        }
        return Ok(cargoDetail);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CargoDetailCreateDto cargoDetailDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cargoDetail = new CargoDetail
        {
            Id = Guid.NewGuid(),
            Sender = cargoDetailDto.Sender,
            Receiver = cargoDetailDto.Receiver,
            Barcode = cargoDetailDto.Barcode,
            CompanyId = cargoDetailDto.CompanyId,
        };

        await _unitOfWork.Repository<CargoDetail>().AddAsync(cargoDetail);
        await _unitOfWork.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = cargoDetail.Id }, cargoDetail);
    }

    [HttpPost]
    public async Task<IActionResult> AddRange([FromBody] IEnumerable<CargoDetailCreateDto> cargoDetailDtos)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cargoDetails = new List<CargoDetail>();

        foreach (var dto in cargoDetailDtos)
        {
            cargoDetails.Add(new CargoDetail
            {
                Id = Guid.NewGuid(),
                Sender = dto.Sender,
                Receiver = dto.Receiver,
                Barcode = dto.Barcode,
                CompanyId = dto.CompanyId,
            });
        }

        await _unitOfWork.Repository<CargoDetail>().AddRangeAsync(cargoDetails);
        await _unitOfWork.SaveChangesAsync();
        return Ok(cargoDetails);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CargoDetailUpdateDto cargoDetailDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cargoDetail = await _unitOfWork.Repository<CargoDetail>().GetByIdAsync(cargoDetailDto.Id);
        if (cargoDetail == null)
        {
            return NotFound();
        }

        cargoDetail.Sender = cargoDetailDto.Sender;
        cargoDetail.Receiver = cargoDetailDto.Receiver;
        cargoDetail.Barcode = cargoDetailDto.Barcode;
        cargoDetail.CompanyId = cargoDetailDto.CompanyId;

        _unitOfWork.Repository<CargoDetail>().Update(cargoDetail);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var cargoDetail = await _unitOfWork.Repository<CargoDetail>().GetByIdAsync(id);
        if (cargoDetail == null)
        {
            return NotFound();
        }

        _unitOfWork.Repository<CargoDetail>().Remove(cargoDetail);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRange([FromBody] IEnumerable<Guid> ids)
    {
        var cargoDetails = new List<CargoDetail>();

        foreach (var id in ids)
        {
            var cargoDetail = await _unitOfWork.Repository<CargoDetail>().GetByIdAsync(id);
            if (cargoDetail != null)
            {
                cargoDetails.Add(cargoDetail);
            }
        }

        if (cargoDetails.Count == 0)
        {
            return NotFound();
        }

        _unitOfWork.Repository<CargoDetail>().RemoveRange(cargoDetails);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
