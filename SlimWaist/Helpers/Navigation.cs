namespace SlimWaist.Helpers
{
    public class Navigation
    {
        public static async Task GoToAsync(string pageName)
        {
            await Shell.Current.GoToAsync(pageName, animate: true);
        }
    }
}

