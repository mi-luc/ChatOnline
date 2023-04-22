using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChatEntityFramework
{
    public class contact_model
    {
        public string m_username { get; set; }
        public ImageSource m_imageSource { get; set; }
        public string m_lastMessage { get; set; }

        public bool isAGroup { get; set; } = false;

        public ObservableCollection<string> m_messages { get; set; }

    }
}
