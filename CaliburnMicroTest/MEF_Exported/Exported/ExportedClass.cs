using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZZG.Common.Tolls;
using MEF_Exported.Constract;
using MEF_Test.Log;
using log4net;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using MEF_Exported.Attribute;
namespace MEF_Test
{
    /// <summary>
    /// 导出提供的是服务，而导入则使用或者说接入这些服务
    /// 由此提供程序所需的计算服务，以后更新，只需要更新此文件，程序无需重新编译
    /// </summary>
    /// 

    /// <summary>
    /// Log4Net 第3方的使用方法
    /// </summary>
    class LoggerPart
    {
        public LoggerPart()
        {
            WPFConsoleManager.Show();
            Console.WriteLine("LoggerPart Created");
        }
        [Export]
        public ILog Logger
        {
            get { return Log.LogManager.Instance.Logger; }
        }
    }

    #region 计算服务
    [ExportOperate(OperateType =OperateTypes.Add)]
    class Add : IOperation
    {
        //构造函数此次至是用来做Debug测试
        public Add()
        {
            WPFConsoleManager.Show();
            Console.WriteLine("Add Created");
        }
        public int Operate(int left, int right)
        {
            return left + right;
        }
    }

    [ExportOperate(OperateType = OperateTypes.Sub)]
    class Subtract : IOperation
    {
        public Subtract()
        {
            WPFConsoleManager.Show();
            Console.WriteLine("Subtract Created");
        }
        public int Operate(int left, int right)
        {
            return left - right;
        }
    }



    [Export(typeof(ICalculator))]
     class MySimpleCalculator : ICalculator
    {
        public MySimpleCalculator()
        {
            WPFConsoleManager.Show();
            Console.WriteLine("MySimpleCalculator Created");
        }

        /// <summary>
        /// MEF 提供了多个目录，其中有两个目录可重新组合。
        /// DirectoryCatalog 是可通过调用其 Refresh 方法来重新组合的目录。
        /// AggregateCatalog，这是目录的目录。您可使用 Catalogs 集合属性向该目录添加目录，这会启动重新组合。
        /// </summary>
        [ImportMany(AllowRecomposition = true)]//AllowRecomposition = true 允许重新组合,适用远程下载更新
        IEnumerable<Lazy<IOperation, IOperateMetadata>> operations;

        public String Calculate(String input)
        {
            int left;
            int right;
            string operation;
            int fn = FindFirstNonDigit(input); //finds the operator
            if (fn < 0) return "Could not parse command.";

            try
            {
                //separate out the operands
                left = int.Parse(input.Substring(0, fn));
                right = int.Parse(input.Substring(fn + 1));
            }
            catch
            {
                return "Could not parse command.";
            }

            operation = input[fn].ToString();

            foreach (Lazy<IOperation, IOperateMetadata> i in operations)
            {
                if (i.Metadata.OperateType.GetDescription().Equals(operation)) return i.Value.Operate(left, right).ToString();
            }
            return "Operation Not Found!";
        }

        private int FindFirstNonDigit(String s)
        {

            for (int i = 0; i < s.Length; i++)
            {
                if (!(Char.IsDigit(s[i]))) return i;
            }
            return -1;
        }
    }

    #endregion

    #region 对象注入
    [Export(typeof(IAniamalSound))]
    class AnimalSound : IAniamalSound
    {
        //属性注入
        [Import]
        public ICalculator cal { get; set; }

        // 构造函数注入，优先级比默认高（默认构造函数不调用）
        public ILog log;      
        [ImportingConstructor]
        public AnimalSound(ILog log)
        {
            this.log = log;
            WPFConsoleManager.Show();
            Console.WriteLine("AnimalSound Created");
            log.Info("AnimalSound Created");
        }

        public AnimalSound()
        {
            WPFConsoleManager.Show();
            Console.WriteLine("AnimalSound222 Created");
        }

        public void call(string sound)
        {
            Console.WriteLine("AnimalSound:" + sound);
        }
     
    }
  
    #endregion
}
