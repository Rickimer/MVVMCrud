using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVMCrud.Data.Model
{
    public class User : Entity, INotifyPropertyChanged
    {
        private string _login;
        private string? _firstName;
        private string? _lastName;
        
        public required string Login
        {
            get { return _login; }
            set { 
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string? FirstName
        {
            get { return _firstName; }
            set { 
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string? LastName
        {
            get { return _lastName; }
            set { 
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
