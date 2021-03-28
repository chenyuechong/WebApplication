using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DB
{
    public class commonfunctions
    {
        public string generateToken()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            return GuidString; 
        }

        public string findIdByToken(string token, PetitionDBOperate db)
        {
            string query = @"select users.user_id as userId from users  
                            where 
                            users.auth_token='" + token + @"'
                            ";
            DataTable table = db.excuteSQL(query);
            if (table.Rows.Count == 0)
                return "";
            
            return table.Rows[0].ToString();

            

        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool IsEmailExist(string email, PetitionDBOperate db)
        {
            string query = @"select users.user_id as userId from users  
                            where 
                            users.email='" + email + @"'
                            ";
            DataTable table = db.excuteSQL(query);
            if (table.Rows.Count == 0)
                return false;
            return true;
        }

        public bool IsUser(string token, int id, PetitionDBOperate db)
        {
            string query = @"select user_id from users where auth_token='" + token + @"'";
            DataTable b = db.excuteSQL(query);
            string userId = b.Rows[0][0].ToString();
            if (userId != id.ToString())
                return false;
            else
                return true;
        }

    }
}
