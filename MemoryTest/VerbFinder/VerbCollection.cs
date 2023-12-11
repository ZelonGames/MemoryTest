using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest.VerbFinder
{
    public class VerbCollection
    {
        [JsonProperty("verbs")]
        public List<Verb>? Verbs { get; private set; }

        public static async Task<VerbCollection?> LoadVerbs()
        {
            string data = await File.ReadAllTextAsync("svenska verb.json");
            return JsonConvert.DeserializeObject<VerbCollection>(data);
        }

        public List<string> GetVerbsThatBeginsWith(string pattern)
        {
            var foundVerbs = new List<string>();

            foreach (var verb in Verbs!)
            {
                if (verb.Word!.Length >= pattern.Length && verb.Word[..pattern.Length] == pattern)
                    foundVerbs.Add(verb.Word);
            }

            return foundVerbs;
        }
    }
}
