using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest.PAO.ChessSquarePAO
{
    public class ConsoleInputHandler : ChessSquareInputHandler
    {
        private readonly PAOData pAOData;

        public ConsoleInputHandler(PAOData pAOData)
        {
            this.pAOData = pAOData;
        }

        public override void WaitForInput()
        {
            string? input = null;

            while (input == null)
                input = Console.ReadLine();

            // Person
            var autoCompleteList = new List<string>();
            while (autoCompleteList.Count == 0)
            {
                autoCompleteList = pAOData.GetAutoCompletePersonList(input!);
                if (autoCompleteList.Count == 0)
                    input = Console.ReadLine();
            }

            string? autoCompleteChoice = null;
            if (!autoCompleteList.Any(x => x.ToLower() == input!.ToLower()))
            {
                for (int i = 0; i < autoCompleteList.Count; i++)
                    Console.WriteLine($"[{i}] {autoCompleteList[i]}");

                autoCompleteChoice = Console.ReadLine();
                input = autoCompleteList[Convert.ToInt32(autoCompleteChoice)] + " ";
            }
            else
                input += " ";

            Console.WriteLine(input);

            // Action
            autoCompleteList = new List<string>();
            string? action = null;
            while (autoCompleteList.Count == 0)
            {
                action = Console.ReadLine();
                autoCompleteList = pAOData.GetAutoCompleteActionList(action!);
            }

            if (!autoCompleteList.Any(x => x.ToLower() == action!.ToLower()))
            {
                for (int i = 0; i < autoCompleteList.Count; i++)
                    Console.WriteLine($"[{i}] {autoCompleteList[i]}");

                autoCompleteChoice = Console.ReadLine();
                input += autoCompleteList[Convert.ToInt32(autoCompleteChoice)] + " ";
            }
            else
                input += action + " ";

            Console.WriteLine(input);

            // Object
            autoCompleteList = new List<string>();
            string? @object = null;
            while (autoCompleteList.Count == 0)
            {
                @object = Console.ReadLine();
                autoCompleteList = pAOData.GetAutoCompleteObjectList(@object!);
            }

            if (!autoCompleteList.Any(x => x.ToLower() == @object!.ToLower()))
            {
                for (int i = 0; i < autoCompleteList.Count; i++)
                    Console.WriteLine($"[{i}] {autoCompleteList[i]}");

                autoCompleteChoice = Console.ReadLine();
                input += autoCompleteList[Convert.ToInt32(autoCompleteChoice)];
            }
            else
                input += @object;

            Console.WriteLine(input);

            InvokeEvent(input);
        }
    }
}
