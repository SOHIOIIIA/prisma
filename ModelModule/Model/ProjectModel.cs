using Prism.Mvvm;
using Prisma.Core.Abstractions;

namespace ModelModule.Model
{
    public class ProjectModel: BindableBase, IProjectPage
    {
        private string _dataBasePath = "";
        private string _pyScriptPath = "";
        private BindableBase _currentPage;
        public BindableBase CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        public string DataBasePath
        {
            get => _dataBasePath;
            set => SetProperty(ref _dataBasePath, value);
        }
        public string PyScriptPath
        {
            get => _pyScriptPath;
            set => SetProperty(ref _pyScriptPath, value);
        }

        public ProjectModel()
        {
        }
    }
}