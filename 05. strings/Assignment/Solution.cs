using System.Text;

namespace Assignment
{
    class Solution
    {
        internal static int WordCount(string text)
        {
            int wordLength = 0;
            int wordCount = 0;
            foreach (var letter in text)
            {
                if (letter == ' ' && wordLength > 0)
                {
                    wordCount++;
                    wordLength = 0;
                }
                else
                {
                    wordLength++;
                }
            }
            if (wordLength > 0)
            {
                wordCount++;
            }
            return wordCount;

            //return text.Split(' ').Length;
        }
        internal static int SentenceCount(string text)
        {
            int sentenceCount = 0;
            foreach (var letter in text)
            {
                if (letter == '.')
                {
                    sentenceCount++;
                }
            }
            return sentenceCount;

            //return text.Split('.').Length - 1;
        }
        internal static int CountOccurances(string text, string occurance)
        {
            int occurancesCounter = 0;
            for (int i = 0; i < text.Length; i++)
            {
                bool foundMatch = true;
                for (int j = 0; j < occurance.Length; j++)
                {
                    if (text[i + j] != occurance[j])
                    {
                        foundMatch = false;
                        break;
                    }
                }
                if (foundMatch)
                {
                    occurancesCounter++;
                    i += occurance.Length; // jump ahead
                }
            }

            return occurancesCounter;

            //var query = from word in text.Split(' ', '.')
            //            where word == occurance
            //            select word;
            //return query.Count();
        }
        internal static string ReverseString(string text)
        {
            StringBuilder stringBuilder = new StringBuilder(text.Length);

            for (int i = text.Length - 1; i >= 0; i--)
            {
                stringBuilder.Append(text[i]);
            }

            return stringBuilder.ToString();
        }
        internal static string ReplaceOccurances(string text, string occurance, string replaceWith)
        {
            StringBuilder stringBuilder = new StringBuilder(text.Length);

            for (int i = 0; i < text.Length; i++)
            {
                bool foundMatch = true;
                for (int j = 0; j < occurance.Length; j++)
                {
                    if (text[i + j] != occurance[j])
                    {
                        foundMatch = false;
                        break;
                    }
                }
                if (foundMatch)
                {
                    stringBuilder.Append(replaceWith);
                    i += occurance.Length; // jump ahead
                    
                    if (i >= text.Length)
                        break;
                }

                stringBuilder.Append(text[i]);
            }

            return stringBuilder.ToString();
        }
    }
}
