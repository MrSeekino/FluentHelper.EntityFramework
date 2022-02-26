using System;

namespace FluentHelper.EntityFramework.Examples.Models
{
    public class TestData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }
    }
}
