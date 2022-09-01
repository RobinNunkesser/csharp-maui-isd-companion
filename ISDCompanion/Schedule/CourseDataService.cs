using System;
using System.Collections.Generic;
using System.Linq;

namespace ISDCompanion
{
    public static class CourseDataService
    {

        public static List<CourseViewModel> Courses { get; } = new List<CourseViewModel>
            {
            new CourseViewModel { Name = "Mathematik II VL",
                                  Room = "Detail 1",
                                    StartDate = "31.08.2022 08:00",
            Length = 180, Lecturer = "Bubu", Semester=3},
            new CourseViewModel { Name = "Banana", Room = "Detail 2", Lecturer = "Buba" },
            new CourseViewModel { Name = "Laptop", Room = "Detail 3", Lecturer = "Bubi" },
            new CourseViewModel { Name = "Teddy Bear", Room = "Detail 4", Lecturer = "Bub" }
            };

        public static List<CourseViewModel> GetSearchResults(string queryString)
        {

            var normalizedQuery = queryString?.ToLower() ?? "";
            if (string.IsNullOrEmpty(normalizedQuery)) return Courses;
            bool success = int.TryParse(normalizedQuery, out int semester);
            if (success)
            {
                return Courses.Where(item => item.Semester == semester).ToList();
            }
            return Courses.Where(
                item => item.Name.ToLower().Contains(normalizedQuery) ||
                item.Lecturer.ToLower().Contains(normalizedQuery)).ToList();
        }
    }
}

