﻿#pragma checksum "..\..\Trackball Page.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D9F3950B18FE4607E389324752326E0E"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.34209
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Trackball_Project {
    
    
    /// <summary>
    /// Trackball_Page
    /// </summary>
    public partial class Trackball_Page : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\Trackball Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid TRACKBALL;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\Trackball Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Background_Trackball_Image;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\Trackball Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image LeftTopIcon;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\Trackball Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image RightTopIcon;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\Trackball Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image LeftDownIcon;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\Trackball Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image RightDownIcon;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\Trackball Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Trackball_Icon;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\Trackball Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Tracbk_Page_Title;
        
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
            System.Uri resourceLocater = new System.Uri("/Trackball Project;component/trackball%20page.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Trackball Page.xaml"
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
            this.TRACKBALL = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.Background_Trackball_Image = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.LeftTopIcon = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.RightTopIcon = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.LeftDownIcon = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.RightDownIcon = ((System.Windows.Controls.Image)(target));
            return;
            case 7:
            
            #line 18 "..\..\Trackball Page.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Click_LeftTop);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 35 "..\..\Trackball Page.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Click_RightTop);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 52 "..\..\Trackball Page.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Click_LeftDown);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 69 "..\..\Trackball Page.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Click_RightDown);
            
            #line default
            #line hidden
            return;
            case 11:
            this.Trackball_Icon = ((System.Windows.Controls.Image)(target));
            return;
            case 12:
            this.Tracbk_Page_Title = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

