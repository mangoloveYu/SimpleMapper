using SimpleMapper;
using System;

namespace TestSimpleMapper
{
    public class TestSimpleMapper
    {
        public void TestMapper()
        {
            try
            {
                var studentA = new StudentA()
                {
                    Id = 1,
                    Age1 = 20,
                    Name = "mango"
                };

                var studentB = SimpleMapper<StudentA, StudentB>.Map(studentA);
                Console.WriteLine($"Id = {studentB.Id} Name = {studentB.Name} Age = {studentB.Age} Gender = {studentB.Gender}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }


    class StudentA
    {
        public int Id;
        public string Name { get; set; }

        [SimpleName("Age")]
        public int Age1 { get; set; }

    }

    class StudentB
    {
        public int Id;
        public string Name { get; set; }

        [SimpleName("Age1")]
        public int Age { get; set; }

        public string Gender { get; set; }
    }
}
