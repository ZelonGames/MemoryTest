using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest.PAO
{
    public class PAOItem
    {
        [JsonProperty]
        public string? Key { get; private set; }
        [JsonProperty]
        public string? Person { get; private set; }
        [JsonProperty]
        public string? Action { get; private set; }
        [JsonProperty]
        public string? Object { get; private set; }
    }
}
