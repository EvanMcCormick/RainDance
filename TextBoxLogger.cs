using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Raindance.Services.Logging
{
    public class TextBoxLogger : ILogger
    {
        private readonly RichTextBox _richTextBox;
        private readonly string _categoryName;

        public TextBoxLogger(string categoryName, RichTextBox textBox)
        {
            _richTextBox = textBox;
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!_richTextBox.IsDisposed && formatter != null)
            {
                string message = $"{DateTime.Now} [{logLevel}] {_categoryName}: {formatter(state, exception)}";
                _richTextBox.Invoke(new Action(() =>
                {
                    _richTextBox.SelectionStart = _richTextBox.TextLength;
                    _richTextBox.SelectionLength = 0;

                    _richTextBox.SelectionColor = GetColorForLogLevel(logLevel);
                    _richTextBox.AppendText(message + Environment.NewLine);
                    _richTextBox.SelectionColor = _richTextBox.ForeColor;
                }));
            }
        }

        private Color GetColorForLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Critical: return Color.Red;
                case LogLevel.Error: return Color.DarkRed;
                case LogLevel.Warning: return Color.Orange;
                case LogLevel.Information: return Color.Green;
                case LogLevel.Debug: return Color.Gray;
                case LogLevel.Trace: return Color.LightGray;
                default: return Color.Black;
            }
        }
    }
}
