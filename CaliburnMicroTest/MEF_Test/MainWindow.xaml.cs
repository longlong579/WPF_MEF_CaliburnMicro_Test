using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using HZZG.Common.Tolls;
namespace MEF_Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
       
        private CompositionContainer _container;

        [Import(typeof(ICalculator))]
        public ICalculator calculator;

        public MainWindow()
        {
            InitMEF();
            WPFConsoleManager.Show();//开启Console窗口
            InitializeComponent();
            String s;
            Console.WriteLine("Enter Command:");
            while (true)
            {
                s = Console.ReadLine();
                Console.WriteLine(this.calculator.Calculate(s));
            }
        }

        private void InitMEF()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MainWindow).Assembly));
          //  catalog.Catalogs.Add(new DirectoryCatalog("C:\\Users\\SomeUser\\Documents\\Visual Studio 2010\\Projects\\SimpleCalculator3\\SimpleCalculator3\\Extensions"));


            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }
    }
}
