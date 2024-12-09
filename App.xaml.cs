using Microsoft.Extensions.DependencyInjection;
using MVVMCrud.Data;
using MVVMCrud.Data.Context;
using MVVMCrud.Data.Model;
using MVVMCrud.Services;
using System.Windows;

namespace MVVMCrud
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            var typeDataSource = System.Configuration.ConfigurationManager.AppSettings["TypeDataSource"].ToString();
            var useSqlLite = typeDataSource.Equals("sqlLite", StringComparison.CurrentCultureIgnoreCase);
            if (useSqlLite)
            {
                services.AddSingleton<IContext<User>>(e => new SQLLiteContext<User>(SQLIteDBContext.GetContext()));
            }
            else {                
                services.AddSingleton<IContext<User>>(e => new XMLContext<User>("person.xml"));
            }

            // Register Views
            services.AddSingleton(typeof(IUserService), typeof(UserService));
            services.AddSingleton<MainWindow>();
        }
    }

}
