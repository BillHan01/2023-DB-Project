using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;


namespace DBproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : Controller
    {
        [HttpGet("userlist")]
        public ActionResult userlist([FromQuery] QueryModel queryModel)
        {
            List<UserModel> users = new List<UserModel>();
            string sql = "";
            if (queryModel.USER_TYPE == 0)
            {
                string orderOption = "";
                if (queryModel.NAME_ORDER == 0) orderOption = "order by NLSSORT(CUS_NICKNAME,'NLS_SORT=SCHINESE_PINYIN_M') DESC) ";
                else if (queryModel.NAME_ORDER == 1) orderOption = "order by NLSSORT(CUS_NICKNAME,'NLS_SORT=SCHINESE_PINYIN_M') ASC) ";
                else if (queryModel.ID_ORDER == 0) orderOption = "order by CUS_ID DESC) ";
                else if (queryModel.ID_ORDER == 1) orderOption = "order by CUS_ID ASC) ";
                sql = @"SELECT CUS_ID,CUS_NICKNAME,USER_REGTIME,USER_STATE FROM(
                        SELECT CUS_ID,CUS_NICKNAME,USER_REGTIME,USER_STATE,ROWNUM as row_num
                        FROM CUSTOMER  JOIN USERS ON customer.cus_id = USERS.USER_ID
                        where CUS_NICKNAME like :USER_NAME " + orderOption +
                        "where row_num>=:BEGIN_NUMBER and row_num<=:END_NUMBER";
                using (OracleCommand selectCommand = new OracleCommand(sql, DBproject.DataBase.oracleCon.con))
                {
                    if (queryModel.USER_NAME.Equals("NULL")) queryModel.USER_NAME = "";
                    selectCommand.Parameters.Add(new OracleParameter(":USER_NAME", "%" + queryModel.USER_NAME + "%"));
                    selectCommand.Parameters.Add(new OracleParameter(":BEGIN_NUMBER", queryModel.BEGIN_NUMBER));
                    selectCommand.Parameters.Add(new OracleParameter(":END_NUMBER", queryModel.END_NUMBER));
                    using (OracleDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserModel userModel = new UserModel();
                            userModel.USER_ID = reader.GetInt32("CUS_ID");
                            userModel.USER_NAME = reader.GetString("CUS_NICKNAME");
                            userModel.USER_REGTIME = reader.GetDateTime("USER_REGTIME").ToString("yyyy-MM-dd");
                            userModel.USER_STATE = reader.GetInt32("USER_STATE");
                            userModel.STO_IMAGE = "NULL";
                            users.Add(userModel);
                        }
                    }
                }
            }
            else
            {
                string orderOption = "";
                if (queryModel.NAME_ORDER == 0) orderOption = "order by NLSSORT(STORE_WITH_IMAGE.STO_NAME,'NLS_SORT=SCHINESE_PINYIN_M') DESC) ";
                else if (queryModel.NAME_ORDER == 1) orderOption = "order by NLSSORT(STORE_WITH_IMAGE.STO_NAME,'NLS_SORT=SCHINESE_PINYIN_M') ASC) ";
                else if (queryModel.ID_ORDER == 0) orderOption = "order by STORE_WITH_IMAGE.STO_ID DESC) ";
                else if (queryModel.ID_ORDER == 1) orderOption = "order by STORE_WITH_IMAGE.STO_ID ASC) ";
                sql = @"WITH STORE_WITH_IMAGE AS (
                    SELECT STORE.*, ci.STO_IMAGE
                    FROM STORE
                    JOIN(
                        SELECT STO_ID, MIN(STO_IMAGE) AS STO_IMAGE
                        FROM STOREIMAGE
                        GROUP BY STO_ID
                    ) ci ON STORE.STO_ID = ci.STO_ID
                )
                SELECT STO_ID,STO_NAME, USER_REGTIME,USER_STATE,STO_IMAGE FROM(               
                SELECT STORE_WITH_IMAGE.STO_ID,STORE_WITH_IMAGE.STO_NAME,USERS.USER_REGTIME,USERS.USER_STATE,STORE_WITH_IMAGE.STO_IMAGE,ROWNUM as row_num
                FROM STORE_WITH_IMAGE JOIN USERS ON STORE_WITH_IMAGE.STO_ID = USERS.USER_ID
                WHERE STORE_WITH_IMAGE.STO_NAME LIKE :USER_NAME " + orderOption +
                " where row_num>=:BEGIN_NUMBER and row_num<=:END_NUMBER";
                using (OracleCommand selectCommand = new OracleCommand(sql, DBproject.DataBase.oracleCon.con))
                {
                    if (queryModel.USER_NAME.Equals("NULL")) queryModel.USER_NAME = "";
                    selectCommand.Parameters.Add(new OracleParameter(":USER_NAME", "%" + queryModel.USER_NAME + "%"));
                    selectCommand.Parameters.Add(new OracleParameter(":BEGIN_NUMBER", queryModel.BEGIN_NUMBER));
                    selectCommand.Parameters.Add(new OracleParameter(":END_NUMBER", queryModel.END_NUMBER));
                    using (OracleDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserModel userModel = new UserModel();
                            userModel.USER_ID = reader.GetInt32("STO_ID");
                            userModel.USER_NAME = reader.GetString("STO_NAME");
                            userModel.USER_REGTIME = reader.GetDateTime("USER_REGTIME").ToString("yyyy-MM-dd");
                            userModel.USER_STATE = reader.GetInt32("USER_STATE");
                            userModel.STO_IMAGE = reader.GetString("STO_IMAGE");
                            users.Add(userModel);
                        }
                    }
                }
            }


            string response = JsonConvert.SerializeObject(users);
            Console.WriteLine(response);
            return Ok(response);
        }
    }

    public class QueryModel
    {
        public int ID_ORDER { get; set; }
        public int NAME_ORDER { get; set; }
        public string USER_NAME { get; set; }
        public int USER_TYPE { get; set; }
        public int BEGIN_NUMBER { get; set; }
        public int END_NUMBER { get; set; }
    }

    public class UserModel
    {
        public int USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string USER_REGTIME { get; set; }
        public int USER_STATE { get; set; }
        public string STO_IMAGE { get; set; }
        public UserModel() { }
    }
}
