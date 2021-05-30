using SimpleMapper;

namespace TestSimpleMapper.Dto
{
    /// <summary>
    /// StudentTableB
    /// <author>mango</author>
    /// </summary>
    class StudentTableB
    {
        // id
        public int Id;
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Age
        /// </summary>
        [SimpleName("Age1")]
        public int Age { get; set; }
        /// <summary>
        /// Gender
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// Student
        /// </summary>
        public StudentTableA Student { get; set; }
    }
}
