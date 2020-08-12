using Caliburn.Micro;
using CaliburnTest.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// NameSpace : EdenGUI.ViewModels
// FileName : AppViewModel
//
// Created by : XHL at 2020/8/7 22:10:55
//
// Function : 
//
//========================================================================
namespace CaliburnTest.ViewModels
{
    [Export(typeof(ColorViewModel))]
    public class ColorViewModel:Screen
    {
        private readonly IEventAggregator _events;
        public ColorViewModel()
        { }
        [ImportingConstructor]
        public ColorViewModel(IEventAggregator events)
        {
            _events = events;
           
        }

        public void Red()
        {
            _events.PublishOnUIThread(new ColorEvent(new SolidColorBrush(Colors.Red)));
            //_events.Publish(new ColorEvent(new SolidColorBrush(Colors.Red)), action => {
            //    Task.Factory.StartNew(action);
            //});
        }

        public void Green()
        {
             _events.PublishOnUIThread(new ColorEvent(new SolidColorBrush(Colors.Green)));
            //_events.Publish(new ColorEvent(new SolidColorBrush(Colors.Green)), action =>
            //{
            //    Task.Factory.StartNew(action);
            //});
        }

        public void Blue()
        {
            _events.Publish(new ColorEvent(new SolidColorBrush(Colors.Blue)), action => {
                Task.Factory.StartNew(action);
            });
        }
    }
}
