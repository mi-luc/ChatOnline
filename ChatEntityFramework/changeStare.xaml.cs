using ChatEntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

namespace ChatEntityFramework
{
    /// <summary>
    /// Interaction logic for changeStare.xaml
    /// </summary>
    public partial class changeStare : Window
    {
        private String m_stare;
        private bool m_modified;
        private Label to_modify;
        private Image to_modify_Image;
        private User currentUser;
        private Chat_OnlineEntities ctx;
        public String get_m_stare()
        {
            return this.m_stare;
        }
        public void atribuie_label(Label lala)
        {
            this.to_modify = lala;
        }

        public void atribuie_image(Image lala)
        {
            this.to_modify_Image = lala;
        }

        public bool get_m_modified()
        {
            return this.m_modified;
        }
        public changeStare(User userLoggedIn, Chat_OnlineEntities context)
        {

            InitializeComponent();
            currentUser = userLoggedIn;
            ctx = context;
        }

        private void State_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //this.to_modify = 
            //this.m_modified = true;

            var xxx = State.SelectedIndex;

            to_modify.Content = m_stare;
            if (xxx == 0)
            {
                to_modify_Image.Source = new BitmapImage(new Uri(@"/red_bullet30.png", UriKind.Relative));
                to_modify.Content = "Busy";
                currentUser.Details = "Busy";
            }
            else if (xxx == 1)
            {
                to_modify_Image.Source = new BitmapImage(new Uri(@"green_bullet30.png", UriKind.Relative));
                to_modify.Content = "Online";
                currentUser.Details = "Online";
            }
            else if (xxx == 2)
            {
                to_modify_Image.Source = new BitmapImage(new Uri(@"/gray_bullet10.png", UriKind.Relative));
                to_modify.Content = "Offline";
                currentUser.Details = "Offline";
            }
            else if (xxx == 3)
            {
                to_modify_Image.Source = new BitmapImage(new Uri(@"/orange_bullet.png", UriKind.Relative));
                to_modify.Content = "Away";
                currentUser.Details = "Away";
            }
            //ctx.Users.InsertOnSubmit(currentUser);
            ctx.Users.AddOrUpdate(currentUser);
            ctx.SaveChanges();
        }
    }
}
