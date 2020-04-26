using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace LogDownloader.Data
{
    public static class JsonToObject
    {
        public static ObservableCollection<T> LoadConfiguration<T>(string configFileName)
        {
            var defaultConfiguration = ReadDeafultConfiguration(configFileName);
            return ParseToEnviorementTreeItem<T>(defaultConfiguration);
        }

        private static string ReadDeafultConfiguration(string configFileName)
        {
            StreamReader reader = new StreamReader(configFileName);
            var file = reader.ReadToEnd();
            reader.Close();

            return file;
        }

        private static ObservableCollection<T> ParseToEnviorementTreeItem<T>(string defaultConfiguration)
        {
            return JsonConvert.DeserializeObject<ObservableCollection<T>>(defaultConfiguration);
        }
    }
}
