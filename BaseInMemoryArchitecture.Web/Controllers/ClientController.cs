using System.Collections.Generic;
using AutoMapper;
using BaseInMemoryArchitecture.BusinessLogic.Contracts;
using BaseInMemoryArchitecture.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BaseInMemoryArchitecture.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : BaseController
    {
        private IClientService _clientService;

        public ClientController(IConfiguration configuration, IMapper mapper, IClientService clientService) : base(configuration, mapper)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            var result = _mapper.Map<IList<Client>>(_clientService.GetAll());

            return Ok(result);
        }

        [HttpDelete]
        [Route("{clientId}")]
        public IActionResult Delete(int clientId)
        {
            var client = _clientService.GetById(clientId);

            if (client == null)
                return NotFound();

            _clientService.RemoveById(clientId);

            return Ok();
        }
    }
}
