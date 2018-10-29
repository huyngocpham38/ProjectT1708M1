using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormStudent.Entity
{
    class KeyLogin
    {
        public string token { get; set; }

        public string secretToken { get; set; }

        public  string userId { get; set; }

        public int status { get; set; }

        public Dictionary<string, string> keyLogin { get; set; }
    }
}
