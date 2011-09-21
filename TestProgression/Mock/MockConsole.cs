using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TestProgression.Mock
{
    public class MockConsole : TextWriter
    {
        private readonly StringBuilder output = new StringBuilder();
        private readonly Regex carriageReturnPattern = new Regex(@"^\r(?!\n)");

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

        public override void WriteLine()
        {
            output.Append(Environment.NewLine);
        }

        public bool Flushed { get; set; }

        public override void Flush()
        {
            base.Flush();
            Flushed = true;
        }
    }
}

