//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Life
    {
        public int lifeid { get; set; }
        public int typeid { get; set; }
        public string title { get; set; }
        public string lifecontent { get; set; }
        public System.DateTime addtime { get; set; }
        public int admid { get; set; }
        public string cover { get; set; }
        public int state { get; set; }
    
        public virtual Administrator Administrator { get; set; }
        public virtual LifeType LifeType { get; set; }
    }
}
