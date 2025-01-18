using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _unitOfWork.Repository<Company>().GetAllAsync();
        var companiesDto = companies.Select(c => new CompanyDto
        {
            Id = c.Id,
            CompanyName = c.CompanyName
        });
        return Ok(companiesDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var company = await _unitOfWork.Repository<CompanyDto>().GetByIdAsync(id);
        if (company == null)
        {
            return NotFound();
        }
        return Ok(company);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CompanyCreateDto companyDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var company = new Company
        {
            Id = Guid.NewGuid(),
            CompanyName = companyDto.CompanyName,
        };

        await _unitOfWork.Repository<Company>().AddAsync(company);
        await _unitOfWork.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = company.Id }, company);
    }

    [HttpPost]
    public async Task<IActionResult> AddRange([FromBody] IEnumerable<CompanyCreateDto> companyDtos)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var companies = new List<Company>();

        foreach (var dto in companyDtos)
        {
            companies.Add(new Company
            {
                Id = Guid.NewGuid(),
                CompanyName = dto.CompanyName,

            });
        }

        await _unitOfWork.Repository<Company>().AddRangeAsync(companies);
        await _unitOfWork.SaveChangesAsync();
        return Ok(companies);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CompanyUpdateDto companyDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var company = await _unitOfWork.Repository<Company>().GetByIdAsync(companyDto.Id);
        if (company == null)
        {
            return NotFound();
        }

        company.CompanyName = companyDto.CompanyName;

        _unitOfWork.Repository<Company>().Update(company);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var company = await _unitOfWork.Repository<Company>().GetByIdAsync(id);
        if (company == null)
        {
            return NotFound();
        }

        _unitOfWork.Repository<Company>().Remove(company);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRange([FromBody] IEnumerable<Guid> ids)
    {
        var companies = new List<Company>();

        foreach (var id in ids)
        {
            var company = await _unitOfWork.Repository<Company>().GetByIdAsync(id);
            if (company != null)
            {
                companies.Add(company);
            }
        }

        if (companies.Count == 0)
        {
            return NotFound();
        }

        _unitOfWork.Repository<Company>().RemoveRange(companies);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
