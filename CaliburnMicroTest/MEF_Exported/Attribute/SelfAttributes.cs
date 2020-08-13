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
    //俗称：“元数据视图”的接口，元数据视图中有且只能定义只读的属性
    //[DefaultValue]给属性默认值，元数据视图中定义的所有属性在使用时都必须赋值
    public interface IOperateMetadata
    {
        [DefaultValue(OperateTypes.Add)]//指定后就成可选属性,未加则为必选属性(即：ExportOperateAttribute必须提供是属性，不提供则找不到对应关系)
        OperateTypes OperateType { get; }
        //[DefaultValue("+")]//指定后就成可选属性
        string OperateTypeStr { get; }
    }
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportOperateAttribute : ExportAttribute
    {
        public ExportOperateAttribute()
        : base(typeof(IOperation))
        { }
        //因为OperateTypes被 [DefaultValue]修饰，变为可选属性
         public OperateTypes OperateType { get; set; }
        public string OperateTypeStr { get; set; }
    }
    #endregion
}
