using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lesson5
{
    class Program
    {
        // Ученик - Андрей Марачковский
        #region Task01
        static void Task01a()
        {
            int k = 3;
            bool result = false;
            bool isError = false;
            Console.Clear();
            do
            {
                Console.WriteLine($"Осталось попыток: {k}");
                Console.Write("Введите логин: ");
                string strLogin = Console.ReadLine();
                if (strLogin.Length == 0)
                {
                    Console.WriteLine("Пустая строка!");
                    isError = true;
                    k--;
                    continue;
                }

                if (strLogin.Length >= 2 && strLogin.Length <= 10)
                {
                    var a = strLogin.ToCharArray();
                    for (int i = 0; i < a.Length; i++)
                    {
                        var category = char.GetUnicodeCategory(a[i]);
                        if (i == 0 && category == UnicodeCategory.DecimalDigitNumber)
                        {
                            Console.WriteLine("Нельзя использовать число первым!");
                            isError = true;
                            break;
                        }
                        else
                        {   
                            if (category != UnicodeCategory.UppercaseLetter && category != UnicodeCategory.LowercaseLetter && category != UnicodeCategory.DecimalDigitNumber)
                            {
                                Console.WriteLine("Недопустимый символ!");
                                isError = true;
                                break;
                            }
                        }
                    }
                }
                if (!isError)
                {
                    result = true;
                    break;
                }
                k--;
            }
            while (k > 0);
            if (result)
                Console.WriteLine("Доступ разрешен!");
            else
                Console.WriteLine("Доступ запрешен!");
            Console.ReadKey();
        }
        static void Task01b()
        {
            int k = 3;
            bool result = false;
            Console.Clear();
            do
            {
                Console.WriteLine($"Осталось попыток: {k}");
                Console.Write("Введите логин: ");
                string strLogin = Console.ReadLine();
                var myRegex = new Regex(@"^[^0-9][A-Za-z0-9]{2,10}$");
                if (myRegex.IsMatch(strLogin))
                {
                    result = true;
                    break;
                }
                k--;
            }
            while (k > 0);
            if (result)
                Console.WriteLine("Доступ разрешен!");
            else
                Console.WriteLine("Доступ запрешен!");
            Console.ReadKey();
        }
        static void Task01()
        {
            Task01a();
            Task01b();
        }
        #endregion

        #region Task02
        class Message
        {
            private string _message;
            private int _count;
            private string _bukva;
            public Message(string message, int count)
            {
                _message = message;
                _count = count;
            }
            public Message(string message, string bukva)
            {
                _message = message;
                _bukva = bukva;
            }
            public Message(string message)
            {
                _message = message;
            }
            public string words()
            {
                string[] separators = { ",", ".", " ", "?", "!", ";", ":" };
                var result = new StringBuilder();
                var words = _message.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Length <= _count)
                        result.Append(words[i] + " ");
                }
                return result.ToString();
            }
            public string text()
            {
                string[] separators = { ",", ".", " ", "?", "!", ";", ":" };
                var result = new StringBuilder();
                var words = _message.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i][words[i].Length - 1].ToString() != _bukva)
                        result.Append(words[i] + " ");
                }
                return result.ToString();
            }
            public string maxLength()
            {
                string[] separators = { ",", ".", " ", "?", "!", ";", ":" };
                var words = _message.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string result = "";
                int length = 0;
                for (int i = 0; i < words.Length; i++)
                {
                    var tekLength = words[i].Length;
                    if (tekLength > length)
                    {
                        result = words[i];
                    }
                }
                return result;
            }
            public string maxLengthwords()
            {
                string[] separators = { ",", ".", " ", "?", "!", ";", ":" };
                var words = _message.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                var result = new StringBuilder(" ");
                int length = 0;
                for (int i = 0; i < words.Length; i++)
                {
                    var tekLength = words[i].Length;
                    if (tekLength > length)
                    {
                        result.Replace(result.ToString(), words[i] + " ");
                        length = tekLength;
                    }
                    else
                    if (tekLength == length)
                    {
                        result.Append(words[i] + " ");
                        length = tekLength;
                    }
                }
                return result.ToString();
            }
        }
        static void Task02a()
        {
            Console.Clear();
            Console.WriteLine("Введите текст!");
            string strText = Console.ReadLine();
            Console.Write("Введите целое число: ");
            string strNumber = Console.ReadLine();
            int number = Convert.ToInt32(strNumber);
            var text = new StringBuilder(strText);
            Console.WriteLine($"Слова содержащие {number} букв");
            var str = new Message(strText, number);
            Console.WriteLine(str.words());
            Console.ReadKey();
        }
        static void Task02b()
        {
            Console.Clear();
            Console.WriteLine("Введите текст!");
            string strText = Console.ReadLine();
            Console.Write("Введите букву: ");
            string strNumber = Console.ReadLine();
            var text = new StringBuilder(strText);
            Console.WriteLine($"Удалены слова содержащие букву {strNumber}");
            var str = new Message(strText, strNumber);
            Console.WriteLine(str.text());
            Console.ReadKey();
        }
        static void Task02c()
        {
            Console.Clear();
            Console.WriteLine("Введите текст!");
            string strText = Console.ReadLine();
            var text = new StringBuilder(strText);
            Console.WriteLine("Самое длинное слово сообщения!");
            var str = new Message(strText);
            Console.WriteLine(str.maxLength());
            Console.ReadKey();
        }
        static void Task02d()
        {
            Console.Clear();
            Console.WriteLine("Введите текст!");
            string strText = Console.ReadLine();
            var text = new StringBuilder(strText);
            Console.WriteLine("Самые длинные слова в сообщении!");
            var str = new Message(strText);
            Console.WriteLine(str.maxLengthwords());
            Console.ReadKey();
        }
        static void Task02()
        {
            Task02a();
            Task02b();
            Task02c();
            Task02d();
        }
        #endregion

        #region Task03
        static void Task03 ()
        {
            Console.Clear();
            Console.Write("Введите первую строку: ");
            string str1 = Console.ReadLine();
            Console.Write("Введите вторую строку: ");
            string str2 = Console.ReadLine();



            string[] separators = { ",", ".", " ", "?", "!", ";", ":" };
            var words1 = str1.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var words2 = str2.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            if (words1.Length == words2.Length) // проверка длины массив предложений
            {
                bool isMatch = false;
                for (int i = 0; i < words1.Length; i++) // массив слов
                {
                    var w1 = words1[i]; // первое слово в массиве
                    var w2 = words2[i]; // второе слово в массиве
                    if (w1.Length == w2.Length)
                    {
                        for (int z = 0; z < w1.Length; z++)
                        {
                            for (int j = 0; j < w2.Length; j++)
                            {
                                if (w1[z] == w2[j])
                                {
                                    w2 = w2.Remove(j, 1);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Указанные строки НЕ являются подстановкой одна другой!");
                        break;
                    }
                    if (w2.Length == 0)
                    {
                        isMatch = true;
                    }
                    else
                    {
                        isMatch = false;
                    }
                }
                if (isMatch)
                {
                    Console.WriteLine("Указанные строки ЯВЛЯЕТСЯ подстановкой одна другой!");
                }
                else
                {
                    Console.WriteLine("Указанные строки НЕ ЯВЛЯЕТСЯ подстановкой одна другой!");
                }
            }
            else
            {
                Console.WriteLine("Указанные строки НЕ являются подстановкой одна другой!");
            }

            Console.ReadKey();
        }
        #endregion
        static void Main(string[] args)
        {
            bool isExit = false;
            do
            {
                int number;
                Console.Clear();
                Console.Write("Введите номер задания (1-3), либо число 0 для выхода: ");
                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    number = 0;
                }
                switch (number)
                {
                    case 0:
                        isExit = true;
                        break;
                    case 1:
                        Console.Clear();
                        Task01();
                        break;
                    case 2:
                        Console.Clear();
                        Task02();
                        break;
                    case 3:
                        Console.Clear();
                        Task03();
                        break;
                }
            }
            while (!isExit);
        }
    }
}
