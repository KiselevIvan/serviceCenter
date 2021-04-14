namespace serviceCenter
{
    using System;
    using System.Collections.Generic;
    
    public partial class requestedService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public requestedService()
        {
            this.cost = 0;
            this.servicesExecution = new HashSet<serviceExecution>();
        }
    
        public int Id { get; set; }
        public int cost { get; set; }
        public int clientDeviceId { get; set; }
        public int serviceId { get; set; }
        public string description { get; set; }
        public int stageOfImplementationId { get; set; }
    
        public virtual clientDevice clientDevice { get; set; }
        public virtual service service { get; set; }
        public virtual stageOfImplementation stageOfImplementation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<serviceExecution> servicesExecution { get; set; }
    }
}
