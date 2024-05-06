using System;

namespace RainDance.Services.Logging
{
    public class TextBoxLoggerProvider : ILoggerProvider
    {
        private readonly TextBox _textBox;

        public TextBoxLoggerProvider(TextBox textBox)
        {
            _textBox = textBox;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new TextBoxLogger(categoryName, _textBox);
        }

        public void Dispose() { }
    }
}
