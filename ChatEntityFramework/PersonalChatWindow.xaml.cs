
using ChatEntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using System.Windows.Shell;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using System.Windows.Interop;


//CUM SA STERG MESAJELE AM PROBLEMA CU ATTACHED???
//CUM PUTEM FACE MESAJELE SA APARA PE STANGA SI PE DREAPTA???
//CE FACEM CU ENTITY FRAMEURILE???


namespace ChatEntityFramework
{

    public partial class PersonalChatWindow : Window
    {
        public List<string> myList2 { get; set; }
        Chat_OnlineEntities context = new Chat_OnlineEntities();
        Chat_OnlineEntities contextFriends = new Chat_OnlineEntities();
        Chat_OnlineEntities contextMessages = new Chat_OnlineEntities();
        Chat_OnlineEntities contextGroups = new Chat_OnlineEntities();
        Chat_OnlineEntities contextStatus = new Chat_OnlineEntities();
        Chat_OnlineEntities ctxMSGGR = new Chat_OnlineEntities();
        public ObservableCollection<contact_model> contactsList { get; set; }
        //public List<string> HoIsHe;
        public ObservableCollection<Message> messList { get; set; }
        private List<int> m_alreadyUsed;
        private User currentUser;
        private Mutex mutex = new Mutex();
        private String searchedUsername;
        private String userState;
        private contact_model selectedContact;

        private List<string> m_imageList;
        private string message_input_text { get; set; }

        private int hisID;

        private byte[] _rawImageData;
        public byte[] RawImageData
        {
            get { return _rawImageData; }
            set
            {
                if (value != _rawImageData)
                {
                    _rawImageData = value;
                    //NotifyPropertyChanged("RawImageData");
                }
            }
        }
        void set_initial_status()
        {
            hisID = -1;
            var user = (from cacat in context.Users where cacat.UserID == currentUser.UserID select cacat).First();
            String rahat = user.Details;
            StatusLabel.Content = rahat;
            if (rahat == "Online")
            {
                StatusLabel.Content = rahat;
                StatusImage.Source = new BitmapImage(new Uri(@"green_bullet30.png", UriKind.Relative));
            }
            else if (rahat == "Offline")
            {
                StatusLabel.Content = rahat;
                StatusImage.Source = new BitmapImage(new Uri(@"gray_bullet10.png", UriKind.Relative));
            }
            else if (rahat == "Away")
            {
                StatusLabel.Content = rahat;
                StatusImage.Source = new BitmapImage(new Uri(@"/orange_bullet.png", UriKind.Relative));
            }
            else if (rahat == "Busy")
            {
                StatusLabel.Content = rahat;
                StatusImage.Source = new BitmapImage(new Uri(@"/red_bullet30.png", UriKind.Relative));
            }
        }

        public void set_profile_picture()
        {
            Random randomx = new Random();
            int randomIndex = 0;
            randomIndex = randomx.Next(0, 61);
            if (m_alreadyUsed.Count() == 0)
            {
                m_alreadyUsed.Add(randomIndex);
            }
            else
            {
                bool check = false;
                while (true)
                {
                    for (int j = 0; j < m_alreadyUsed.Count(); j++)
                    {
                        if (randomIndex == m_alreadyUsed[j])
                        {
                            check = true;
                            break;
                        }
                    }
                    if (check == true)
                    {
                        randomIndex = randomx.Next(0, 61);
                        check = false;
                    }
                    else
                    {
                        m_alreadyUsed.Add(randomIndex);
                        break;
                    }
                }
            }
            string filePath = @"D:\FACULTATE\ChatEntityFramework\ChatEntityFramework" + m_imageList[randomIndex];
            ImageSource imgSource = new BitmapImage(new Uri(filePath));
            ProfilePictureX.ImageSource = imgSource;
        }
        public PersonalChatWindow(User loggedInUser)
        {

            InitializeComponent();

            context.Database.Connection.Open();
            contextFriends.Database.Connection.Open();
            contextMessages.Database.Connection.Open();
            contextGroups.Database.Connection.Open();
            contextStatus.Database.Connection.Open();

            messList = new ObservableCollection<Message>();
            contactsList = new ObservableCollection<contact_model>();
            //this.HoIsHe = new List<string>();
            currentUser = loggedInUser;
            m_imageList = new List<string>();
            m_alreadyUsed = new List<int>();
            Thread messageThread = new Thread(ThreadFuncForMessages);
            Thread friendsThread = new Thread(ThreadFuncForFriends);
            Thread statusThread = new Thread(ThreadFuncForStatus);

            for (int i = 0; i < 62; i++)
            {
                this.m_imageList.Add("/" + (i+1).ToString() + ".png");
            }


            set_profile_picture();
            set_initial_status();
            statusThread.Start();
            friendsThread.Start();
            messageThread.Start();
        }
        private void initContactList()
        {
           
           initFriends();
           //addMyGroupsInContactList();
        }
        private void initFriends()
        {

           
                var list = getListofFriends();

                List<Group> groupsIAmPartOf = (from uig in contextGroups.UsersInGroups
                                               join groups in contextGroups.Groups
                                               on uig.GroupID equals groups.GroupID
                                               where uig.UserID == currentUser.UserID
                                               select groups).ToList();
                var groupListExtractedFromContactList = contactsList.Where(group => group.isAGroup == true);
                foreach (var group in groupsIAmPartOf)
                {
                    if (groupListExtractedFromContactList.Where(inListGroup => inListGroup.m_username == group.GroupName).Count() == 0)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            contactsList.Add(new contact_model
                            {
                                m_username = group.GroupName,
                                isAGroup = true,
                            });
                        });
                    }
                }
                this.Dispatcher.Invoke(() =>
                {
                    ContactList.ItemsSource = contactsList;
                });

            bool isTrue=false;
                this.Dispatcher.Invoke(() =>
                {
                    //mutex.WaitOne();
                    if (list.Count + groupsIAmPartOf.Count == contactsList.Count)
                    {
                        isTrue = true;
                    }
                    else
                    {
                        Console.Write($"NOT EQUAL {list.Count}-{contactsList.Count}");
                    }
                    //mutex.ReleaseMutex();

                });
                 if (isTrue)
                      return;
                this.Dispatcher.Invoke(() =>
                {
                    myUsername.Content = currentUser.Username;
                });
                //mutex.WaitOne();
                this.Dispatcher.Invoke(() =>
                {

                    contactsList.Clear();
                    for (int i = 0; i < list.Count; i++)
                    {
                        Random randomx = new Random();
                        int randomIndex = 0;
                        randomIndex = randomx.Next(0, 61);
                        if (m_alreadyUsed.Count() == 0)
                        {
                            m_alreadyUsed.Add(randomIndex);
                        }
                        else
                        {
                            bool check = false;
                            while (true)
                            {
                                for (int j = 0; j < m_alreadyUsed.Count(); j++)
                                {
                                    if (randomIndex == m_alreadyUsed[j])
                                    {
                                        check = true;
                                        break;
                                    }
                                }
                                if (check == true)
                                {
                                    randomIndex = randomx.Next(0, 61);
                                    check = false;
                                }
                                else
                                {
                                    m_alreadyUsed.Add(randomIndex);
                                    break;
                                }
                            }
                        }
                        string filePath = @"D:\FACULTATE\ChatEntityFramework\ChatEntityFramework" + m_imageList[i];
                        ImageSource imgSource = new BitmapImage(new Uri(filePath));
                        //finalImage.Source = new BitmapImage(new Uri("pack://application:,,,/AssemblyName;component/Resources/logo.png"));
                        contactsList.Add(new contact_model
                        {
                            m_username = list[i],
                            m_imageSource = imgSource
                        });
                    }

                });
                this.Dispatcher.Invoke(() =>
                {
                    ContactList.ItemsSource = contactsList;
                });
                //mutex.ReleaseMutex();
          
        }


        private List<String> getListofFriends()
        {
            List<String> ListOfFriends = new List<String>();
            //mutex.WaitOne();
            var friendsID = (from friend in contextFriends.Friends
                             where friend.Friend1ID == currentUser.UserID
                             select friend.Friend2ID).ToList();
            friendsID.AddRange((from friend in contextFriends.Friends
                                where friend.Friend2ID == currentUser.UserID
                                select friend.Friend1ID).ToList());
            var allUsers = (from user in context.Users
                            select user);
            //mutex.ReleaseMutex();
            
           
            var myfriends = allUsers.Where(x => friendsID.Contains(x.UserID)).ToList();

            foreach (var friend in myfriends)
            {
                ListOfFriends.Add(friend.Username);
            }
            return ListOfFriends;
        }

        private void getStatus()
        {
            if (hisID < 0 || hisID == 9999)
                return;

            var partener = (from u in context.Users
                            where u.UserID == hisID
                            select u).First();
            
            var hisusername = partener.Username;
            this.Dispatcher.Invoke(() =>
            {
                ConversationPartener.Content = hisusername;
            });
            var status = partener.Details;
            this.Dispatcher.Invoke(() =>
            {
                StatusImageReceiver.Visibility = Visibility.Collapsed;
                if (status == "Online")
                {
                    StatusImageReceiver.Visibility = Visibility.Visible;
                    StatusImageReceiver.Source = new BitmapImage(new Uri(@"green_bullet30.png", UriKind.Relative));
                }
                else if (status == "Busy")
                {
                    StatusImageReceiver.Visibility = Visibility.Visible;
                    StatusImageReceiver.Source = new BitmapImage(new Uri(@"/red_bullet30.png", UriKind.Relative));
                }
                else if (status == "Offline")
                {
                    StatusImageReceiver.Visibility = Visibility.Visible;
                    StatusImageReceiver.Source = new BitmapImage(new Uri(@"gray_bullet10.png", UriKind.Relative));
                }
                else if (status == "Away")
                {
                    StatusImageReceiver.Visibility = Visibility.Visible;
                    StatusImageReceiver.Source = new BitmapImage(new Uri(@"/orange_bullet.png", UriKind.Relative));
                }
                else if (status == "")
                {
                    StatusImageReceiver.Visibility = Visibility.Collapsed;
                }
            });
        }

        private void ThreadFuncForStatus()
        {
            while (true)
            {
                getStatus();
                Thread.Sleep(1000);
            }
        }
        private void ThreadFuncForMessages()
        {
            while(true)
            {
                getMyMessages();
                Thread.Sleep(250);
            }

        }
        private void ThreadFuncForFriends()
        {
            while (true)
            {
                try
                {
                    initContactList();
                    Thread.Sleep(750);
                }
                catch(Exception e)
                {
                    continue;
                }

            }
           

        }
        private void getMyMessages()
        {
            if(hisID<0)
            {
                return;
            }
            List<Message> messages = new List<Message>();


            contact_model selectedItem = selectedContact;
            if (selectedItem == null)
                return;
            string nameOfSelectedItem = selectedItem.m_username;

            var isAGroup=(from groups in contextGroups.Groups
                          where nameOfSelectedItem==groups.GroupName
                          select groups).FirstOrDefault();
            try
            {
                if (selectedItem.isAGroup == false && isAGroup == null)
                {
                    // mutex.WaitOne();
                    messages = (from msg in contextMessages.Messages
                                where msg.User_ID_Sender == currentUser.UserID
                                where msg.User_ID_Receiver == hisID
                                select msg).ToList();
                    var receivedMessages = (from msg in contextMessages.Messages
                                            where msg.User_ID_Sender == hisID
                                            where msg.User_ID_Receiver == currentUser.UserID
                                            select msg).ToList();

                    messages.AddRange(receivedMessages);
                    var tempList = messages;
                    var hisusername = (from u in contextMessages.Users
                                       where u.UserID == hisID
                                       select u.Username).First();
                    // mutex.ReleaseMutex();

                    messList.Clear();
                    foreach (Message mesg in tempList)
                    {
                        String username;
                        string HoIsHe = "";
                        if (mesg.User_ID_Sender == hisID)
                        {
                            username = hisusername;
                            HoIsHe = "Sender";
                        }
                        else
                        {
                            username = currentUser.Username;
                            HoIsHe = "Receiver";
                        }
                        messList.Add(new Message
                        {
                            Payload_Text = mesg.Time_sent.ToString("HH:mm") + " " + username + " :" + mesg.Payload_Text,
                            Time_sent = mesg.Time_sent,
                            Seen = mesg.Seen,
                            User_ID_Sender= mesg.User_ID_Sender,
                        });
                    }

                    var sorted = messList.ToList();
                    sorted.Sort((Message x, Message y) => x.Time_sent.CompareTo(y.Time_sent));
                    this.Dispatcher.Invoke(() =>
                        {
                            MessageList.ItemsSource = sorted;
                        });
                }
                else
                {
                    messages = (from msg in contextMessages.Messages
                                where msg.GroupID == isAGroup.GroupID
                                select msg).ToList();



                    var tempList = messages;


                    messList.Clear();
                    foreach (Message mesg in tempList)
                    {
                        String username = mesg.User.Username;
                        string HoIsHe = "";

                        messList.Add(new Message
                        {
                            Payload_Text = mesg.Time_sent.ToString("HH:mm") + " " + username + " :" + mesg.Payload_Text,
                            Time_sent = mesg.Time_sent,
                            Seen = mesg.Seen,
                            User_ID_Sender=mesg.User_ID_Sender,

                        });
                    }

                    var sorted = messList.ToList();
                    sorted.Sort((Message x, Message y) => x.Time_sent.CompareTo(y.Time_sent));
                    this.Dispatcher.Invoke(() =>
                    {
                        MessageList.ItemsSource = sorted;
                    });
                }
            }
            catch(Exception exc)
            {
                return;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                System.Windows.Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                System.Windows.Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            mutex.WaitOne();
            var me = (from user in context.Users
                      where user.UserID == currentUser.UserID
                      select user).First();
            mutex.ReleaseMutex();
            me.LastSeen = DateTime.Now;
            context.SaveChanges();
            Console.WriteLine("Close");
            System.Windows.Application.Current.Shutdown();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText;
            string caption = "Login Error!";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBoxResult result;
            contact_model selectedItem = (contact_model)ContactList.SelectedItem;

            if (selectedItem.isAGroup == false)
            {
                String searchedUsername = selectedItem.m_username;
               // mutex.WaitOne();
                hisID = (from user in context.Users
                         where user.Username == searchedUsername
                         select user.UserID).First();
               // mutex.ReleaseMutex();
                message_input_text = MessageInput.Text;
                var message = new Message
                {
                    Payload_Text = message_input_text,
                    User_ID_Sender = currentUser.UserID,
                    User_ID_Receiver = hisID,
                    Time_sent = DateTime.Now,
                    Seen = false,
                    GroupID=9999,
                };
                bool success = false;
                while (success == false)
                {
                    try
                    {

                        mutex.WaitOne();
                        context.Messages.Add(message);
                        context.SaveChanges();
                        mutex.ReleaseMutex();
                        success = true;
                    }
                    catch (Exception exc)
                    {
                        success = false;
                        messageBoxText = "Sender not selected!";
                        //result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                        Console.WriteLine(exc.Message);
                    }
                }
                //getMyMessages();
            }
            else
            {
                String groupName = selectedItem.m_username;

                try
                {
                    var requestedGroup = (from groups in ctxMSGGR.Groups
                                          where groups.GroupName == groupName
                                          select groups).FirstOrDefault();
                    if (requestedGroup == null)
                        throw new Exception($"Group groupName({groupName}) is not in the data base!");

                    message_input_text = MessageInput.Text;
                    var message = new Message
                    {
                        Payload_Text = message_input_text,
                        User_ID_Sender = currentUser.UserID,
                        User_ID_Receiver = 17,
                        Time_sent = DateTime.Now,
                        Seen = false,
                        GroupID = requestedGroup.GroupID

                    };
                    try
                    {
                        ctxMSGGR.Messages.Add(message);
                        ctxMSGGR.SaveChanges();
                    }
                    catch (Exception exc)
                    {
                        messageBoxText = "Sender not selected!";
                        result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                        Console.WriteLine(exc.Message);
                        return;
                    }
                   
                    /*
                    messList.Clear();

                    foreach (Message mesg in listOfMessagesFromGroup)
                    {
                        String username = "";

                        messList.Add(new Message
                        {
                            Payload_Text = mesg.Time_sent.ToString("HH:mm") + " " + username + " :" + mesg.Payload_Text,
                            Time_sent = mesg.Time_sent,
                            Seen = mesg.Seen,
                            GroupID = requestedGroup.GroupID,
                        });
                    }
                    var sorted = messList.ToList();
                    sorted.Sort((Message x, Message y) => x.Time_sent.CompareTo(y.Time_sent));
                    MessageList.ItemsSource = sorted;*/

                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message.ToString());
                }
            }
            MessageInput.Clear();
        }
        public void addFriendsToContactList()
        {

            var list = getListofFriends();
            myUsername.Content = currentUser.Username;
            mutex.WaitOne();
            foreach (var friend in list)
            {
                if (contactsList.Where(user => user.m_username == friend).Count() == 0)
                {
                    contactsList.Add(new contact_model
                    {
                        m_username = friend
                    });
                }
            }

            ContactList.ItemsSource = contactsList;
            mutex.ReleaseMutex();
        }
        private void addMyGroupsInContactList()
        {
           
        }
        private void seenMessages()
        {
            

        }

        //to do:
        /*
         * STATUS => window cu status
         * DELETE message for everyone
         * REFRESH BUTTON sa arate mai profi
         * SEEN MESSAGES => blue tick pentru mesajele vazute sau bulinuta albastra/ ceva semn distinctiv
         * ADD Photots
         * FORMAT MAI FRUMOS MESAJE
         * LAST SEEN
         * GRUPURI --de inceput
         * 
         */
        private void ContactList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //initFriends();
            //addFriendsToContactList();
            //seenMessages();

            selectedContact = (contact_model)ContactList.SelectedItem;
            if (selectedContact == null)
                return;
            if (selectedContact.isAGroup == false)
            {
                String searchedUsername = selectedContact.m_username;
                mutex.WaitOne();
                hisID = (from user in context.Users
                             where user.Username == searchedUsername
                             select user.UserID).First();
                mutex.ReleaseMutex();

                messList.Clear();
              //  getMyMessages();
                //var tempList = getMyMessages(hisID);
                mutex.WaitOne();
                var partener = (from u in context.Users
                                where u.UserID == hisID
                                select u).First();
                mutex.ReleaseMutex();
                var hisusername = partener.Username;
                ConversationPartener.Content = hisusername;
                var status = partener.Details;
                StatusImageReceiver.Visibility = Visibility.Collapsed;
                if (status == "Online")
                {
                    StatusImageReceiver.Visibility = Visibility.Visible;
                    StatusImageReceiver.Source = new BitmapImage(new Uri(@"green_bullet30.png", UriKind.Relative));
                }
                else if (status == "Busy")
                {
                    StatusImageReceiver.Visibility = Visibility.Visible;
                    StatusImageReceiver.Source = new BitmapImage(new Uri(@"/red_bullet30.png", UriKind.Relative));
                }
                else if (status == "Offline")
                {
                    StatusImageReceiver.Visibility = Visibility.Visible;
                    StatusImageReceiver.Source = new BitmapImage(new Uri(@"gray_bullet10.png", UriKind.Relative));
                }
                else if (status == "Away")
                {
                    StatusImageReceiver.Visibility = Visibility.Visible;
                    StatusImageReceiver.Source = new BitmapImage(new Uri(@"/orange_bullet.png", UriKind.Relative));
                }
                else if (status == "")
                {
                    StatusImageReceiver.Visibility = Visibility.Collapsed;
                }

                LastSeen.Content = "Last seen: " + partener.LastSeen.ToString("HH:mm");
                /*
                foreach (Message mesg in tempList)
                {
                    String username;
                    if (mesg.User_ID_Sender == hisID)
                        username = hisusername;
                    else
                        username = currentUser.Username;
                    messList.Add(new Message
                    {
                        Payload_Text = mesg.Time_sent.ToString("HH:mm") + " " + username + " :" + mesg.Payload_Text,
                        Time_sent = mesg.Time_sent,
                        Seen = mesg.Seen,
                        GroupID = 0,
                    });
                }
                var sorted = messList.ToList();
                sorted.Sort((Message x, Message y) => x.Time_sent.CompareTo(y.Time_sent));
                MessageList.ItemsSource = sorted;
                */
            }
            else
            {
                hisID = 9999;
                String groupName = selectedContact.m_username;
                try
                {
                    var requestedGroup = (from groups in ctxMSGGR.Groups
                                          where groups.GroupName == groupName
                                          select groups).FirstOrDefault();
                    if (requestedGroup == null)
                        throw new Exception($"Group groupName({groupName}) is not in the data base!");
                    List<Message> listOfMessagesFromGroup = (from msg in ctxMSGGR.Messages
                                                             where msg.GroupID == requestedGroup.GroupID
                                                             select msg).ToList();
                    ConversationPartener.Content = groupName;
                    messList.Clear();

                    foreach (Message mesg in listOfMessagesFromGroup)
                    {
                        String username = "";

                        messList.Add(new Message
                        {
                            Payload_Text = mesg.Time_sent.ToString("HH:mm") + " " + username + " :" + mesg.Payload_Text,
                            Time_sent = mesg.Time_sent,
                            Seen = mesg.Seen,
                            GroupID = requestedGroup.GroupID,
                        });
                    }
                    var sorted = messList.ToList();
                    sorted.Sort((Message x, Message y) => x.Time_sent.CompareTo(y.Time_sent));
                    MessageList.ItemsSource = sorted;

                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message.ToString());
                }

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           AddFriend friendWindow = new AddFriend(currentUser.UserID, this);
           friendWindow.Show();

        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //addFriendsToContactList();
            
            create_group window_for_group = new create_group(currentUser);
            window_for_group.Show();
     
        }

        private void addUserInGroup(int groupID, int userID)
        {
            UsersInGroup joinGroup = new UsersInGroup
            {
                GroupID = groupID,
                UserID = userID,
            };
            context.UsersInGroups.Add(joinGroup);
            context.SaveChanges();
        }
        private void leaveGroup(int groupID, int userID)
        {
            try
            {
                var leaveGroup = (from userInGroup in context.UsersInGroups
                                  where userInGroup.GroupID == groupID &&
                                  userInGroup.UserID == userID
                                  select userInGroup).FirstOrDefault();
                if (leaveGroup != null)
                {
                    context.UsersInGroups.Remove(leaveGroup);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception($"Can not leave group {groupID} because user {userID} is not in group...");
                }
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message.ToString());
            }
        }


        private void MessageList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ///????
            Message msg = (Message)MessageList.SelectedItem;
            msg.Time_sent = DateTime.Now;

            context.Messages.Remove(msg);
            var listOfMessages = MessageList.ItemsSource;
            listOfMessages.Cast<Message>().ToList().Remove(msg);
            MessageList.ItemsSource = listOfMessages;
            context.SaveChanges();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var thisUserLoggedIn = (from user in context.Users
                                    where user.UserID == currentUser.UserID
                                    select user).First();
            changeStare cS = new changeStare(thisUserLoggedIn, context);
            cS.atribuie_label(StatusLabel);
            cS.atribuie_image(StatusImage);
            cS.Show();
        }

        public void changeState(String state)
        {
            this.userState = state;
        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string messageBoxText;
                string caption = "Login Error!";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                contact_model selectedItem = (contact_model)ContactList.SelectedItem;

                if (selectedItem.isAGroup == false)
                {
                    String searchedUsername = selectedItem.m_username;
                    // mutex.WaitOne();
                    hisID = (from user in context.Users
                             where user.Username == searchedUsername
                             select user.UserID).First();
                    // mutex.ReleaseMutex();
                    message_input_text = MessageInput.Text;
                    var message = new Message
                    {
                        Payload_Text = message_input_text,
                        User_ID_Sender = currentUser.UserID,
                        User_ID_Receiver = hisID,
                        Time_sent = DateTime.Now,
                        Seen = false,
                        GroupID = 9999,
                    };
                    bool success = false;
                    while (success == false)
                    {
                        try
                        {

                            mutex.WaitOne();
                            context.Messages.Add(message);
                            context.SaveChanges();
                            mutex.ReleaseMutex();
                            success = true;
                        }
                        catch (Exception exc)
                        {
                            success = false;
                            messageBoxText = "Sender not selected!";
                            //result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                            Console.WriteLine(exc.Message);
                        }
                    }
                    //getMyMessages();
                }
                else
                {
                   
                    String groupName = selectedItem.m_username;

                    try
                    {
                        var requestedGroup = (from groups in context.Groups
                                              where groups.GroupName == groupName
                                              select groups).FirstOrDefault();
                        if (requestedGroup == null)
                            throw new Exception($"Group groupName({groupName}) is not in the data base!");

                        message_input_text = MessageInput.Text;
                        var message = new Message
                        {
                            Payload_Text = message_input_text,
                            User_ID_Sender = currentUser.UserID,
                            User_ID_Receiver = 17,
                            Time_sent = DateTime.Now,
                            Seen = false,
                            GroupID = requestedGroup.GroupID

                        };
                        try
                        {
                            context.Messages.Add(message);
                            context.SaveChanges();
                        }
                        catch (Exception exc)
                        {
                            messageBoxText = "Sender not selected!";
                            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                            Console.WriteLine(exc.Message);
                            return;
                        }
                        List<Message> listOfMessagesFromGroup = (from msg in context.Messages
                                                                 where msg.GroupID == requestedGroup.GroupID
                                                                 select msg).ToList();

                        messList.Clear();

                        foreach (Message mesg in listOfMessagesFromGroup)
                        {
                            String username = "";

                            messList.Add(new Message
                            {
                                Payload_Text = mesg.Time_sent.ToString("HH:mm") + " " + username + " :" + mesg.Payload_Text,
                                Time_sent = mesg.Time_sent,
                                Seen = mesg.Seen,
                                GroupID = requestedGroup.GroupID,
                            });
                        }
                        var sorted = messList.ToList();
                        sorted.Sort((Message x, Message y) => x.Time_sent.CompareTo(y.Time_sent));
                        MessageList.ItemsSource = sorted;

                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message.ToString());
                    }
                }
            }
        }

        private void MessageInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string messageBoxText;
                string caption = "Login Error!";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                contact_model selectedItem = (contact_model)ContactList.SelectedItem;
                //?EWROEROAEIROPAEUROPAHDFIO{UASHDOYHASD{OJHASODIYHAS{OUIDHASIO{DHASOIHDAOSIDH{AOSIHDIA
                if (selectedItem.isAGroup == false)
                {
                    String searchedUsername = selectedItem.m_username;
                    // mutex.WaitOne();
                    hisID = (from user in context.Users
                             where user.Username == searchedUsername
                             select user.UserID).First();
                    // mutex.ReleaseMutex();
                    message_input_text = MessageInput.Text;
                    var message = new Message
                    {
                        Payload_Text = message_input_text,
                        User_ID_Sender = currentUser.UserID,
                        User_ID_Receiver = hisID,
                        Time_sent = DateTime.Now,
                        Seen = false,
                        GroupID = 9999,
                    };
                    bool success = false;
                    while (success == false)
                    {
                        try
                        {

                            mutex.WaitOne();
                            context.Messages.Add(message);
                            context.SaveChanges();
                            mutex.ReleaseMutex();
                            success = true;
                        }
                        catch (Exception exc)
                        {
                            success = false;
                            messageBoxText = "Sender not selected!";
                            //result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                            Console.WriteLine(exc.Message);
                        }
                    }
                    //getMyMessages();
                }
                else
                {
                    String groupName = selectedItem.m_username;

                    try
                    {
                        var requestedGroup = (from groups in context.Groups
                                              where groups.GroupName == groupName
                                              select groups).FirstOrDefault();
                        if (requestedGroup == null)
                            throw new Exception($"Group groupName({groupName}) is not in the data base!");

                        message_input_text = MessageInput.Text;
                        var message = new Message
                        {
                            Payload_Text = message_input_text,
                            User_ID_Sender = currentUser.UserID,
                            User_ID_Receiver = 9999,
                            Time_sent = DateTime.Now,
                            Seen = false,
                            GroupID = requestedGroup.GroupID

                        };
                        try
                        {
                            context.Messages.Add(message);
                            context.SaveChanges();
                        }
                        catch (Exception exc)
                        {
                            messageBoxText = "Sender not selected!";
                            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                            Console.WriteLine(exc.Message);
                            return;
                        }
                        List<Message> listOfMessagesFromGroup = (from msg in context.Messages
                                                                 where msg.GroupID == requestedGroup.GroupID
                                                                 select msg).ToList();

                        messList.Clear();

                        foreach (Message mesg in listOfMessagesFromGroup)
                        {
                            String username = "";

                            messList.Add(new Message
                            {
                                Payload_Text = mesg.Time_sent.ToString("HH:mm") + " " + username + " :" + mesg.Payload_Text,
                                Time_sent = mesg.Time_sent,
                                Seen = mesg.Seen,
                                GroupID = requestedGroup.GroupID,
                            });
                        }
                        var sorted = messList.ToList();
                        sorted.Sort((Message x, Message y) => x.Time_sent.CompareTo(y.Time_sent));
                        MessageList.ItemsSource = sorted;

                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message.ToString());
                    }
                }
                MessageInput.Clear();
            }
        }
    }
}
