﻿#pragma checksum "..\..\PersonalChatWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D61259BE4F0F9C24FA0F873529DB6AA670CAB3F844F297B03498404D7A01D6EF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ChatEntityFramework;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ChatEntityFramework {
    
    
    /// <summary>
    /// PersonalChatWindow
    /// </summary>
    public partial class PersonalChatWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 42 "..\..\PersonalChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ContactTitle;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\PersonalChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ContactList;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\PersonalChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ImageBrush ProfilePictureX;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\PersonalChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label myUsername;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\PersonalChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label StatusLabel;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\PersonalChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image StatusImage;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\PersonalChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ConversationPartener;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\PersonalChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image StatusImageReceiver;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\PersonalChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MessageInput;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\PersonalChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView MessageList;
        
        #line default
        #line hidden
        
        
        #line 179 "..\..\PersonalChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LastSeen;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ChatEntityFramework;component/personalchatwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PersonalChatWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 24 "..\..\PersonalChatWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Border_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ContactTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.ContactList = ((System.Windows.Controls.ListView)(target));
            
            #line 51 "..\..\PersonalChatWindow.xaml"
            this.ContactList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ContactList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ProfilePictureX = ((System.Windows.Media.ImageBrush)(target));
            return;
            case 5:
            this.myUsername = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.StatusLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.StatusImage = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.ConversationPartener = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.StatusImageReceiver = ((System.Windows.Controls.Image)(target));
            return;
            case 10:
            
            #line 108 "..\..\PersonalChatWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 120 "..\..\PersonalChatWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_3);
            
            #line default
            #line hidden
            return;
            case 12:
            this.MessageInput = ((System.Windows.Controls.TextBox)(target));
            
            #line 144 "..\..\PersonalChatWindow.xaml"
            this.MessageInput.KeyDown += new System.Windows.Input.KeyEventHandler(this.MessageInput_KeyDown);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 157 "..\..\PersonalChatWindow.xaml"
            ((System.Windows.Controls.Button)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Button_KeyDown);
            
            #line default
            #line hidden
            
            #line 158 "..\..\PersonalChatWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.MessageList = ((System.Windows.Controls.ListView)(target));
            
            #line 164 "..\..\PersonalChatWindow.xaml"
            this.MessageList.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.MessageList_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 170 "..\..\PersonalChatWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 16:
            this.LastSeen = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

