using Microsoft.Extensions.Logging;
using Raindance.Services.Logging;
using System;
using System.Windows.Forms;

namespace RainDance.Services.Logging
{
    public class TextBoxLoggerProvider : ILoggerProvider
    {
        private readonly RichTextBox _textBox;

        public TextBoxLoggerProvider(RichTextBox textBox)
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
