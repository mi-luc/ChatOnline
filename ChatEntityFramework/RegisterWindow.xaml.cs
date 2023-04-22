using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatEntityFramework
{
    
    public partial class RegisterWindow : Window
    {
        Chat_OnlineEntities context = new Chat_OnlineEntities();
       // LocalUser localuser = new LocalUser("");
        public RegisterWindow()
        {
            InitializeComponent();
        }

        public void HostWindowInFrame(Frame fraContainer, Window win)
        {
            object tmp = win.Content;
            win.Content = null;
            //Page myPage = null;
            fraContainer.Content = new ContentControl() { Content = tmp };
            // m_frame.Navigate(typeof(Page), null, new SlideNavigationTransitionInfo() { Effect = SlideNa})
        }

        private void SubmitCredential_Click(object sender, RoutedEventArgs e)
        {
            string userName = username.Text;
            string passWord = password.Password.ToString();
            string messageBoxText;
            string caption = "Register Error!";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBoxResult result;

            if (passWord.Length < 5)
            {
                messageBoxText = "Password is to short...";
                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                return;
            }
            var user = new User()
            {
                Username = username.Text,
                Passw = CreateMD5(password.Password.ToString()),
                Details = "",
                LastSeen = DateTime.Now,
            };
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);

                messageBoxText = "Username is already used! Choose a different one...";
                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                return;
            }
            var registeredUser = (from u in context.Users
                                  where u.Username == userName
                                  select u).First();
           
           HostWindowInFrame(this.m_frame, new PersonalChatWindow(registeredUser));
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);



                // Convert the byte array to hexadecimal string prior to .NET 5
                StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText;
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Exclamation;
            MessageBoxResult result;
            string caption = "Password Show!";
            messageBoxText = "The password you typed is: " + password.Password.ToString();
            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        }

        private Frame m_frame;

        public void anuntaFrame(Frame priv)
        {
            m_frame = priv;
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (username.Text.Length > 0)
                {
                    if (password.Password.Length > 0)
                    {
                        /// if (e.Key == Key.Enter)
                        //{
                        SubmitCredential_Click(sender, e);
                        //}
                    }
                    else
                    {
                        string messageBoxText;
                        MessageBoxButton button = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Warning;
                        MessageBoxResult result;
                        string caption = "Warning!";
                        messageBoxText = "Password field is clear!" + password.Password.ToString();
                        result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                    }
                }
                else
                {
                    string messageBoxText;
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result;
                    string caption = "Warning!";
                    messageBoxText = "Username field is clear!" + password.Password.ToString();
                    result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                }
            }
        }

        private void username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (username.Text.Length > 0)
                {
                    if (password.Password.Length > 0)
                    {
                       /// if (e.Key == Key.Enter)
                        //{
                            SubmitCredential_Click(sender, e);
                        //}
                    }
                    else
                    {
                        string messageBoxText;
                        MessageBoxButton button = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Warning;
                        MessageBoxResult result;
                        string caption = "Warning!";
                        messageBoxText = "Password field is clear!" + password.Password.ToString();
                        result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                    }
                }
                else
                {
                    string messageBoxText;
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result;
                    string caption = "Warning!";
                    messageBoxText = "Username field is clear!" + password.Password.ToString();
                    result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                }
            }
        }
    }
}
