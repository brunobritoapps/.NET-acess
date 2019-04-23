using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcessTower.myDomain
{
    public class myUserDto
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string  lastName { get; set; }
        public string jobTitle { get; set; }

        public myUserDto()
        {
            id = "";
            firstName = "";
            lastName = "";
            jobTitle = "";
        }
    }
}
