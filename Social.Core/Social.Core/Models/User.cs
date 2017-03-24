using Social.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Core.Models
{
    public class User : IUser
    {
        public string UserName { get; set; }
        public override bool Equals(object obj)
        {
            var objUsr = obj as IUser;
            if (objUsr == null) return false;
            return !string.IsNullOrEmpty(UserName) 
                            && !string.IsNullOrEmpty(objUsr.UserName) 
                            && objUsr.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(UserName)) return base.GetHashCode();
            return UserName.GetHashCode();
        }
    }
}
