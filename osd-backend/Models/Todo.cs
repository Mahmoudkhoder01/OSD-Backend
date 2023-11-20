using IDO;
using System;

namespace osd_backend.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime? DueDate { get; set; }
        public string Estimate { get; set; }
        public string Importance { get; set; }
        public string Status { get; set; }

        // Foreign key and navigation property
        public int UserId { get; set; }

        // Enums
        public enum ImportanceEnum
        {
            Low,
            Medium,
            High
        }

        public enum StatusEnum
        {
            Todo,
            Doing,
            Done
        }
    }
}
