using System.Threading.Tasks;
using Example.Api.DtoModels;
using Example.Api.Transformers;
using Example.Api.Validators;
using Example.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Microsoft.Extensions.Logging;
namespace Example.Api.Controllers
{
    public class ExampleController : Controller
    {
        private IUserHandlers _userHandlers;
        private BasicTypesValidator _basicTypesValidator;
        private UserTransformersApi _transformersApi;
        private ILogger _logger;

        public ExampleController(IUserHandlers userHandlers, BasicTypesValidator basicTypesValidator, ILogger<ExampleController> logger, UserTransformersApi transformersApi)
        {
            _userHandlers = userHandlers;
            _basicTypesValidator = basicTypesValidator;
            _logger = logger;
            _transformersApi = transformersApi;
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUser([FromQuery] string name)
        {
            if (_basicTypesValidator.StringValidator(name))
                return BadRequest($"Name was invalid");
            
            var response = await _userHandlers.GetUser(name);

            if (response is null)
                return NotFound($"User with name:{name} was not found");

            return Ok(_transformersApi.ModelToDto(response));
        }
        
        [HttpPost("user")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                await _userHandlers.AddUser(_transformersApi.DtoToModel(user));

                return Ok(user);
            }

            return BadRequest(ModelState);
        }
        
        [HttpPut("user")]
        public async Task<IActionResult> EditUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                await _userHandlers.UpdateUser(_transformersApi.DtoToModel(user));

                return Ok(user);
            }

            return BadRequest(ModelState);
        }
        
        [HttpDelete("user")]
        public async Task<IActionResult> DeleteUser([FromQuery] string name)
        {
            if (_basicTypesValidator.StringValidator(name))
                return BadRequest($"Name is invalid");
            
            await _userHandlers.DeleteUser(name);

            return Ok(name);
        }
    }
}