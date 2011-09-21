using System;
using System.IO;
using System.Text;

namespace Progression
{
    public class ConsoleWrapper : TextWriter
    {
        private readonly TextWriter textWriter;

        public ConsoleWrapper(TextWriter textWriter)
        {
            this.textWriter = textWriter;
        }

        public event Action PreWrite = delegate { };

        public event Action PostWrite = delegate { };

        public void BypassWrite(string text)
        {
            textWriter.Write(text);
        }

        public void BypassWriteLine()
        {
            textWriter.WriteLine();
        }

        public override Encoding Encoding
        {
            get { return textWriter.Encoding; }
        }

        #region Some horrible boilerplate code

        private void Wrap(Action action)
        {
            PreWrite();
            action();
            PostWrite();
        }

        public override void Write(char value)
        {
            Wrap(() => textWriter.Write(value));
        }

        public override void Write(char[] buffer)
        {
            Wrap(() => textWriter.Write(buffer));
        }

        public override void Write(char[] buffer, int index, int count)
        {
            Wrap(() => textWriter.Write(buffer, index, count));
        }

        public override void Write(bool value)
        {
            Wrap(() => textWriter.Write(value));
        }

        public override void Write(int value)
        {
            Wrap(() => textWriter.Write(value));
        }

        public override void Write(uint value)
        {
            Wrap(() => textWriter.Write(value));
        }

        public override void Write(long value)
        {
            Wrap(() => textWriter.Write(value));
        }

        public override void Write(ulong value)
        {
            Wrap(() => textWriter.Write(value));
        }

        public override void Write(float value)
        {
            Wrap(() => textWriter.Write(value));
        }

        public override void Write(double value)
        {
            Wrap(() => textWriter.Write(value));
        }

        public override void Write(decimal value)
        {
            Wrap(() => textWriter.Write(value));
        }

        public override void Write(string value)
        {
            Wrap(() => textWriter.Write(value));
        }

        public override void Write(object value)
        {
            Wrap(() => textWriter.Write(value));
        }

        public override void Write(string format, object arg0)
        {
            Wrap(() => textWriter.Write(format, arg0));
        }

        public override void Write(string format, object arg0, object arg1)
        {
            Wrap(() => textWriter.Write(format, arg0, arg1));
        }

        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            Wrap(() => textWriter.Write(format, arg0, arg1, arg2));
        }

        public override void Write(string format, params object[] arg)
        {
            Wrap(() => textWriter.Write(format, arg));
        }

        public override void WriteLine()
        {
            Wrap(() => textWriter.WriteLine());
        }

        public override void WriteLine(char value)
        {
            Wrap(() => textWriter.WriteLine(value));
        }

        public override void WriteLine(char[] buffer)
        {
            Wrap(() => textWriter.WriteLine(buffer));
        }

        public override void WriteLine(char[] buffer, int index, int count)
        {
            Wrap(() => textWriter.WriteLine(buffer, index, count));
        }

        public override void WriteLine(bool value)
        {
            Wrap(() => textWriter.WriteLine(value));
        }

        public override void WriteLine(int value)
        {
            Wrap(() => textWriter.WriteLine(value));
        }

        public override void WriteLine(uint value)
        {
            Wrap(() => textWriter.WriteLine(value));
        }

        public override void WriteLine(long value)
        {
            Wrap(() => textWriter.WriteLine(value));
        }

        public override void WriteLine(ulong value)
        {
            Wrap(() => textWriter.WriteLine(value));
        }

        public override void WriteLine(float value)
        {
            Wrap(() => textWriter.WriteLine(value));
        }

        public override void WriteLine(double value)
        {
            Wrap(() => textWriter.WriteLine(value));
        }

        public override void WriteLine(decimal value)
        {
            Wrap(() => textWriter.WriteLine(value));
        }

        public override void WriteLine(string value)
        {
            Wrap(() => textWriter.WriteLine(value));
        }

        public override void WriteLine(object value)
        {
            Wrap(() => textWriter.WriteLine(value));
        }

        public override void WriteLine(string format, object arg0)
        {
            Wrap(() => textWriter.WriteLine(format, arg0));
        }

        public override void WriteLine(string format, object arg0, object arg1)
        {
            Wrap(() => textWriter.WriteLine(format, arg0, arg1));
        }

        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            Wrap(() => textWriter.WriteLine(format, arg0, arg1, arg2));
        }

        public override void WriteLine(string format, params object[] arg)
        {
            Wrap(() => textWriter.WriteLine(format, arg));
        }

        #endregion
    }
}

