using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest.PAO
{
    public class PAOData
    {
        [JsonProperty]
        private List<PAOItem>? PAOList { get; set; }

        [JsonIgnore]
        public Dictionary<string, PAOItem> PAOItems { get; private set; }

        public PAOData() 
        {
            PAOItems = new Dictionary<string, PAOItem>();
        }

        public static async Task<PAOData?> LoadPAO(string file)
        {
            string data = await File.ReadAllTextAsync(file);
            var paoData = JsonConvert.DeserializeObject<PAOData>(data);
            if (paoData == null)
                return null;

            foreach (var item in paoData.PAOList!)
                paoData.PAOItems.Add(item.Key!, item);

            return paoData;
        }
        
        public static PAOData Merge(PAOData paoData1, PAOData paoData2)
        {
            var paoData = new PAOData();

            foreach (var item in paoData1.PAOItems!)
                paoData.PAOItems.Add(item.Key, item.Value);

            foreach (var item in paoData2.PAOItems!)
                paoData.PAOItems.Add(item.Key, item.Value);

            return paoData;
        }

        public List<string> GetAutoCompletePersonList(string input)
        {
            return GetAutoCompleteList(input, PAOItems.Select(x => x.Value.Person).ToList());
        }

        public List<string> GetAutoCompleteActionList(string input)
        {
            return GetAutoCompleteList(input, PAOItems.Select(x => x.Value.Action).ToList());
        }

        public List<string> GetAutoCompleteObjectList(string input)
        {
            return GetAutoCompleteList(input, PAOItems.Select(x => x.Value.Object).ToList());
        }

        private List<string> GetAutoCompleteList(string input, List<string?> itemTypeList)
        {
            var people = new List<string>();

            foreach (var item in itemTypeList)
            {
                if (item != null && item.Length >= input.Length && item[..input.Length].ToLower() == input.ToLower())
                    people.Add(item);
            }

            return people;
        }

        public string GetLongStoryFromSequence(string sequence)
        {
            string story = "";

            if (sequence.Contains(' '))
            {
                string[] stories = sequence.Split(' ');
                foreach (string shortStory in stories)
                    story += GetStoryFromNumber(shortStory) + "\n";
            }
            else
            {
                for (int i = 0; i < sequence.Length; i += 6)
                {
                    string chunk = sequence[i..Math.Min(sequence.Length, i + 6)];
                    story += GetStoryFromNumber(chunk) + "\n";
                }
            }

            return story;
        }

        public string GetStoryFromNumber(string number)
        {
            string story = "";
            string[] numbers = !number.Contains(' ') ? GetPAONumberWithoutSpaces(number).ToArray() : number.Split(' ');
            if (numbers.Length == 0)
                return "";

            for (int i = 0; i < numbers.Length; i += 3)
            {
                if (!PAOItems.ContainsKey(numbers[i]) ||
                    numbers.Length > i + 1 && !PAOItems.ContainsKey(numbers[i + 1]) ||
                    numbers.Length > i + 2 && !PAOItems.ContainsKey(numbers[i + 2]))
                    return "";

                story += PAOItems![numbers[i]].Person + " ";
                if (numbers.Length > i + 1)
                    story += PAOItems[numbers[i + 1]].Action + " ";
                if (numbers.Length > i + 2)
                    story += PAOItems[numbers[i + 2]].Object;
            }

            return story;
        }

        public string GetNumberFromStory(string story)
        {
            string[] paoStory = story.Split(' ');

            string person = PAOList!.Where(x => x.Person == paoStory[0]).FirstOrDefault()?.Key!;
            string action = PAOList!.Where(x => x.Action == paoStory[1]).FirstOrDefault()?.Key!;
            string @object = PAOList!.Where(x => x.Object == paoStory[2]).FirstOrDefault()?.Key!;

            return $"{person} {action} {@object}";
        }

        private List<string> GetPAONumberWithoutSpaces(string numberWithoutSpace)
        {
            var numbers = new List<string>(3);
            string currentNumber = "";

            for (int i = 0; i < numberWithoutSpace.Length; i++)
            {
                currentNumber += numberWithoutSpace[i];
                if (i % 2 == 1)
                {
                    numbers.Add(currentNumber);
                    if (numbers.Count == numberWithoutSpace.Length / 2)
                        return numbers;

                    currentNumber = "";
                }
            }

            return numbers;
        }
    }
}
