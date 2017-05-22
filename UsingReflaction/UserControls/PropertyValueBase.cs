﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UsingReflection.TestEntities;

namespace UsingReflection.UserControls
{
    public class PropertyValueBase: UserControl
    {
        public string NameAbr { get; set; }

        public string TypeFullName { get; set; }

        private MemberInfo memberInformation;

        public MemberInfo MemberInformation
        {
            set
            {
                memberInformation = value;
                FieldInfo fieldInfo = null;
                PropertyInfo propertyInfo = null;
                if (memberInformation is FieldInfo)
                {
                    fieldInfo = memberInformation as FieldInfo;
                    FieldInformation = fieldInfo;
                    TypeFullName = fieldInfo.FieldType.FullName;
                    NameAbr = fieldInfo.Name;
                }
                else
                {
                    if (memberInformation is PropertyInfo)
                    {
                        propertyInfo = memberInformation as PropertyInfo;
                        PropertyInformation = propertyInfo;
                        TypeFullName = propertyInfo.PropertyType.FullName;
                        NameAbr = propertyInfo.Name;
                    }
                }
            }
            get
            {
                return memberInformation;
            }
        }

        public FieldInfo FieldInformation { get; set; }
        public PropertyInfo PropertyInformation { get; set; }
        public StackPanel StackPanelInfo { get; set; }
    }
}
