﻿#pragma checksum "C:\Users\Admin\source\repos\FormStudent\FormStudent\View\LoginAccount.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "41A39D2B281EBA99C4C681C5F313ED54"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FormStudent.View
{
    partial class LoginAccount : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // View\LoginAccount.xaml line 13
                {
                    this.Email = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 3: // View\LoginAccount.xaml line 14
                {
                    this.Password = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                    ((global::Windows.UI.Xaml.Controls.PasswordBox)this.Password).KeyDown += this.password_validate;
                }
                break;
            case 4: // View\LoginAccount.xaml line 15
                {
                    this.Login_Success = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5: // View\LoginAccount.xaml line 16
                {
                    this.Login_Error = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6: // View\LoginAccount.xaml line 18
                {
                    this.btn_submit = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_submit).Click += this.Submit_Form;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

