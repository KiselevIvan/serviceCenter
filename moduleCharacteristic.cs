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
    
    public partial class moduleCharacteristic
    {
        public int Id { get; set; }
        public int characteristicId { get; set; }
        public int moduleId { get; set; }
        public string Value { get; set; }
    
        public virtual characteristic characteristic { get; set; }
        public virtual module module { get; set; }
    }
}
