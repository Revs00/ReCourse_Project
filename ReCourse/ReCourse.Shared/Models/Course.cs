using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCourse.Shared.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationMinutes { get; set; } = 60;
        public decimal Price { get; set; } = 0m;
        public string Level { get; set; } = "Beginner"; // Beginner|Intermediate|Advanced
        public string? ThumbnailFileName { get; set; } = string.Empty;
        public int TrainerId { get; set; }


        public Trainer? Trainer { get; set; }
    }
}
