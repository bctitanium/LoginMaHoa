using BLL;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LoginMaHoa
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class userView : Page
    {
        BLLcontroller bllc = new BLLcontroller();

        public userView()
        {
            this.InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                //lbWelcomeUsername.Text = $"{e.Parameter.ToString()}";
                lbWelcomeUsername.Text = e.Parameter.ToString();
            }
            else
            {
                lbWelcomeUsername.Text = "Hi!";
            }

            base.OnNavigatedTo(e);
        }

        private void loadDG()
        {
            DataGridHuman.Columns.Clear();

            for (int i = 0; i < bllc.LoadDGHuman().Columns.Count; i++)
            {
                DataGridHuman.Columns.Add(new DataGridTextColumn()
                {
                    Header = bllc.LoadDGHuman().Columns[i].ColumnName,
                    Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") }
                });
            }

            var collection = new ObservableCollection<object>();

            foreach (DataRow row in bllc.LoadDGHuman().Rows)
            {
                collection.Add(row.ItemArray);
            }

            DataGridHuman.ItemsSource = collection;
        }

        private async void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (txtOldPassword.Password == bllc.passwordDecryption(bllc.getPassword(lbWelcomeUsername.Text)))
            {
                if (txtPassword.Password == txtPasswordComfirm.Password)
                {
                    if (bllc.DoiMatKhau(lbWelcomeUsername.Text, bllc.passwordEncryption(txtPassword.Password)))
                    {
                        if (bllc.BiTheoDoi(bllc.GetIdFromUsername(lbWelcomeUsername.Text)))
                        {
                            bllc.GhiPasswordChangeLog(lbWelcomeUsername.Text);
                        }

                        #region change pass suc
                        ContentDialog changeSuc = new ContentDialog
                        {
                            Title = "Thông báo!",
                            Content = "Đổi mật khẩu thành công!!\nHãy thử đăng nhập lại",
                            PrimaryButtonText = "Ok",
                            CloseButtonText = "Cancel"
                        };

                        ContentDialogResult result = await changeSuc.ShowAsync();
                        #endregion
                    }
                    else
                    {

                    }
                }
                else
                {
                    #region change pass unsuc differ pass
                    ContentDialog changeUnSuc = new ContentDialog
                    {
                        Title = "Thông báo!",
                        Content = "Đổi mật khẩu không thành công!!\nHãy thử lại với mật khẩu hoặc mật khẩu xác nhận giống nhau",
                        PrimaryButtonText = "Ok",
                        CloseButtonText = "Cancel"
                    };

                    ContentDialogResult result = await changeUnSuc.ShowAsync();
                    #endregion
                }
            }
            else
            {
                #region change pass Unsuc wrong old pass
                ContentDialog changeUnSuc = new ContentDialog
                {
                    Title = "Thông báo!",
                    Content = "Đổi mật khẩu không thành công!!\nMật khẩu cũ không đúng",
                    PrimaryButtonText = "Ok",
                    CloseButtonText = "Cancel"
                };

                ContentDialogResult result = await changeUnSuc.ShowAsync();
                #endregion
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            bllc.AddHuman(txtTen.Text, txtTuoi.Text);

            if (bllc.BiTheoDoi(bllc.GetIdFromUsername(lbWelcomeUsername.Text)))
            {
                bllc.GhiHumanChange(lbWelcomeUsername.Text, 1, 0, "", txtTen.Text, "", "");
            }

            loadDG();
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            int selectedId = 0;
            string selectedName = "";
            string selectedAge = "";
            int i = 0;

            foreach (DataRow row in bllc.LoadDGHuman().Rows)
            {
                if (i == DataGridHuman.SelectedIndex)
                {
                    selectedId = Convert.ToInt32(row.ItemArray[0]);
                    selectedName = row.ItemArray[1].ToString();
                    selectedAge = row.ItemArray[2].ToString();

                    break;
                }

                i++;
            }

            bllc.EditHuman(selectedId, txtTen.Text, txtTuoi.Text);

            if (bllc.BiTheoDoi(bllc.GetIdFromUsername(lbWelcomeUsername.Text)))
            {
                bllc.GhiHumanChange(lbWelcomeUsername.Text, 2, selectedId, selectedName, txtTen.Text, selectedAge, txtTuoi.Text);
            }
            
            loadDG();
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            int selectedId = 0;
            string selectedName = "";
            string selectedAge = "";
            int i = 0;

            foreach (DataRow row in bllc.LoadDGHuman().Rows)
            {
                if (i == DataGridHuman.SelectedIndex)
                {
                    selectedId = Convert.ToInt32(row.ItemArray[0]);
                    selectedName = row.ItemArray[1].ToString();
                    selectedAge = row.ItemArray[2].ToString();

                    break;
                }

                i++;
            }

            bllc.RemoveHuman(selectedId);

            if (bllc.BiTheoDoi(bllc.GetIdFromUsername(lbWelcomeUsername.Text)))
            {
                bllc.GhiHumanChange(lbWelcomeUsername.Text, 3, selectedId, selectedName, "", selectedAge, "");
            }
            
            loadDG();
        }

        private void Grid_Loading(FrameworkElement sender, object args)
        {
            DataGridHuman.Columns.Clear();

            for (int i = 0; i < bllc.LoadDGHuman().Columns.Count; i++)
            {
                DataGridHuman.Columns.Add(new DataGridTextColumn()
                {
                    Header = bllc.LoadDGHuman().Columns[i].ColumnName,
                    Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") }
                });
            }

            var collection = new ObservableCollection<object>();

            foreach (DataRow row in bllc.LoadDGHuman().Rows)
            {
                collection.Add(row.ItemArray);
            }

            DataGridHuman.ItemsSource = collection;
        }

        private void DataGridHuman_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = 0;

            foreach (DataRow row in bllc.LoadDGHuman().Rows)
            {
                if (i == DataGridHuman.SelectedIndex)
                {
                    txtTen.Text = row.ItemArray[1].ToString();
                    txtTuoi.Text = row.ItemArray[2].ToString();

                    break;
                }

                i++;
            }
        }
    }
}
