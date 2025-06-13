using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class People
    {
        public string CodeName { get; set; }
        public string Name { get; set; }

        public People(string name)
        {
            this.Name = name;
            this.CodeName = GenerateCodeName(name);
        }

        private string GenerateCodeName(string input)
        {
            // Example: take the name, remove spaces, add random 5 digits
            string baseCode = new string(input.Where(char.IsLetterOrDigit).ToArray()).ToLower();
            string suffix = new Random().Next(10000, 99999).ToString();
            return baseCode + suffix;
        }
    }
}

