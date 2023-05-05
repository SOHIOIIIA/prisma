using System.ComponentModel;

namespace Prisma.Core.Abstractions
{
    public interface INavigation
    {
        bool CanNext { get; set; }
        bool CanPrevious { get; set; }
        void NextPage(string next_page);
        void PreviousPage(string prev_page);
    }
}