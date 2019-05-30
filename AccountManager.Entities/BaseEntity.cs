using System;
using System.ComponentModel;

namespace AccountManager.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }  
        [DefaultValue(true)]
        public bool Active { get; set; }
    }
}
