﻿using System.Windows.Controls;
using UsingReflection.Entities;

namespace UsingReflection.UserControls
{
    public partial class UserControlForStringParameter : UserControl
    {
        public string ParameterValue
        {
            get
            {
                return txbTextBox.Text;
            }
        }

        public UserControlForStringParameter()
        {
            InitializeComponent();
        }

        public UserControlForStringParameter(MyParameterInfo parameter)
            : this()
        {
            lblParameterName.Text = parameter.ParameterName;
        }
    }
}
