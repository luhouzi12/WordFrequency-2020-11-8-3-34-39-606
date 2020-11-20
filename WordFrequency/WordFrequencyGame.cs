using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WordFrequency
{
    public class WordFrequencyGame
    {
        public string GetResult(string inputStr)
        {
            const string SpacePattern = @"\s+";
            if (Regex.Split(inputStr, SpacePattern).Length == 1)
            {
                return inputStr + " 1";
            }

            var inputList = SplitInputToList(inputStr);

            //get the map for the next step of sizing the same word
            var map = GetListMap(inputList);

            var list = new List<Input>();
            foreach (var entry in map)
            {
                var input = new Input(entry.Key, entry.Value.Count);
                list.Add(input);
            }

            inputList = list;

            inputList.Sort((wordCount1, wordCount2) => wordCount2.WordCount - wordCount1.WordCount);

            List<string> strList = RenderInputList(inputList);

            return string.Join("\n", strList.ToArray());
        }

        private static List<string> RenderInputList(List<Input> inputList)
        {
            List<string> strList = new List<string>();
            foreach (Input w in inputList)
            {
                string s = w.Value + " " + w.WordCount;
                strList.Add(s);
            }

            return strList;
        }

        private static List<Input> SplitInputToList(string inputStr)
        {
            //split the input string with 1 to n pieces of spaces
            string spacePattern = @"\s+";
            string[] arr = Regex.Split(inputStr, spacePattern);

            List<Input> inputList = new List<Input>();
            foreach (var splitString in arr)
            {
                Input input = new Input(splitString, 1);
                inputList.Add(input);
            }

            return inputList;
        }

        private Dictionary<string, List<Input>> GetListMap(List<Input> inputList)
        {
            Dictionary<string, List<Input>> map = new Dictionary<string, List<Input>>();
            foreach (var input in inputList)
            {
                if (!map.ContainsKey(input.Value))
                {
                    List<Input> arr = new List<Input>();
                    arr.Add(input);
                    map.Add(input.Value, arr);
                }
                else
                {
                    map[input.Value].Add(input);
                }
            }

            return map;
        }
    }
}
