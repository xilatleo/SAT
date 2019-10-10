using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.DATA.EF/*.Metadata*/
{
    #region Students
    public class StudentMetadata
    {
        [Required]

        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        [Required]
        public Nullable<int> SSID { get; set; }
        [Required]
        public string Major { get; set; }
    }
    [MetadataType(typeof(StudentMetadata))]
    public partial class Student
    {
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
    #endregion

    #region ScheduledClasses
    public class ScheduledClassesMetadata
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public System.DateTime StartDate { get; set; }
        [Required]
        public System.DateTime EndDate { get; set; }
        [Required]
        public string InstructorName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int SCID { get; set; }
    }
    [MetadataType(typeof(ScheduledClassesMetadata))]
    public partial class ScheduledClasses
    {
        public string QuickPick
        {
            get { return StartDate + "-" + CourseId + "-" + Location; }
        }
    }
    #endregion

    public class CoursesMetadata
    {
        [Required]
        [Display(Name ="Course Name")]
        public string CourseName { get; set; }
        [Required]
        [Display(Name = "Course Description")]
        public string CourseDescription { get; set; }
        [Required]
        [Display(Name = "Credit Hours")]
        public byte CreditHours { get; set; }
        public string Curriculum { get; set; }
        public string Notes { get; set; }
        public bool isActive { get; set; }
    }
    [MetadataType(typeof(CoursesMetadata))]
    public partial class Courses
    {

    }

}
