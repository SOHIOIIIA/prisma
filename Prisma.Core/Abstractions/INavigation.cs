using System.ComponentModel;

namespace Prisma.Core.Abstractions
{
    public interface INavigation
    {
        bool CanNext { get; set; }
        bool CanPrevious { get; set; }
        string CurrentPage { get; set; }
        void NextPage();
        void PreviousPage();
    }
}