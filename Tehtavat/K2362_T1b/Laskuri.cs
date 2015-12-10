using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2362_T1b
{
    class Laskuri
    {
        private List<char> allowd = new List<char>();

        public int stringLength(string str_source)
        {
            return str_source.Length;
        }

        public List<string> calculateString(string str_source)
        {
            //Update allowd char
            string a = "abcdefghijklmnopqrstuvwyxzåäö";
            char[] b = a.ToCharArray();
            allowd.Clear();
            allowd.AddRange(b);

            //make lower case
            string sourceStr = str_source.ToLower();
            //init needed list
            List<char> Astr = sourceStr.ToList();
            List<char> parsedList = new List<char>();
            List<int> parsedListCount = new List<int>();
            List<string> finalList = new List<string>();

            for (int i = 0; i < Astr.Count; i++){


                //If char is in allowd list
                if (allowd.Contains(Astr[i]))  
                {
                    if (parsedList.Contains(Astr[i]))
                    {
                        //If in list

                        //find index number and add +1 to counter
                        for (int k = 0; k < parsedList.Count; k++)
                        {
                            if (parsedList.ElementAt(k) == Astr[i])
                            {
                                int tmpNum = parsedListCount[k];
                                tmpNum++;
                                parsedListCount[k] = tmpNum;
                            }
                        }


                    }
                    else
                    {
                        //if not in list
                        parsedList.Add(Astr[i]);
                        parsedListCount.Add(1);

                    }
                }

            }

            //Create a row to but in result
            for (int l = 0; l < parsedList.Count; l++)
            {
                string tmpStr = "";
                tmpStr += parsedList[l];
                tmpStr += " = ";
                tmpStr += parsedListCount[l];
                finalList.Add(tmpStr);
            }

            finalList.Sort();   //Sort final list
            return finalList;

        }
    }
}
