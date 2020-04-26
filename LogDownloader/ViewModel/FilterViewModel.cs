using LogDownloader.Data;
using LogDownloader.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LogDownloader.ViewModel
{
    public class FilterViewModel : BaseViewModel
    {
        private readonly string _foldersData = @"Data\\Folders.json";

        public FilterViewModel()
        {
            this.InitialDate = DateTime.Now.AddDays(-3);
            this.EndDate = DateTime.Now;
            GenerateNewOutputPath();

            this.Folders = JsonToObject.LoadConfiguration<FolderViewModel>(_foldersData);
        }

        public DateTime InitialDate { get; set; }

        public DateTime EndDate { get; set; }

        public string OutputPath { get; set; }

        public ObservableCollection<FolderViewModel> Folders { get; set; }

        public List<FolderViewModel> GetFoldersChecked()
        {
            var list = Folders.Where(s => s.IsChecked).ToList() ?? new List<FolderViewModel>();
            if(list.Count == 0)
            {
                throw new Exception(Resource.ErrorNotFolders);
            }

            return list;
        }

        public void GenerateNewOutputPath()
        {
            this.OutputPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}
