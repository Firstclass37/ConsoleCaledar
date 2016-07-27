using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCaledar
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                try
                {
                    Console.Clear();
                    Console.Write("Input your date: ");
                    string dateString = Console.ReadLine();
                    var targetDate = Program.DateParse(dateString);
                    Show(targetDate);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    Console.ReadKey();
                }
            } while (ConfermDialog());

        }

        static DateTime DateParse(string dateString)
        {
            var result = DateTime.Parse(dateString);
            if (result.Year < 1600 || result.Year > 2400)
            {
                throw new ArgumentException("Years range must be from 1600 till 2400");
            }
            return result;
        }

        static void Show(DateTime inputDate)
        {
            var daysCount = DateTime.DaysInMonth(inputDate.Year,inputDate.Month);
            var daysArray = GetDaysArray(daysCount);
            for (int dayOfWeek = 1; dayOfWeek < 8; dayOfWeek++)
            {    
                     
                DayOfWeek currentWeekDay = dayOfWeek == 7? DayOfWeek.Sunday : (DayOfWeek)dayOfWeek;
                
                var days = daysArray
                    .Where(d =>  new DateTime(inputDate.Year, inputDate.Month,d ).DayOfWeek == currentWeekDay)
                    .ToArray();
                ShowWeekDay(currentWeekDay,inputDate.Day,days);
            }

        }

        static void ShowWeekDay(DayOfWeek dayOfWeek,int targetDay, int[] days)
        {
            string resultString = dayOfWeek.ToString().ToLower().Substring(0, 3);
            if (days.Length == 4)
            {
                resultString += new string(' ',5);
            }

            for (int i = 0; i < days.Length; i++)
            {
                resultString += " ";
                string dayString = days[i].ToString().Length == 1 ? " " + days[i].ToString() : days[i].ToString();
                if (days[i] != targetDay)
                {
                    dayString = " " + dayString + " ";
                }
                else
                {
                    dayString = String.Format("[{0}]",dayString);
                }
                resultString += dayString;

            }
            resultString = resultString.Remove(resultString.Length-1,1);
            Console.WriteLine(resultString);

        }

        static int[] GetDaysArray(int daysCount)
        {
            int[] result = new int[daysCount];
            for (int i = 0; i < daysCount; i++)
            {
                result[i] = i + 1;
            }
            return result;
            
        }

        static bool ConfermDialog()
        {
            Console.WriteLine("Continue? (y/n)");
            char answer = Console.ReadKey().KeyChar;
            return answer.Equals('y');
        }
    }
}
