using System;

namespace SimpleMapper
{
    /// <summary>
    /// SimpleNameAttribute
    /// <author>mango</author>>
    /// </summary>
    public class SimpleNameAttribute : Attribute
    {
        public string Name { get; set; }
        public SimpleNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
