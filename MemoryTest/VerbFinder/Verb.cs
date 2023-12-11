using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest.VerbFinder
{
    public class Verb
    {
        [JsonProperty("infinitiv")]
        public string? Word { get; private set; }
    }
}
