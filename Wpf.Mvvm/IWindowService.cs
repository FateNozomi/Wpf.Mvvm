using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf.Mvvm
{
    public interface IWindowService
    {
        void Show<T>(IWindow dataContext)
            where T : Window, new();

        bool? ShowDialog<T>(IWindow dataContext)
            where T : Window, new();

        Task<bool?> ShowAsync<T>(IWindow dataContext, CancellationToken token)
            where T : Window, new();

        Task<bool?> ShowAsync<T>(IWindow dataContext, bool setAcitiveWindowAsOwner, CancellationToken token)
            where T : Window, new();

        Task<bool?> ShowAsync<T>(IWindow dataContext, IWindow parentContext, bool setActiveWindowAsOwner, CancellationToken token)
            where T : Window, new();

    }
}
