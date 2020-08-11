using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Spectre.Console.Internal
{
    internal sealed class StringBuffer : IDisposable
    {
        [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "False positive")]
        private readonly StringReader _reader;
        private readonly int _length;

        public int Position { get; private set; }
        public bool Eof => Position >= _length;

        public StringBuffer(string text)
        {
            text ??= string.Empty;

            _reader = new StringReader(text);
            _length = text.Length;

            Position = 0;
        }

        public void Dispose()
        {
            _reader.Dispose();
        }

        public char Peek()
        {
            if (Eof)
            {
                throw new InvalidOperationException("Tried to peek past the end of the text.");
            }

            return (char)_reader.Peek();
        }

        public char Read()
        {
            if (Eof)
            {
                throw new InvalidOperationException("Tried to read past the end of the text.");
            }

            Position++;
            return (char)_reader.Read();
        }
    }
}
