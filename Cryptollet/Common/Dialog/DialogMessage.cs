using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cryptollet.Common.Dialog
{
    public interface IDialogMessage
    {
        Task DisplayAlert(string title, string message, string cancel);
        Task<string> DisplayPrompt(string title, string message);
        Task<string> DisplayActionSheet(string title, string destruction, params string[] buttons);
    }

    public class DialogMessage : IDialogMessage
    {
        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Shell.Current.DisplayAlert(title, message, cancel);
        }

        public Task<string> DisplayPrompt(string title, string message)
        {
            return Shell.Current.DisplayPromptAsync(title, message);
        }

        public Task<string> DisplayActionSheet(string title, string destruction, params string[] buttons)
        {
            return Shell.Current.DisplayActionSheet(title, "Cancel", destruction, buttons);
        }
    }
}
