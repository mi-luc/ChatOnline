using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
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
    /// Interaction logic for create_group.xaml
    /// </summary>
    public partial class create_group : Window
    {

        private List<string> m_selectedFriends = new List<string>();
        private List<string> m_friendList = new List<string>();
        private bool m_isFirstFriendAdded = false;
        private User currentUser;
        public create_group(User currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            initialise_list();
        }

        void initialise_list()
        {
           
           
            Chat_OnlineEntities contextFriends = new Chat_OnlineEntities();
            var friendsID = (from friend in contextFriends.Friends
                             where friend.Friend1ID == currentUser.UserID
                             select friend.Friend2ID).ToList();
            friendsID.AddRange((from friend in contextFriends.Friends
                                where friend.Friend2ID == currentUser.UserID
                                select friend.Friend1ID).ToList());
            var allUsers = (from user in contextFriends.Users
                            select user);
          
            var myfriends = allUsers.Where(x => friendsID.Contains(x.UserID)).ToList();

            foreach (var friend in myfriends)
            {
                FriendList.Items.Add(friend.Username);
                m_friendList.Add(friend.Username);
            }
          
           
        }
       
        private void FriendList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FriendList.SelectedIndex == -1) { return; }
            m_selectedFriends.Add(FriendList.SelectedItem.ToString());
            FriendList.Items.Remove(FriendList.SelectedItem.ToString());
            FriendList.SelectedIndex = -1;
        }

        private void CreateGroup_Click(object sender, RoutedEventArgs e)
        {
            string namedGroup = GroupName.Text;
            var context = new Chat_OnlineEntities();
            Group group = new Group()
            {
                GroupName = namedGroup,
                TimeCreated = DateTime.Now,
            };
            List<UsersInGroup> listaUtilizatoriGrup = new List<UsersInGroup>();

            if (namedGroup.Length > 3)
            {
                context.Groups.Add(group);
                context.SaveChanges();
            }
            else
                return;

            var groupGet = (from groups in context.Groups
                            where groups.GroupName == namedGroup
                            select groups).FirstOrDefault();

            foreach (var user in m_friendList)
            {
                var id = (from users in context.Users
                          where users.Username == user
                          select users).FirstOrDefault();
                listaUtilizatoriGrup.Add(new UsersInGroup()
                {
                    UserID = id.UserID,
                    GroupID = groupGet.GroupID,
                });
            }
            UsersInGroup me = new UsersInGroup()
            { 
             UserID=currentUser.UserID,
             GroupID=groupGet.GroupID,
            };

            listaUtilizatoriGrup.Add(me);

            foreach (var friend in listaUtilizatoriGrup)
            {
                context.UsersInGroups.Add(friend);
                context.SaveChanges();
            }


        }
    }
}
