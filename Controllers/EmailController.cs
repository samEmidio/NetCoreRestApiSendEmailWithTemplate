using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreRestApiSendEmailWithTemplate.Interface;

namespace NetCoreRestApiSendEmailWithTemplate.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ISendEmail iSendEmail;
        public EmailController(ISendEmail iSendEmail)
        {
            this.iSendEmail = iSendEmail;
        }

        [HttpGet]
        public ActionResult sendEmail(string email)
        {
            if(email == null){
                return NoContent();
            }

            try{
                iSendEmail.sendEmail(email,"toAddressTitle","subject","title","fromAddressTitle");
                return Ok();
            }catch(Exception){
                throw;
            }
            
        }

    }

}