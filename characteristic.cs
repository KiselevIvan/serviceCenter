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
    
    public partial class characteristic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public characteristic()
        {
            this.moduleCharacteristics = new HashSet<moduleCharacteristic>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public int unitId { get; set; }
    
        public virtual unit unit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<moduleCharacteristic> moduleCharacteristics { get; set; }
    }
}