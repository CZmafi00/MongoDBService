using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBService
{
    public static class CollectionHelper
    {
        public static string GetTimeOrientedName(this MongoCollection collection)
        {
            return $"{collection.Name}_{DateTime.Now.ToString("yyyyMMdd")}";
        }
    }
}
