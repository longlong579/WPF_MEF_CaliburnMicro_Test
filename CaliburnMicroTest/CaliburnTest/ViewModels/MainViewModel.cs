using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Windows;
using EdenGUI.Views;

namespace CaliburnTest.ViewModels
{
 
    [Export(typeof(MainViewModel))]
    public class MainViewModel:Screen
    {
        private readonly IWindowManager _windowManager;
        int count=1;

        [ImportingConstructor]
        public MainViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                NotifyOfPropertyChange(() => Count);
                NotifyOfPropertyChange(() => CanIncrementCount);
            }
        }

        public bool CanIncrementCount
        {
            get { return count<200; }
        }

        public void IncrementCount(int Delta)
        {
            Count += Delta;
            //MessageBox.Show(string.Format("Hello {0}{1}{2}!", Name,a,b)); //Don't do this in real life :)
        }

        public void ShowAppView()
        {
            _windowManager.ShowWindow(new AppViewModel());
        }
    }
}
