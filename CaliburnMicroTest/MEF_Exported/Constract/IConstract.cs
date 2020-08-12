using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF_Exported.Constract
{
    /// <summary>
    ///程序需要的契约单独存放 
    /// </summary>
    /// 

    #region 计算契约
    public interface ICalculator
    {
        String Calculate(String input);
    }

    public interface IOperation
    {
        int Operate(int left, int right);
    }

    public interface IOperationData
    {
        Char Symbol { get; }
    }
    #endregion

    #region 对象注入
    public interface IAniamalSound
    {
        void call(string sound);
    }
    #endregion
}
