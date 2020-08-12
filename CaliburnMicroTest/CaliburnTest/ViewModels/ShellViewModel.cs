using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// NameSpace : CaliburnTest.ViewModels
// FileName : PViewModel
//
// Created by : XHL at 2020/6/30 17:16:46
//
// Function : 
//
//========================================================================
namespace CaliburnTest.ViewModels
{
    public class ShellViewModel:Screen,INotifyPropertyChanged
    {
        private double _left;
        private double _right;
        private double _result;

        public double Left
        {
            get { return _left; }
            set
            {
                _left = value;
                NotifyOfPropertyChange();
            }
        }

        public double Right
        {
            get { return _right; }
            set
            {
                _right = value;
                NotifyOfPropertyChange();
            }
        }

        public double Result
        {
            get { return _result; }
            set
            {
                _result = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
