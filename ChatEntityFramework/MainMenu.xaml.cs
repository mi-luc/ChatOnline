using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ChatEntityFramework
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
  
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            //Legatura cu baza de date Azure MySQL
            var connectionstring = "Server=tcp:chat-onsrv.database.windows.net,1433;Initial Catalog=chat-db;Persist Security Info=False;User ID=admin-db;Password=Luca2001;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionstring);
            
            connection.Open();
            
            InitializeComponent();
            DispatcherTimer LiveTime = new DispatcherTimer();
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += timer_Tick;
            LiveTime.Start();
            LiveDateLabel.Content = System.DateTime.Now.ToShortDateString();
        }
        public void HostWindowInFrame(Frame fraContainer, Window win)
        {
            object tmp = win.Content;
            win.Content = null;
            //Page myPage = null;
            fraContainer.Content = new ContentControl() { Content = tmp };
            // m_frame.Navigate(typeof(Page), null, new SlideNavigationTransitionInfo() { Effect = SlideNa})
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow LoginW = new LoginWindow();
            LoginW.anuntaFrame(this.m_frame);
            HostWindowInFrame(this.m_frame, LoginW);
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow RegisterW = new RegisterWindow();
            RegisterW.anuntaFrame(this.m_frame);
            HostWindowInFrame(this.m_frame, RegisterW);
        }
        void timer_Tick(object sender, EventArgs e)
        {
            LiveTimeLabel.Content = DateTime.Now.ToString("HH:mm:ss");
        }
        public void anuntaFrame(Frame priv)
        {
            m_frame = priv;
        }

        private Frame m_frame;
    }
}
