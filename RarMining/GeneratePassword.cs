using System;
using System.Collections.Generic;
using System.Text;

namespace RarMining
{
    class GeneratePassword
    {

        public int LengthOfPasswd { get; set; }
        public char[] SetOfChar { get; set; }
        public int LengthOfDirChar { get; set; }
        public bool IsDone { get; set; }
        public List<int> IndexOfDicChar { get; set; }
        public GeneratePassword(int lengthOfPasswd,string setOfChar)
        {
            LengthOfPasswd = lengthOfPasswd;
            LengthOfDirChar = setOfChar.Length;
            SetOfChar = new char[LengthOfDirChar];
            IndexOfDicChar = new List<int>();
            for (int i = 0; i < LengthOfDirChar; i++)
            {
                SetOfChar[i] = setOfChar[i];
            }

            for (int i = 0; i < LengthOfPasswd; i++)
            {
                IndexOfDicChar.Add(0);
            }
        }

        public bool IncreaseIndexOfDicChar()
        {
            int tmpIndex = 0;
            while (++IndexOfDicChar[tmpIndex]>=LengthOfDirChar)
            {
                if (tmpIndex == LengthOfPasswd - 1)
                {
                    IsDone = true;
                    return false;
                }
                IndexOfDicChar[tmpIndex++] = 0;
            }
            return true;

        }


        public string GetNextPasswd()
        {
            string passwd = "";
            if (IncreaseIndexOfDicChar())
            {
                for (int i = 0; i < LengthOfPasswd; i++)
                {
                    passwd += SetOfChar[IndexOfDicChar[i]];
                }
            }
            else return "DONE";

            return passwd;
        }

        public void PrintPasswd()
        {
            
        }
    }
}
