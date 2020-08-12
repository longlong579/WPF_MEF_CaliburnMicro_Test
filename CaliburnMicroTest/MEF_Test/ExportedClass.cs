using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZZG.Common.Tolls;

namespace MEF_Test
{
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

    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '+')]
    class Add : IOperation
    {
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

    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '-')]
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
        [ImportMany]
        IEnumerable<Lazy<IOperation, IOperationData>> operations;

        public String Calculate(String input)
        {
            int left;
            int right;
            Char operation;
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

            operation = input[fn];

            foreach (Lazy<IOperation, IOperationData> i in operations)
            {
                if (i.Metadata.Symbol.Equals(operation)) return i.Value.Operate(left, right).ToString();
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
}
