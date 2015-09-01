using GalaSoft.MvvmLight.Messaging;
using HelloAutocadPlugIn.Messages;

namespace HelloAutocadPlugIn.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
            // Регистрируем сообщение для закрытия формы.
            Messenger.Default.Register<CloseMainViewMessage>(this, _ => Close());
        }
    }
}
