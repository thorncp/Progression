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

        public override Encoding Encoding
        {
            get { return textWriter.Encoding; }
        }

        // todo: fill this out with the remaining Write* methods or find
        // a better way to accomplish the same thing
        #region Some horrible boilerplate code

        private void Wrap(Action action)
        {
            PreWrite();
            action();
            PostWrite();
        }

        public override void WriteLine(string value)
        {
            Wrap(() => textWriter.WriteLine(value));
        }


        #endregion
    }
}

