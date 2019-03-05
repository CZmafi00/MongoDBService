using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBService
{
    public class MongoDatabase
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
