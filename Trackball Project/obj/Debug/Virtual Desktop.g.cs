﻿#pragma checksum "..\..\Virtual Desktop.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "32B21FC4BDBCB03B9C2C4AF676044A58"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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


namespace virtual_desktop {
    
    
    /// <summary>
    /// Virtual_Desktop
    /// </summary>
    public partial class Virtual_Desktop : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\Virtual Desktop.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal virtual_desktop.Virtual_Desktop virtual_window;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Virtual Desktop.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid virtual_grid;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Virtual Desktop.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas preview_canvas;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\Virtual Desktop.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid preview_grid;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\Virtual Desktop.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image preview_image1;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\Virtual Desktop.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image preview_image2;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\Virtual Desktop.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image preview_image3;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\Virtual Desktop.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image preview_image4;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\Virtual Desktop.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image1;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\Virtual Desktop.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image2;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\Virtual Desktop.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image3;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\Virtual Desktop.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image4;
        
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
            System.Uri resourceLocater = new System.Uri("/Trackball Project;component/virtual%20desktop.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Virtual Desktop.xaml"
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
            this.virtual_window = ((virtual_desktop.Virtual_Desktop)(target));
            
            #line 9 "..\..\Virtual Desktop.xaml"
            this.virtual_window.Activated += new System.EventHandler(this.window_Activated);
            
            #line default
            #line hidden
            
            #line 10 "..\..\Virtual Desktop.xaml"
            this.virtual_window.Deactivated += new System.EventHandler(this.window_Deactivated);
            
            #line default
            #line hidden
            
            #line 11 "..\..\Virtual Desktop.xaml"
            this.virtual_window.Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 12 "..\..\Virtual Desktop.xaml"
            this.virtual_window.Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.virtual_grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.preview_canvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 29 "..\..\Virtual Desktop.xaml"
            this.preview_canvas.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.preview_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.preview_grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.preview_image1 = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.preview_image2 = ((System.Windows.Controls.Image)(target));
            return;
            case 7:
            this.preview_image3 = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.preview_image4 = ((System.Windows.Controls.Image)(target));
            return;
            case 9:
            this.Image1 = ((System.Windows.Controls.Image)(target));
            
            #line 58 "..\..\Virtual Desktop.xaml"
            this.Image1.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Image1_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.Image2 = ((System.Windows.Controls.Image)(target));
            
            #line 59 "..\..\Virtual Desktop.xaml"
            this.Image2.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Image2_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.Image3 = ((System.Windows.Controls.Image)(target));
            
            #line 60 "..\..\Virtual Desktop.xaml"
            this.Image3.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Image3_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.Image4 = ((System.Windows.Controls.Image)(target));
            
            #line 61 "..\..\Virtual Desktop.xaml"
            this.Image4.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Image4_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
