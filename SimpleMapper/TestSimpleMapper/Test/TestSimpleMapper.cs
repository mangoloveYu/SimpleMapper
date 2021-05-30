using SimpleMapper;
using System;
using System.Collections.Generic;
using TestSimpleMapper.Dto;

namespace TestSimpleMapper
{
    /// <summary>
    /// TestSimpleMapper
    /// <author>mango</author>
    /// </summary>
    public class TestSimpleMapper
    {
        public void TestMapper()
        {
            try
            {
                //#######################Map#########################
                var studentA = new StudentTableA()
                {
                    Id = 1,
                    Age1 = 20,
                    Name = "mango",
                    Student = new StudentTableA() { Id = 22, Name = "li", Age1 = 22 }
                };
                var studentB = SimpleMapper<StudentTableA, StudentTableB>.Map(studentA);
                //构造函数批量映射
                var profiles = new SimpleProfiles<StudentTableA, StudentDto>(new Dictionary<string, string>()
                {
                    { "Name", "StudentName" }
                });
                //
                profiles.CreateMap("Name", "StudentName1");
                var studentDto = SimpleMapper<StudentTableA, StudentDto>.Map(studentA);

                //#######################MapList#########################
                var studentAList = new List<StudentTableA>()
                {
                    new StudentTableA(){Id = 1,Age1 = 20,Name = "mango"},
                    new StudentTableA(){Id = 2,Age1 = 18,Name = "sum"}
                };
                var studentBList = SimpleMapper<StudentTableA, StudentTableB>.MapList(studentAList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
