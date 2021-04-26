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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LoginMaHoa
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        BLLcontroller bllc = new BLLcontroller();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string pw = bllc.passwordEncryption(txtPassword.Password);
            txtPassword.Password = "";
            //pw = bllc.passwordDecryption(bllc.getPassword(txtUsername.Text)); mã hóa thôi

            if (bllc.CheckAccount(txtUsername.Text, pw))
            {
                if (bllc.BiTheoDoi(bllc.GetIdFromUsername(txtUsername.Text)))
                {
                    bllc.GhiLog(txtUsername.Text);
                }

                if (bllc.isAdmin(txtUsername.Text, pw))
                {
                    #region Dialog Login Successful (admin)
                    ContentDialog loginSuc = new ContentDialog
                    {
                        Title = "Thông báo!",
                        Content = "Đăng nhập thành công!!\nChào mừng " + bllc.getUsername(txtUsername.Text),
                        PrimaryButtonText = "Ok",
                        CloseButtonText = "Cancel"
                    };

                    ContentDialogResult result = await loginSuc.ShowAsync();

                    // navigate to next page if the user clicked the primary button.
                    /// Otherwise, do nothing.
                    if (result == ContentDialogResult.Primary)
                    {
                        Frame.Navigate(typeof(adminView));
                    }
                    else
                    {
                        // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                        // Do nothing.
                    }
                    #endregion
                }
                else
                {
                    #region Dialog Login Successful (user)
                    ContentDialog loginSuc = new ContentDialog
                    {
                        Title = "Thông báo!",
                        Content = "Đăng nhập thành công!!\nChào mừng " + bllc.getUsername(txtUsername.Text),
                        PrimaryButtonText = "Ok",
                        CloseButtonText = "Cancel"
                    };

                    ContentDialogResult result = await loginSuc.ShowAsync();

                    // navigate to next page if the user clicked the primary button.
                    /// Otherwise, do nothing.
                    if (result == ContentDialogResult.Primary)
                    {
                        Frame.Navigate(typeof(userView), txtUsername.Text);
                    }
                    else
                    {
                        // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                        // Do nothing.
                    }
                    #endregion
                }
            }
            else
            {
                #region Dialog Login Unsuccessful
                ContentDialog loginUnSuc = new ContentDialog
                {
                    Title = "Thông báo!",
                    Content = "Đăng nhập không thành công!!\nBạn đã tạo tài khoản chưa?",
                    PrimaryButtonText = "Nhập lại",
                    CloseButtonText = "Cancel"
                };

                ContentDialogResult result = await loginUnSuc.ShowAsync();
                #endregion
            }
        }

        private void txtPassword_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                btnLogin_Click(sender, e);
            }
            else
            {
                // không làm gì
            }
        }
    }
}
