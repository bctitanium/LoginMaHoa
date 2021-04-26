using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class profile
    {
        private string username;

        public string UsernamePf { get => username; set => username = value; }

        public profile()
        {

        }

        public profile(string username)
        {
            UsernamePf = username;
        }        
    }
}
