using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimekeepingSystem.Common
{
    [Serializable]
    public class UserLogin
    {
        public string UserName { get; set; }
        public string Name { get; set; }
    }
}