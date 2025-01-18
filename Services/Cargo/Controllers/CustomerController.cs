using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _unitOfWork.Repository<Customer>().GetAllAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CustomerCreateDto customerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = customerDto.Name,
            Surname = customerDto.Surname,
            Email = customerDto.Email,
            Phone = customerDto.Phone,
            Address = customerDto.Address,
            District = customerDto.District,
            City = customerDto.City,
        };

        await _unitOfWork.Repository<Customer>().AddAsync(customer);
        await _unitOfWork.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }

    [HttpPost]
    public async Task<IActionResult> AddRange([FromBody] IEnumerable<CustomerCreateDto> customerDtos)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var customers = new List<Customer>();

        foreach (var dto in customerDtos)
        {
            customers.Add(new Customer
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                District = dto.District,
                City = dto.City,
            });
        }

        await _unitOfWork.Repository<Customer>().AddRangeAsync(customers);
        await _unitOfWork.SaveChangesAsync();
        return Ok(customers);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CustomerUpdateDto customerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(customerDto.Id);
        if (customer == null)
        {
            return NotFound();
        }

        customer.Name = customerDto.Name;
        customer.Surname = customerDto.Surname;
        customer.Email = customerDto.Email;
        customer.Phone = customerDto.Phone;
        customer.Address = customerDto.Address;
        customer.District = customerDto.District;
        customer.City = customerDto.City;

        _unitOfWork.Repository<Customer>().Update(customer);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        _unitOfWork.Repository<Customer>().Remove(customer);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRange([FromBody] IEnumerable<Guid> ids)
    {
        var customers = new List<Customer>();

        foreach (var id in ids)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(id);
            if (customer != null)
            {
                customers.Add(customer);
            }
        }

        if (customers.Count == 0)
        {
            return NotFound();
        }

        _unitOfWork.Repository<Customer>().RemoveRange(customers);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
