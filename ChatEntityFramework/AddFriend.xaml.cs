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
using System.Windows.Shapes;

namespace ChatEntityFramework
{
    /// <summary>
    /// Interaction logic for AddFriend.xaml
    /// </summary>
    public partial class AddFriend : Window
    {

        Chat_OnlineEntities context = new Chat_OnlineEntities();

        private int myID;
        private PersonalChatWindow chatWindow;
        public AddFriend(int ID, PersonalChatWindow win)
        {

            InitializeComponent();
            myID = ID;
            chatWindow = win;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var isEntry = (from user in context.Users
                           join friend in context.Friends on user.UserID equals friend.Friend1ID
                           where user.UserID == myID
                           select friend.Friend2ID).ToList();
            int hisID = -1;
            try
            {
                hisID = (from user in context.Users
                         where user.Username == Search_Name.Text
                         select user.UserID).First();
                if (hisID == myID)
                    throw new Exception("Nu te poti imprieteni cu tine");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
                return;
            }
            if (isEntry.Contains(hisID))
            {

                Console.WriteLine("Already friends!");
            }
            else
            {
                try
                {
                    Friend addFriend = new Friend
                    {
                        Friend1ID = myID,
                        Friend2ID = hisID,
                    };
                    context.Friends.Add(addFriend);
                    context.SaveChanges();
                    chatWindow.addFriendsToContactList();
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message.ToString());
                }
            }
        }


    }
}
