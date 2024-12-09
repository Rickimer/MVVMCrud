using MVVMCrud.Data;
using MVVMCrud.Data.Model;
using MVVMCrud.Services;
using MVVMCrud.ViewModel;
using System.Windows;

namespace MVVMCrud
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IContext<User> _context;
        private readonly IUserService _userService;

        public MainWindow(IUserService userService)            
        {
            InitializeComponent();
            _userService = userService;
            Loaded += MainWindow_Loaded;            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var mainViewModel = new MainViewModel(_userService);
            DataContext = mainViewModel;
        }
    }
}