using LogDownloader.Data;
using LogDownloader.Resources;
using LogDownloader.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LogDownloader.ViewModel
{
    public class DownloaderViewModel : BaseViewModel
    {
        private readonly string _environmentsData = @"Data\\Environments.json";

        private Task downladTask;

        public DownloaderViewModel()
        {
            try
            {
                this.PlayCommand = new RelayCommand(Start);
                this.StopCommand = new RelayCommand(Stop);

                this.Progress = new ProgressViewModel();
                this.ConsoleLog = new ConsoleLogViewModel();
                this.Filter = new FilterViewModel();
                this.Environments = JsonToObject.LoadConfiguration<EnvironmentViewModel>(_environmentsData);

                this.ConsoleLog.WriteLog(Resource.ProfileLoaded);
            }
            catch (Exception ex)
            {
                this.ConsoleLog.WriteLog(ex.Message, true);
            }
        }

        public ObservableCollection<EnvironmentViewModel> Environments { get; set; }

        public FilterViewModel Filter { get; set; }

        public ConsoleLogViewModel ConsoleLog { get; set; }

        public ProgressViewModel Progress { get; set; }

        public ICommand PlayCommand { get; set; }

        public ICommand StopCommand { get; set; }

        public void Start()
        {
            try
            {
                Progress = new ProgressViewModel();

                List<EnvironmentViewModel> listOfEnviorements = GetEnvironmentsChecked();
                List<FolderViewModel> listOfFolders = Filter.GetFoldersChecked();

                GenerateOutPutFolder(listOfEnviorements);
                DownloadFiles(listOfEnviorements, listOfFolders);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Action error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.ConsoleLog.WriteLog(ex.Message, true);
            }
        }

        public void Stop()
        {
            WebClientService.Stop();
        }

        private void Reset()
        {
            ConsoleLog.Data = string.Empty;
            Progress.Value = 0;
            Filter.GenerateNewOutputPath();
        }

        private List<EnvironmentViewModel> GetEnvironmentsChecked()
        {
            List<EnvironmentViewModel> list = new List<EnvironmentViewModel>();
            foreach (var item in Environments)
            {
                list.AddRange(item.GetEnvironmentsChecked());
            }

            if (list.Count == 0)
            {
                throw new Exception(Resource.ErrorEnvironment);
            }

            return list;
        }

        private void GenerateOutPutFolder(List<EnvironmentViewModel> listOfEnvironments)
        {
            this.ConsoleLog.WriteLog(Resource.GenerateFolders);
            foreach (var environments in listOfEnvironments)
            {
                var environmentsPath = Path.Combine(Filter.OutputPath, environments.Name);
                GenerateDirectory(environmentsPath);
            }
        }

        private static void GenerateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void DownloadFiles(List<EnvironmentViewModel> listOfEnvironments, List<FolderViewModel> listOfFolders)
        {
            this.ConsoleLog.WriteLog(Resource.DownloadStarted);
            downladTask = Task.Run(WebClientService.DowndLoadFiles(listOfEnvironments, listOfFolders, Filter, ConsoleLog, Progress));

            downladTask.ContinueWith(GenerateNotification());
        }

        private Action<Task> GenerateNotification()
        {
            return (x) =>
            {
                var state = WebClientService.GetState();
                switch (state)
                {
                    case EnumState.Finished:
                        MessageBox.Show("All files are downloaded", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case EnumState.Stop:
                        MessageBox.Show("The process has been stopped", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                        Reset();
                        break;
                }
            };
        }
    }
}
