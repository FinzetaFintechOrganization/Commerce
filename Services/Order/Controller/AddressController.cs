using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/[action]")]
[ApiController]

public class AddressController : ControllerBase
{
    private readonly GetAddressQueryHandler _getAddressQueryHandler;
    private readonly GetAddressByIdQueryHandler _getAddressByIdQueryHandler;
    private readonly CreateAddressCommandHandler _createAddressCommandHandler;
    private readonly UpdateAddressCommandHandler _updateAddressCommandHandler;
    private readonly RemoveAddressCommandHandler _removeAddressCommandHandler;

    public AddressController(GetAddressQueryHandler getAddressQueryHandler, GetAddressByIdQueryHandler getAddressByIdQueryHandler, CreateAddressCommandHandler createAddressCommandHandler, UpdateAddressCommandHandler updateAddressCommandHandler, RemoveAddressCommandHandler removeAddressCommandHandler)
    {
        _getAddressQueryHandler = getAddressQueryHandler;
        _getAddressByIdQueryHandler = getAddressByIdQueryHandler;
        _createAddressCommandHandler = createAddressCommandHandler;
        _updateAddressCommandHandler = updateAddressCommandHandler;
        _removeAddressCommandHandler = removeAddressCommandHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAddress()
    {
        var values = await _getAddressQueryHandler.Handle();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAddressById(Guid id)
    {
        var value = await _getAddressByIdQueryHandler.Handle(new GetAddressByIdQuery(id));
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress(CreateAddressCommand command)
    {
        await _createAddressCommandHandler.Handle(command);
        return Ok("Adres Başarıyla Eklendi");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAddress(UpdateAddressCommand command)
    {
        await _updateAddressCommandHandler.Handle(command);
        return Ok("Adres Başarıyla Güncellendi");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAddress(RemoveAddressCommand command)
    {
        await _removeAddressCommandHandler.Handle(command);
        return Ok("Adres Başarıyla Silindi.");
    }
}