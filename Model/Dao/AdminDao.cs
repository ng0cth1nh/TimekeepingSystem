using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AdminDao
    {
        TimekeepingDbContext db = null;

        public AdminDao()
        {
            db = new TimekeepingDbContext();
        }

        public Admin GetByUserName(string userName)
        {
            return db.Admins.SingleOrDefault(x => x.UserName == userName);
        }
        public bool Login(string username, string password)
        {
            var res = db.Admins.Count(x => x.UserName == username && x.Password == password);
            if (res > 0)
            {
                return true;
            }

            return false;
        }
        
    }
}
