using Microsoft.Extensions.Logging;
using System;
using System.Windows.Forms;

namespace Raindance.Services.Logging
{
    public class TextBoxLogger : ILogger
    {
        private readonly TextBox _textBox;
        private readonly string _categoryName;

        public TextBoxLogger(string categoryName, TextBox textBox)
        {
            _textBox = textBox;
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter
        )
        {
            if (!_textBox.IsDisposed && formatter != null)
            {
                string message =
                    $"{DateTime.Now} [{logLevel}] {_categoryName}: {formatter(state, exception)}";
                _textBox.Invoke(
                    new Action(() =>
                    {
                        _textBox.AppendText(message + Environment.NewLine);
                    })
                );
            }
        }
    }
}
