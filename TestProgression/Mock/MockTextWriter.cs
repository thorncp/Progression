using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TestProgression
{
    public class MockTextWriter : TextWriter
    {
        private StringBuilder output = new StringBuilder();

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        public string Text
        {
            get { return output.ToString();}
        }

        public override void Write(string text)
        {
            if (Regex.IsMatch(text, @"^\r(?!\n)"))
            {
                text = text.Substring(1);
                output.Remove(0, text.Length);
                output.Insert(0, text);
            }
            else
            {
                output.Append(text);
            }
        }
    }
}

