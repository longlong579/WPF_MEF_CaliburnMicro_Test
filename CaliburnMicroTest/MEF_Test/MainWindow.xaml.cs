using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using HZZG.Common.Tolls;
using MEF_Exported.Constract;
using log4net;
using System.Reflection;

namespace MEF_Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// 
    /// </summary>
    public partial class MainWindow : Window
    { 
        // 有Import则必须要有对应的CompositionContainer
        private CompositionContainer _container;
        /// <summary>
       
        [Import]
        public ILog log;

        [Import]
        public ICalculator calculator;

        [Import]
        public IAniamalSound amimalSound;

        public MainWindow()
        {
            InitMEF();
            WPFConsoleManager.Show();//开启Console窗口
            InitializeComponent();
            String s;
            log.Warn("我是MEF的Log测试");

            amimalSound.call("猫叫");
           // Console.WriteLine(amimalSound.cal.Calculate("1+4"));//不建议这么用(实现类要加public),建议用构造函数注入
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
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MainWindow).Assembly));//本地可执行文件
            catalog.Catalogs.Add(new DirectoryCatalog(@"D:\XHLProjects\WPF_MEF_CaliburnMicro_Test\CaliburnMicroTest\MEF_Exported\bin\Debug\"));//Exported文件夹下

            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                this._container.ComposeParts(this);
                this.DownloadAssemblies(catalog);//远程下载更新程序集到catalog
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        #region 自动下载更新
        private void DownloadAssemblies(AggregateCatalog catalog)
        {
            //asynchronously downloads assemblies and calls AddAssemblies
        }

        //之所以采用这种方式进行添加，是为了一次性完成重新组合，而不是反复进行（在直接添加程序集目录时会出现这种情况）。
        private void AddAssemblies(Assembly[] assemblies, AggregateCatalog catalog)
        {
            var assemblyCatalogs = new AggregateCatalog();
            foreach (Assembly assembly in assemblies)
                assemblyCatalogs.Catalogs.Add(new AssemblyCatalog(assembly));
            catalog.Catalogs.Add(assemblyCatalogs);
        }
        #endregion
    }
}
