using CommunityToolkit.Mvvm.ComponentModel;

namespace ShopApp;

//public class BindingUtilObject : INotifyPropertyChanged
//{
//    public event PropertyChangedEventHandler PropertyChanged;

//    public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
//    {
//        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//    }
//}

public partial class GlobalVM : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy = false;

    public bool IsNotBusy => !IsBusy;
}
