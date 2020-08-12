using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF_Exported.Constract;
using HZZG.Common.Tolls;
using System.ComponentModel;

namespace MEF_Exported.Attribute
{
    #region OperateAttribute
    public enum OperateTypes {
        [Description("+")]
        Add,
        [Description("-")]
        Sub,
    }
    public interface IOperateMetadata
    {
        OperateTypes OperateType { get; }
    }
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportOperateAttribute : ExportAttribute
    {
        public ExportOperateAttribute()
        : base(typeof(IOperation))
        { }

        public OperateTypes OperateType { get; set; }
    }
    #endregion
}
