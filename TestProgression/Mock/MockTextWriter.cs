using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TestProgression
{
    public class MockTextWriter : TextWriter
    {
        private StringBuilder output = new StringBuilder();
        private Regex carriageReturnPattern = new Regex(@"^\r(?!\n)");

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        public string Text
        {
            get { return output.ToString(); }
        }

        public override void Write(string text)
        {
            if (carriageReturnPattern.IsMatch(text))
            {
                text = text.Substring(1);
                int lastLineIndex = Text.LastIndexOf(Environment.NewLine);
                int startIndex = lastLineIndex < 0 ? 0 : lastLineIndex + Environment.NewLine.Length;
                output.Remove(startIndex, Math.Min(text.Length, output.Length - startIndex));
                output.Insert(startIndex, text);
            }
            else
            {
                output.Append(text);
            }
        }

        public override void WriteLine(string value)
        {
            output.Append(value + Environment.NewLine);
        }
    }
}

