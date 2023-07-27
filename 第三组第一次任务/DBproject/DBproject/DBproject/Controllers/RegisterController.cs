using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System;

namespace DBproject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpPost()]
        [Consumes("application/json")]
        public IActionResult PostRegister([FromBody] RegisterModel model)
        {
            Console.WriteLine("注册信息如下:");
            Console.WriteLine("user_phone: " + model.user_phone);
            Console.WriteLine("user_password: " + model.user_password);
            Console.WriteLine("user_address: " + model.user_address);
            Console.WriteLine("user_type (0顾客1商家): " + model.user_type);

            string message = DBproject.DataBase.oracleCon.RegisterUser(model.user_phone, model.user_password, model.user_address, model.user_type);
            RegisterReturnModel returnmodel = new RegisterReturnModel();
            if (char.IsDigit(message[0]))
            {
                returnmodel.user_ID = message;
                Console.WriteLine("New_ID:"+message);
                returnmodel.user_type = model.user_type;
                returnmodel.message = "success";
            }
            else
            {
                returnmodel.user_ID = "0000000";
                returnmodel.user_type = model.user_type;
                if (message.Contains("违反唯一约束条件") && message.Contains("USERS_UK1"))
                {
                    returnmodel.message = "电话号码已经存在";
                }
                else
                {
                    returnmodel.message = message;
                }

            }
            return new JsonResult(returnmodel);
        }

        [HttpPost("store")]
        [Consumes("multipart/form-data")]
        public IActionResult PostStoreRegister([FromForm] RegisterStoreModel model)
        {
            RegisterStoreReturnModel returnmodel = new RegisterStoreReturnModel();
            try
            {
                // 使用 model 对象中的属性来访问表单数据
                Console.WriteLine("接收到商家基本信息如下：");
                Console.WriteLine("sto_ID：" + model.sto_ID);
                Console.WriteLine("sto_name：" + model.sto_name);
                Console.WriteLine("sto_introduction：" + model.sto_introduction);
                Console.WriteLine("sto_licenseImage名称：" + model.sto_licenseImage.FileName);

                // 判断文件格式是否正确
                string fileExtension = Path.GetExtension(model.sto_licenseImage.FileName);
                if (fileExtension != ".jpg" && fileExtension != ".png" && fileExtension != ".gif")
                {
                    returnmodel.message = "图片格式不正确";
                    Console.WriteLine(returnmodel.message);
                    return new JsonResult(returnmodel);
                }
                //**********************************************************************************************************************
                //**********************************************************************************************************************
                string uploadPath = "D:\\Database-Project\\Register_function\\Test\\formalDBProject\\DBproject\\DBproject\\wwwroot\\licenses";  //此处需要修改到本地保存路径
                string newFileName = model.sto_ID + "_license" + fileExtension;      //图片命名规范为（商家ID+"_license"+文件拓展名）
                string filePath = Path.Combine(uploadPath, newFileName);
                Console.WriteLine("filePath：" + filePath);
                string fileURL = "images\\" + newFileName;
                //**********************************************************************************************************************
                //**********************************************************************************************************************

                returnmodel.message = DBproject.DataBase.oracleCon.RegiserSto(model.sto_ID, model.sto_name, model.sto_introduction, fileURL);
                if (returnmodel.message != "success")
                {
                    if (returnmodel.message.Contains("违反唯一约束条件") && returnmodel.message.Contains("STORE_PK"))
                    {
                        returnmodel.message = "商家已经存在，无法重新创建";
                    }
                    Console.WriteLine(returnmodel.message);
                    return new JsonResult(returnmodel);
                }
                //将文件保存到指定路径
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.sto_licenseImage.CopyTo(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"图片处理失败：{ex.Message}");
                returnmodel.message = $"图片处理失败：{ex.Message}";
            }
            return new JsonResult(returnmodel);
        }

    }
    public class RegisterModel
    {
        public string user_phone { get; set; }
        public string user_password { get; set; }
        public string user_address { get; set; }
        public string user_type { get; set; }
    }
    public class RegisterReturnModel
    {
        public string user_ID { get; set; } 
        public string user_type { get; set; }
        public string message { get; set; }
    }
    public class RegisterStoreModel
    {
        public string sto_ID { get; set; }
        public string sto_name { get; set; }
        public string sto_introduction { get; set; }
        public IFormFile sto_licenseImage { get; set; }
    }
    public class RegisterStoreReturnModel
    {
        public string message { get; set; }
    }
}
