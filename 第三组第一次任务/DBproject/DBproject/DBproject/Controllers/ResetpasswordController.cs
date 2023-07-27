using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System;


namespace DBproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetpasswordController : ControllerBase
    {
        [HttpPost()]
        [Consumes("application/json")]
        public IActionResult ResetPost([FromBody] ResetModel model)
        {
            Console.WriteLine("修改信息如下");
            Console.WriteLine("user_phonenumber:" + model.user_phonenumber);
            Console.WriteLine("user_new_password:" + model.user_new_password);
            string message = DBproject.DataBase.oracleCon.ResetPassword(model.user_phonenumber, model.user_new_password);

            ResetReturnModel returnmodel = new ResetReturnModel();
            returnmodel.message = message;

            return new JsonResult(returnmodel);
        }
    }

    public class ResetModel
    {
        public string user_phonenumber { get; set; }
        public string user_new_password { get; set; }
    }
    public class ResetReturnModel
    {
        public string message { get; set; }
    }
}
