using SimpleMapper;

namespace TestSimpleMapper.Dto
{
    /// <summary>
    /// StudentTableA
    /// <author>mango</author>
    /// </summary>
    class StudentTableA
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
        [SimpleName("Age")]
        public int Age1 { get; set; }
        /// <summary>
        /// Student
        /// </summary>
        public StudentTableA Student { get; set; }
    }
}
