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
    /// <summary>
    /// 默认是单例 可以在Export上指定创建策略，也可以在[Import]上指定
    /// MEF的创建策略有：Shared（共享）和NonShared(非共享)1. 标记为Any的导出部件与Shared和NonShared的导入均能成功匹配；
    ///当导入导出都是用默认创建策略，或者都是用默认，MEF将默认创建策略为Shared
    ///2. 创建策略为Shared或NonShared的导出以及标记为Any的导入匹配成功；
    ///3. 创建策略为Shared的导出只能与创建策略为Shared和标记为Any匹配成功；
    ///4. 创建策略为NonShared的导出只能为Shared和标记为Any的导入匹配成功。
    ///导入部件  [Import(RequiredCreationPolicy = CreationPolicy.Shared)]
    ///导出部件[PartCreationPolicy(CreationPolicy.NonShared)]
    /// </summary>
    [ExportOperate(OperateTypeStr ="+")]
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

    [ExportOperate(OperateType =OperateTypes.Sub, OperateTypeStr ="-")]
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
                //if (i.Metadata.OperateType.GetDescription().Equals(operation)) return i.Value.Operate(left, right).ToString();
                if (i.Metadata.OperateTypeStr.Equals(operation)) return i.Value.Operate(left, right).ToString();

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

    #region 对象注入 传入参数可以在构造函数中调用，属性注入则必须等构造完毕才能用
    /// <summary>
    ///注意：若导出部件占用资源，继承IDisposable释放（容器释放或调用ReleaseExport时）
    ///如果部件实现了接口IPartImportsSatisfiedNotification ，当组合已完成并且部件的导入可供使用时，组合窗口将对部件调用接口中得方法OnImportsSatisfied。
    ///IPartImportsSatisfiedNotification 包含一个名为 OnImportsSatisfied 的方法。
    ///当组合已完成并且部件的导入可供使用时，组合窗口将对实现接口的任何部件调用此方法。
    ///部件是组合引擎创建，用于满足其他部件的导入。 在设置好部件的导入之前，
    ///您无法执行任何依赖于部件构造函数中的导入值或对这些值进行操作的初始化，
    ///除非已通过使用 ImportingConstructor 特性将这些指定为必备。 
    ///此方法通常为首选方法，但在某些情况下，构造函数注入可能不可用。 
    ///在这些情况下，可以在 OnImportsSatisfied 中执行初始化，并且部件应实现 IPartImportsSatisfiedNotification。
    /// </summary>
    [Export(ContractName.AnimaleOne, typeof( IAniamalSound))]
    class AnimalSound : IAniamalSound
    {
        // 构造函数注入，优先级比默认高（默认构造函数不调用）
        public ILog log;      
        [ImportingConstructor]
        public AnimalSound(ILog log)
        {
            this.log = log;
            WPFConsoleManager.Show();
            log.Info("AnimalSound Created");
        }

        public AnimalSound()
        {
            WPFConsoleManager.Show();
            Console.WriteLine("AnimalSound2 Created");
        }

        public void call(string sound)
        {
            Console.WriteLine("AnimalSound:" + sound);
        }
     
    }
    #endregion

    #region 属性注入
    [Export(ContractName.AnimaleTwo, typeof(IAniamalSound))]
    class AnimalSound2 : IAniamalSound,IPartImportsSatisfiedNotification
    {
        ////属性注入
        [Import]
        public ILog log { get; set; } 
            
        public AnimalSound2()
        {
            this.log = log;
            WPFConsoleManager.Show();
            Console.WriteLine("AnimalSound2 Created");
        }

        public void call(string sound)
        {
            Console.WriteLine("AnimalSound2:" + sound);
        }

        //会在构造完毕后回调
        public void OnImportsSatisfied()
        {
            log.Info($"AnimalSound2 属性注入 log回调");
        }
    }
    #endregion
}
