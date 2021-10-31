using API.RequestModels.Accounts;
using AutoMapper;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get(Guid Id)
        {
            try
            {
                var account = _accountService.Get(x => x.Id == Id, null);
                return StatusCode(200, new
                {
                    Message = "OK",
                    Data = account,
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public IActionResult Create(CreateAccount createAccount)
        {
            try
            {
                //Account account = new Account();
                //account.Avatar = createAccount.Avatar;
                //account.Email = createAccount.Email;
                //account.Id = new Guid();
                //account.Password = createAccount.Password;
                //account.Username = createAccount.Username;
                //account.Status = (int)AccountStatus.enable;
                var account = _mapper.Map<Account>(createAccount);
                _accountService.Create(account);
                return StatusCode(200, new
                {
                    message = "Create Success",
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody] UpdateAccount updateAccount)
        {
            try
            {
                var account = _accountService.Get(x => x.Id == updateAccount.Id, null);
                account.Email = updateAccount.Email;
                account.Avatar = updateAccount.Avatar;
                _accountService.Update(account);
                return StatusCode(200, new
                {
                    message = "Update Success",
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
