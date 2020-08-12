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
    [Export(typeof(AppViewModel))]
    public class AppViewModel:Screen, IHandle<ColorEvent>
    {
        public AppViewModel()
        { }
        [ImportingConstructor]
        public AppViewModel(ColorViewModel colorModel, IEventAggregator events)
        {
            ColorModel = colorModel;
            events.Subscribe(this);
        }

        public ColorViewModel ColorModel { get; private set; }

        private SolidColorBrush _Color;
        public SolidColorBrush Color
        {
            get { return _Color; }
            set
            {
                _Color = value;
                NotifyOfPropertyChange(() => Color);
            }
        }

        public void Handle(ColorEvent message)
        {
            Color = message.Color;
        }
    }
}
