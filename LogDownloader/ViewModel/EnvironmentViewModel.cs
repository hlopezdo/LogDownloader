using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LogDownloader.ViewModel
{
    public class EnvironmentViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public bool IsExpanded { get; set; }

        public bool IsChecked
        {
            get { return this.CheckStatus; }
            set { UpdateCheck(value); }
        }

        public bool IsLeaf { get; set; }

        public bool HasChilds
        {
            get { return Children != null; }
        }

        public ObservableCollection<EnvironmentViewModel> Children { get; set; }

        public List<EnvironmentViewModel> GetEnvironmentsChecked()
        {
            List<EnvironmentViewModel> list = new List<EnvironmentViewModel>();

            if (IsLeaf && IsChecked)
            {
                list.Add(this);
            }
            else if (HasChilds)
            {
                foreach (var child in Children)
                {
                    list.AddRange(child.GetEnvironmentsChecked());
                }
            }

            return list;
        }

        private bool CheckStatus { get; set; }

        private void UpdateCheck(bool value)
        {
            this.CheckStatus = value;
            if (this.Children != null)
            {
                foreach (var child in Children)
                {
                    child.IsChecked = value;
                }
            }
        }
    }
}