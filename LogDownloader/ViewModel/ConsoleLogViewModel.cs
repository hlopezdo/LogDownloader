using LogDownloader.Resources;
using System;

namespace LogDownloader.ViewModel
{
    public class ConsoleLogViewModel : BaseViewModel
    {
        public ConsoleLogViewModel()
        {
            this.WriteLog(Resource.Initializing);
        }

        public string Data { get; set; }

        public void WriteLog(string message, bool error = false)
        {
            var date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");
            var newParagrhap = error ? $"({date}) ERROR - {message}\n" : $"({date}) {message}\n";

            Data = newParagrhap + Data;
        }
    }
}
