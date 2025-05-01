using SlimWaist.Languages;
using System.ComponentModel;
using System.Globalization;

namespace SlimWaist.Extentions
{
    public class Translator : INotifyPropertyChanged
    {
        public string this[string Key]
        {
            get => AppResource.ResourceManager.GetString(Key, CultureInfo);
        }

        public CultureInfo CultureInfo { get; set; } = new CultureInfo("ar-SA");

        public static Translator instance { get; set; } = new Translator();

        public event PropertyChangedEventHandler? PropertyChanged;

        //notify ui without restart 
        //need to be called after change cutltureinfo of the instance
        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
