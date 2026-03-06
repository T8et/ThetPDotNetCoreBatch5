using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.JsonObject
{
    public static class Extension
    {
        public static string toJson(this object obj)
        {
            string retjson = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return retjson;
        }
    }
}
