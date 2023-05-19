using System.Windows;

namespace Wpf.Mvvm
{
    public interface IMessageBoxService
    {
        bool? Show(string text, string caption = "", MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage image = MessageBoxImage.None);

        bool? Dispatch(string text, string caption = "", MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage image = MessageBoxImage.None);
    }
}
