using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.DATA.EF/*.Metadata*/
{
    #region Student Statues
    public class StudentStatusesMetadata
    {
        [Required]
        public int SSID { get; set; }
        [Required]
        [Display(Name ="Name")]
        public string SSName { get; set; }
        [Required]
        [Display(Name ="Description")]
        public string SSDescription { get; set; }
    }
    [MetadataType(typeof(StudentStatusesMetadata))]
    public partial class StudentStatuses
    {

    }
    #endregion

    #region Students
    public class StudentMetadata
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Display(Name ="Student Photo")]
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
        [Display(Name = "Course ID")]
        public int CourseId { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", NullDisplayText = "[N/A]")]
        [Display(Name ="Start Date")]
        public System.DateTime StartDate { get; set; }
        [Required]
        [Display(Name ="End Date")]
        [DisplayFormat(DataFormatString = "{0:d}", NullDisplayText = "[N/A]")]
        public System.DateTime EndDate { get; set; }
        [Required]
        [Display(Name ="Instructor's Name")]
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
            get { return StartDate + "-" + Cours.CourseName + "-" + Location; }
        }
    }
    #endregion

    #region Enrollment
    public class EnrollmentMetadata
    {
        [Required]
        public int EnrollmentId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int ScheduledClassId { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", NullDisplayText = "[N/A]")]
        [Display(Name ="Enrollment Date")]
        public System.DateTime EnrollmentDate { get; set; }
    }
    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollments
    {

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
        [Display(Name = "Active Course")]
        public bool isActive { get; set; }
    }
    [MetadataType(typeof(CoursesMetadata))]
    public partial class Courses
    {

    }

}
