//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace serviceCenter
{
    using System;
    using System.Collections.Generic;
    
    public partial class upgradeReplacement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public upgradeReplacement()
        {
            this.modulePrice = 0;
        }
    
        public int Id { get; set; }
        public int modulePrice { get; set; }
        public int moduleId { get; set; }
        public string description { get; set; }
        public int clientDeviceId { get; set; }
        public int employeeId { get; set; }
    
        public virtual module module { get; set; }
        public virtual clientDevice clientDevice { get; set; }
        public virtual employee employee { get; set; }
    }
}