using MVVMCrud.Data;
using MVVMCrud.Data.Model;
using MVVMCrud.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace MVVMCrud.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IContext<User> _context;
        IUserService _userService;

        public MainViewModel(IUserService userService)
        {
            _userService = userService;
            var list = _userService.GetAll();
            Users = new ObservableCollection<User>(list);
;
            OnPropertyChanged(nameof(Users));
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        RelayCommand? editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new RelayCommand(obj =>
                    {
                        User? user = obj as User;
                        if (user == null) {
                            MessageBox.Show("Данные для добавления не созданы. Нажмите +");
                            return;
                        }

                        if (String.IsNullOrEmpty(user.Login)) {
                            MessageBox.Show("Требуется не пустой логин");
                            return;
                        }                                                
                        
                        if (user.Id == Guid.Empty)
                        {
                            var checkUser = Users.Where(x => user.Login.Equals(x.Login, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                            if (checkUser != null)
                            {
                                MessageBox.Show("Пользователь с таким логином уже существует!");
                                return;
                            }

                            try
                            {
                                Users.Insert(0, user);
                                _userService.Add(user);
                                MessageBox.Show("Пользователь успешно добавлен!");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка" + ex.Message);
                            }
                        }
                        else
                        {
                            try
                            {
                                _userService.Update(user);
                                MessageBox.Show("Пользователь успешно изменен!");
                            }
                            catch (Exception ex) {
                                MessageBox.Show("Ошибка"+ex.Message);                                
                            }
                        }
                        SelectedUser = user;
                        OnPropertyChanged("SelectedUser");
                        OnPropertyChanged(nameof(Users));                                                
                    }));
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      User user = new User { Login ="" };                      
                      SelectedUser = user;
                      OnPropertyChanged("SelectedUser");
                  }));
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(obj =>
                  {
                      if (MessageBox.Show("Вы действительно хотите удалить выбранную запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                      {
                          User? user = obj as User;
                          if (user != null)
                          {
                              try
                              {
                                  Users.Remove(user);
                                  _userService.Delete(user);
                                  MessageBox.Show("Пользователь успешно удален!");
                              }
                              catch (Exception ex)
                              {
                                  MessageBox.Show("Ошибка" + ex.Message);
                              }
                          }
                          OnPropertyChanged("Users");
                      }
                  }));
            }
        }
    }
}
