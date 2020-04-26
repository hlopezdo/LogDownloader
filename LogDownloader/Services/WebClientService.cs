using LogDownloader.Resources;
using LogDownloader.ViewModel;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace LogDownloader.Services
{
    public class WebClientService
    {
        private const string _patternDate = @"\d{2}/\d{2}/\d{4}";
        private const string _patternTime = @"\d{2}:\d{2}";
        private const string _patternSize = @"\d*";
        private const string _patternSpace = @"\s*";
        private const string _patternClousure = @".*</a>";
        private const string _patternName = @">.*<";

        private static List<EnvironmentViewModel> ListOfEnvironment;
        private static List<FolderViewModel> ListOfFolder;
        private static FilterViewModel Filter;
        private static ConsoleLogViewModel ConsoleLog;
        private static ProgressViewModel Progress;

        private static EnumState State;

        public static Action DowndLoadFiles(
            List<EnvironmentViewModel> listOfEnvironment,
            List<FolderViewModel> listOfFolder,
            FilterViewModel filter,
            ConsoleLogViewModel consoleLog,
            ProgressViewModel progress)
        {
            ListOfEnvironment = listOfEnvironment;
            ListOfFolder = listOfFolder;
            Filter = filter;
            ConsoleLog = consoleLog;
            Progress = progress;

            List<dynamic> listOfFiles = ObtainListOfFiles();
            return () => GetFilesFromList(listOfFiles);
        }

        public static EnumState GetState()
        {
            return State;
        }

        public static void Stop()
        {
            State = EnumState.Stop;
        }

        private static List<dynamic> ObtainListOfFiles()
        {
            ConsoleLog.WriteLog(Resource.ScanningFolders);

            Initialize();
            List<dynamic> listOfFiles = new List<dynamic>();
            foreach (var environment in ListOfEnvironment)
            {
                foreach (var folder in ListOfFolder)
                {
                    var url = GetRemotePath(environment, folder);
                    var filesInRemotePath = GetFileList(url, environment, folder);
                    listOfFiles.AddRange(filesInRemotePath);
                }
            }

            return listOfFiles;
        }

        private static List<ExpandoObject> GetFileList(string url, EnvironmentViewModel environment, FolderViewModel folder)
        {
            try
            {
                var html = GetDocumentFromUrl(url);
                var list = GetListOfNameAndDates(html);

                var returnList = new List<ExpandoObject>();
                var listOfNames = list.Where(l => l.Value >= Filter.InitialDate && l.Value <= Filter.EndDate).ToList();

                foreach (var name in listOfNames)
                {
                    dynamic newExpando = new ExpandoObject();
                    newExpando.Name = name.Key;
                    newExpando.Url = url;
                    newExpando.Environment = environment;
                    newExpando.Folder = folder;

                    returnList.Add(newExpando);
                }

                return returnList;
            }
            catch (Exception ex)
            {
                ConsoleLog.WriteLog(url + " " + ex.Message, false);
                return new List<ExpandoObject>();
            }
        }

        private static void GetFilesFromList(List<dynamic> listOfFiles)
        {
            Run();

            var amountOfFiles = listOfFiles.Count;
            Progress.SetMaximum(amountOfFiles);
            ConsoleLog.WriteLog($"{amountOfFiles} files selected to downloading");

            for (int x = 0; (x < listOfFiles.Count && IsRunnig()); x++)
            {
                var file = listOfFiles[x];
                var localPath = GetLocalPath(file.Name, file.Environment, file.Folder);
                DownloadFile(file.Url, file.Name, localPath);
            }

            Finish();
        }

        private static string GetDocumentFromUrl(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string html = reader.ReadToEnd();
            html = html.Replace("<br>", "\n").ToLower();

            response.Close();
            reader.Close();

            return html;
        }

        private static Dictionary<String, DateTime> GetListOfNameAndDates(string html)
        {
            Dictionary<String, DateTime> list = new Dictionary<String, DateTime>();
            var _patternHtmlRow = $"{_patternDate}{_patternSpace}{_patternTime}{_patternSpace}{_patternSize}{_patternSpace}{_patternClousure}";

            Regex regex = new Regex(_patternHtmlRow);
            MatchCollection matches = regex.Matches(html);

            foreach (Match match in matches)
            {
                var name = GetNameFromRow(match.Value);
                var date = GetDateFromRow(match.Value);

                list.Add(name, date);
            }

            return list;
        }

        private static string GetNameFromRow(string row)
        {
            Regex regex = new Regex(_patternName);
            MatchCollection matches = regex.Matches(row);
            if (matches.Count > 0)
            {
                return matches[0].Value.Replace("<", "").Replace(">", "");
            }
            else
            {
                throw new Exception(Resource.ErrorParsingName);
            }
        }

        private static DateTime GetDateFromRow(string row)
        {
            Regex regex = new Regex(_patternDate);
            MatchCollection matches = regex.Matches(row);
            if (matches.Count > 0)
            {
                DateTime date = new DateTime();
                DateTime.TryParse(matches[0].Value, out date);

                return date;
            }
            else
            {
                return new DateTime();
            }
        }

        private static string GetRemotePath(EnvironmentViewModel environments, FolderViewModel folder)
        {
            return $"{environments.Url}/{folder.Path}";
        }

        private static string GetLocalPath(string fileName, EnvironmentViewModel environments, FolderViewModel folder)
        {
            var folderPlusFileName = $"({folder.Name}) - {fileName}";
            return Path.Combine(Filter.OutputPath, environments.Name, folderPlusFileName);
        }

        private static void DownloadFile(string url, string fileName, string localPath)
        {
            try
            {
                using (WebClient web = new WebClient())
                {
                    var finalPath = $"{url}/{fileName}";
                    ConsoleLog.WriteLog($"Dowloading {finalPath} into local folder {localPath}");

                    web.DownloadFile(finalPath, localPath);
                    Progress.Increment();
                }
            }
            catch
            {
                ConsoleLog.WriteLog($"An error has ocurred while trying to download the file {fileName}", true);
            }
        }

        private static void Initialize()
        {
            State = EnumState.Initialized;
        }

        private static void Run()
        {
            State = EnumState.Running;
        }

        private static void Finish()
        {
            if (State != EnumState.Stop)
            {
                State = EnumState.Finished;
            }
        }

        private static bool IsRunnig()
        {
            return (State == EnumState.Running);
        }
    }
}
