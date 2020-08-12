using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// NameSpace : EdenGUI.Messages
// FileName : ColorEvent
//
// Created by : XHL at 2020/8/7 22:32:48
//
// Function : 
//
//========================================================================
namespace CaliburnTest.Messages
{
    public class ColorEvent
    {
        public ColorEvent(SolidColorBrush color)
        {
            Color = color;
        }

        public SolidColorBrush Color { get; private set; }
    }
}
