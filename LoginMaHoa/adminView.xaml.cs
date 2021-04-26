using System;
using System.Collections.Generic;
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
using BLL;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Collections.ObjectModel;
using System.Data;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LoginMaHoa
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class adminView : Page
    {
        BLLcontroller bllc = new BLLcontroller();

        public adminView()
        {
            this.InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            if (bllc.CheckUsername(txtUsername.Text))
            {
                #region Dialog Create Account UnSuc
                ContentDialog createUnSuc = new ContentDialog
                {
                    Title = "Thông báo!",
                    Content = "Tên tài khoản đã tồn tại\nDui lòng nhập tên tài khoản khác",
                    PrimaryButtonText = "Ok",
                    CloseButtonText = "Cancel"
                };

                ContentDialogResult result = await createUnSuc.ShowAsync();
                #endregion
            }
            else
            {
                if (txtPassword.Password != txtPasswordComfirm.Password)
                {
                    #region Dialog Create Account DupPass
                    ContentDialog createDupPass = new ContentDialog
                    {
                        Title = "Thông báo!",
                        Content = "Mật khẩu và mật khẩu xác nhận phải trùng nhau",
                        PrimaryButtonText = "Ok",
                        CloseButtonText = "Cancel"
                    };

                    ContentDialogResult result = await createDupPass.ShowAsync();
                    #endregion
                }
                else
                {
                    string passEncrypted = bllc.passwordEncryption(txtPassword.Password);

                    if (bllc.CreateAccount(txtUsername.Text, passEncrypted))
                    {
                        #region Dialog Create Account Suc
                        ContentDialog createSuc = new ContentDialog
                        {
                            Title = "Thông báo!",
                            Content = "Tạo tài khoản thành công",
                            PrimaryButtonText = "Ok",
                            CloseButtonText = "Cancel"
                        };

                        ContentDialogResult result = await createSuc.ShowAsync();
                        #endregion
                    }
                    else
                    {
                        //không làm gì
                    }
                }
            }
        }

        private void frmAdmin_Loading(FrameworkElement sender, object args)
        {
            LoadDGLog();
            LoadDGAcc();
            LoadTD();

            //if (bllc.isAuditing())
            //{
            //    toggleSwitchAuditing.IsOn = true;
            //}
            //else
            //{
            //    toggleSwitchAuditing.IsOn = false;
            //}
        }

        private void LoadDGLog()
        {
            dgLog.Columns.Clear();

            for (int i = 0; i < bllc.LoadDGLog().Columns.Count; i++)
            {
                dgLog.Columns.Add(new DataGridTextColumn()
                {
                    Header = bllc.LoadDGLog().Columns[i].ColumnName,
                    Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") }
                });
            }

            var collection = new ObservableCollection<object>();

            foreach (DataRow row in bllc.LoadDGLog().Rows)
            {
                collection.Add(row.ItemArray);
            }

            dgLog.ItemsSource = collection;
        }

        private void LoadDGAcc()
        {
            dgAcc.Columns.Clear();

            for (int i = 0; i < bllc.LoadDGAcc().Columns.Count; i++)
            {
                dgAcc.Columns.Add(new DataGridTextColumn()
                {
                    Header = bllc.LoadDGAcc().Columns[i].ColumnName,
                    Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") }
                });
            }

            var collection = new ObservableCollection<object>();

            foreach (DataRow row in bllc.LoadDGAcc().Rows)
            {
                collection.Add(row.ItemArray);
            }

            dgAcc.ItemsSource = collection;
        }

        private void LoadTD()
        {
            dgTheoDoi.Columns.Clear();

            for (int i = 0; i < bllc.LoadDGTheoDoi().Columns.Count; i++)
            {
                dgTheoDoi.Columns.Add(new DataGridTextColumn()
                {
                    Header = bllc.LoadDGTheoDoi().Columns[i].ColumnName,
                    Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") }
                });
            }

            var collection = new ObservableCollection<object>();

            foreach (DataRow row in bllc.LoadDGTheoDoi().Rows)
            {
                collection.Add(row.ItemArray);
            }

            dgTheoDoi.ItemsSource = collection;
        }

        private void btnViewLog_Click(object sender, RoutedEventArgs e)
        {
            dgLog.Columns.Clear();

            for (int i = 0; i < bllc.LoadDGLog().Columns.Count; i++)
            {
                dgLog.Columns.Add(new DataGridTextColumn()
                {
                    Header = bllc.LoadDGLog().Columns[i].ColumnName,
                    Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") }
                });
            }

            var collection = new ObservableCollection<object>();

            foreach (DataRow row in bllc.LoadDGLog().Rows)
            {
                collection.Add(row.ItemArray);
            }

            dgLog.ItemsSource = collection;
        }

        private void btnViewPCL_Click(object sender, RoutedEventArgs e)
        {
            dgLog.Columns.Clear();

            for (int i = 0; i < bllc.LoadDGPasswordChangeLog().Columns.Count; i++)
            {
                dgLog.Columns.Add(new DataGridTextColumn()
                {
                    Header = bllc.LoadDGPasswordChangeLog().Columns[i].ColumnName,
                    Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") }
                });
            }

            var collection = new ObservableCollection<object>();

            foreach (DataRow row in bllc.LoadDGPasswordChangeLog().Rows)
            {
                collection.Add(row.ItemArray);
            }

            dgLog.ItemsSource = collection;
        }

        private void btnViewChange_Click(object sender, RoutedEventArgs e)
        {
            dgLog.Columns.Clear();

            for (int i = 0; i < bllc.LoadDGHumanChange().Columns.Count; i++)
            {
                dgLog.Columns.Add(new DataGridTextColumn()
                {
                    Header = bllc.LoadDGHumanChange().Columns[i].ColumnName,
                    Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") }
                });
            }

            var collection = new ObservableCollection<object>();

            foreach (DataRow row in bllc.LoadDGHumanChange().Rows)
            {
                collection.Add(row.ItemArray);
            }

            dgLog.ItemsSource = collection;
        }

        private void dgAcc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = 0;

            foreach (DataRow row in bllc.LoadDGAcc().Rows)
            {
                if (i == dgAcc.SelectedIndex)
                {
                    bllc.GhiTheoDoi(Convert.ToInt32(row.ItemArray[0]));
                    LoadTD();

                    break;
                }

                i++;
            }
        }

        private void btnXoaTheoDoi_Click(object sender, RoutedEventArgs e)
        {
            bllc.XoaTheoDoi();
            LoadTD();
        }

        private void btnTheoDoiTatCa_Click(object sender, RoutedEventArgs e)
        {
            bllc.GhiTheoDoiTatCa();
            LoadTD();
        }
    }
}
