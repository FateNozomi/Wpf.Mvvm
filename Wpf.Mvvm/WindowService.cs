using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf.Mvvm
{
    public class WindowService : IWindowService
    {
        public void Show<T>(IWindow dataContext)
            where T : Window, new()
        {
            T window = new T();
            WindowConductor conductor = new WindowConductor(dataContext, window);
            conductor.Initialize();
            window.Show();
        }

        public bool? ShowDialog<T>(IWindow dataContext)
            where T : Window, new()
        {
            T window = new T();
            WindowConductor conductor = new WindowConductor(dataContext, window);
            conductor.Initialize();
            return window.ShowDialog();
        }

        public Task<bool?> ShowAsync<T>(IWindow dataContext, CancellationToken token)
            where T : Window, new()
        {
            return ShowAsync<T>(dataContext, null, false, token);
        }

        public Task<bool?> ShowAsync<T>(IWindow dataContext, bool setActiveWindowAsOwner, CancellationToken token)
            where T : Window, new()
        {
            return ShowAsync<T>(dataContext, null, setActiveWindowAsOwner, token);
        }

        public async Task<bool?> ShowAsync<T>(IWindow dataContext, IWindow? parentContext, bool setActiveWindowAsOwner, CancellationToken token)
            where T : Window, new()
        {
            T window = new T();
            WindowConductor conductor = new WindowConductor(dataContext, window);
            conductor.Initialize();

            if (setActiveWindowAsOwner)
            {
                var activeWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);
                window.Owner = activeWindow;
            }

            SemaphoreSlim semaphore = new SemaphoreSlim(0, 1);
            window.Closed += (s, e) => semaphore.Release();

            try
            {
                if (parentContext != null)
                    parentContext.IsEnabled = false;

                window.Show();
                await semaphore.WaitAsync(token);
                semaphore.Dispose();
                return dataContext.WindowResult;
            }
            finally
            {
                if (parentContext != null)
                    parentContext.IsEnabled = true;
            }
        }
    }
}
