//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChatEntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Message
    {
        public int MessageID { get; set; }
        public int User_ID_Sender { get; set; }
        public int User_ID_Receiver { get; set; }
        public string Payload_Text { get; set; }
        public System.DateTime Time_sent { get; set; }
        public bool Seen { get; set; }
        public int GroupID { get; set; }
    
        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
