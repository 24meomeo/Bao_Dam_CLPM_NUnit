using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Selenium
{
    class CheckCharaters
    {
        public bool CheckStrNumberCharacters(string str)
        {
            foreach (char c in str)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;

        }

        public bool CheckSpaceCharacters(string input)
        {
            if (input.StartsWith(" "))
            {
                return true;
            }
            return false;
        }

        public bool CheckFile(string input)
        {
            string[] file = input.Split('.');
            if (file[1] == "jpg" || file[1] == "png")
            {
                return false;
            }
            return true;

        }
        public bool ContainsSpecialCharactersForMail(string input) 
        {
            // Sử dụng biểu thức chính quy để kiểm tra xem chuỗi có chứa bất kỳ ký tự đặc biệt nào
            // trừ chuỗi "@gmail.com" hay không
            Regex regex = new Regex(@"[^a-zA-Z0-9_\.@]");

            // Kiểm tra xem chuỗi có khớp với biểu thức chính quy không
            return regex.IsMatch(input);
        }
        public bool ContainsStrCharacterAndNumber(string input)
        {
            // Sử dụng biểu thức chính quy để kiểm tra xem chuỗi có chứa bất kỳ ký tự đặc biệt nào
            Regex regex = new Regex(@"[^a-zA-Z0-9_]");

            // Kiểm tra xem chuỗi có khớp với biểu thức chính quy không
            return regex.IsMatch(input);
        }

    }
}
