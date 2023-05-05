using Prisma.Core.Abstractions;
using Prism.Mvvm;

namespace ModelModule.Components
{
    public class ProjectNavigation: BindableBase, INavigation
    {
        private bool _canNext;
        private bool _canPrevious;

        public bool CanNext
        {
            get => _canNext;
            set => SetProperty(ref _canNext, value);
        }
        public bool CanPrevious
        {
            get => _canPrevious;
            set => SetProperty(ref _canPrevious, value);
        }

        public void NextPage(string page)
        {
            
        }

        public void PreviousPage(string page)
        {
            
        }

    }
}