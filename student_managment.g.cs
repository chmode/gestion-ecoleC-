﻿#pragma checksum "..\..\..\student_managment.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B6A053ECB0DEAFCE17F1061D42D485DC1A704045"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using StudentManagement;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace StudentManagement {
    
    
    /// <summary>
    /// student_managment
    /// </summary>
    public partial class student_managment : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\student_managment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid datagrid;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\student_managment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cin_in;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\student_managment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nom_in;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\student_managment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox prenom_in;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\student_managment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox adresse_in;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\student_managment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox classe_in;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\student_managment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Calendar date_in;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\student_managment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ajouter_btn;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\student_managment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button supprimer_btn;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\student_managment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button search_btn;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\student_managment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button update_btn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.12.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/StudentManagement;component/student_managment.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\student_managment.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.12.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\student_managment.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.return_btn);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 11 "..\..\..\student_managment.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.logout_btn);
            
            #line default
            #line hidden
            return;
            case 3:
            this.datagrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.cin_in = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.nom_in = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.prenom_in = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.adresse_in = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.classe_in = ((System.Windows.Controls.ComboBox)(target));
            
            #line 23 "..\..\..\student_managment.xaml"
            this.classe_in.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.classe_in_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.date_in = ((System.Windows.Controls.Calendar)(target));
            return;
            case 10:
            this.ajouter_btn = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\student_managment.xaml"
            this.ajouter_btn.Click += new System.Windows.RoutedEventHandler(this.ajouter_btn_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.supprimer_btn = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\student_managment.xaml"
            this.supprimer_btn.Click += new System.Windows.RoutedEventHandler(this.supprimer_btn_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.search_btn = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\student_managment.xaml"
            this.search_btn.Click += new System.Windows.RoutedEventHandler(this.search_btn_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.update_btn = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\student_managment.xaml"
            this.update_btn.Click += new System.Windows.RoutedEventHandler(this.update_btn_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 29 "..\..\..\student_managment.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.logout_btn);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

