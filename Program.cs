using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            String name;
            String surname;
            List<int> homeWorkMarks = new List<int>();
            int examMark;
            int homeWorkLength;
            List<Students> students = new List<Students>();
            String choise;
        Start:
            Console.WriteLine("1.Suvesti mokinius");
            Console.WriteLine("2.Išvesti mokinių rezultatus");
            Console.WriteLine("3.Išeiti");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("4.surinkti mokinius is failo");
            Console.WriteLine("5.sugeneruoti mokinių failą");


            choise = Console.ReadLine();
            switch (choise)
            {
                case "1":
                    {
                        try
                        {
                            Console.WriteLine("įveskite kiek bus mokinių");
                            int numStudents = Convert.ToInt32(Console.ReadLine());
                            for (int i = 0; i < numStudents; i++)
                            {
                                Console.WriteLine("įveskite studento vardą: ");
                                name = Console.ReadLine();

                                Console.WriteLine("įveskite studento pavarde: ");
                                surname = Console.ReadLine();

                                Console.WriteLine("įvekite kiek bus pažymių: ");
                                homeWorkLength = Convert.ToInt32(Console.ReadLine());
                                for (int j = 0; j < homeWorkLength; j++)
                                {
                                    Console.WriteLine("įvekite pažymius: ");
                                    int value = Convert.ToInt32(Console.ReadLine());
                                    homeWorkMarks.Add(value);
                                }

                                Console.WriteLine("įveskite egzamino balą: ");
                                examMark = Convert.ToInt32(Console.ReadLine());
                                students.Add(new Students(name, surname, homeWorkMarks, examMark));
                            }
                        }
                        catch (InvalidCastException e)
                        {
                            throw new Exception("Fsio vyte baiges error tau dabar", e);

                        }
                        goto Start;
                    }
                case "2":
                    {
                        if (students != null)
                        {
                            finalResults(students);
                        }
                        else
                        {
                            Console.WriteLine("-----nu nėra monikių-----");
                        }
                        goto Start;

                    }
                case "3":
                    {
                        break;
                    }
                case "4":
                    {
                        ReadDataFromFile(students);
                        goto Start;
                    }
                case "5":
                    {
                        StudentsFile();
                        goto Start;
                    }
            }
        }

        private static void finalResults(List<Students> students)
        {
            int sum = 0;
            Double average = 0;
            Console.WriteLine("Surname  Name                 Final points (Avg.) /  Final points (Med.)");
            Console.WriteLine("------------------------------------------------------------------------");
            for (int i = 0; i < students.Count; i++)
            {
                students[i].HomeWorkMarks.Add(students[i].ExamMark);
                for (int j = 0; j < students[i].HomeWorkMarks.Count; j++)
                {
                    sum += students[i].HomeWorkMarks[j];
                }
                average = Math.Round(average, 2);
                average = sum / students[i].HomeWorkMarks.Count;
                List<Students> sortList = students.OrderBy(o => o.Name).ToList();
                Console.WriteLine("{0,-20} {1,-22} {2,-2:N2} / {3, -10:N2}", students[i].Name, students[i].Surname, average, GetMedian(students[i].HomeWorkMarks));
            }

        }

        private static void ReadDataFromFile(List<Students> students)
        {
            String line;
            Console.WriteLine("įveskite pilną txt failo vietą");
            try
            {
                String txtFileLocation = Console.ReadLine();
                System.IO.StreamReader text = new System.IO.StreamReader(txtFileLocation);
                while ((line = text.ReadLine()) != null)
                {
                    string[] student = line.Split(' ');
                    List<int> homework = new List<int>(new int[] { int.Parse(student[2]), int.Parse(student[3]), int.Parse(student[4]), int.Parse(student[5]), int.Parse(student[6]) });
                    students.Add(new Students(
                        student[0], student[1],
                        homework,
                        int.Parse(student[7])
                        ));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error log, {0}", e.Message);
            }

        }

        private static void StudentsFile()
        {
            int min = 1;
            int max = 10;
            int sum = 0;
            int count = 0;
            string failedText = @"C:\Users\Mantas\source\repos\ConsoleApp1\failedText.txt";
            string passedText = @"C:\Users\Mantas\source\repos\ConsoleApp1\passedText.txt";
            Double average = 0;

            Random randNum = new Random();
            Console.WriteLine("Kiek norite kad sugeneruotų mokinių?");
            count = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                string name = "name" + i;
                string surname = "surname" + i;
                int[] marks = Enumerable
                     .Repeat(0, 5)
                     .Select(i => randNum.Next(min, max))
                     .ToArray();
                for (int j = 0; j < marks.Length; j++)
                {
                    sum += marks[j];
                }
                average = Math.Round(average, 2);
                average = sum / marks.Length;
                if (average < 5)
                {
                    string[] failure = { name + " " + surname + " " + marks[0] + " " + marks[1] + " " + marks[2] + " " + marks[3] + " " + marks[4] + " " + "failed"};
                    File.AppendAllLines(failedText, failure);
                }
                else if (average >= 5)
                {
                   string[] passed = { name + " " + surname + " " + marks[0] + " " + marks[1] + " " + marks[2] + " " + marks[3] + " " + marks[4] + " " + "Passed"};
                    File.AppendAllLines(passedText, passed);
                }
                Array.Clear(marks, 0, marks.Length);
                sum = 0;
                    }
        }
        private static double GetMedian(List<int> marks)
        {
            int count = marks.Count;
            marks.Sort();

            if (count == 0)
            {
                throw new InvalidOperationException("Empty collection");
            }
            else if (count % 2 == 0)
            {
                double a = marks[count / 2 - 1];
                double b = marks[count / 2];
                double median = (a + b) / 2;
                median = Math.Round(median, 2);
                return median;
            }
            else
            {
                double markss = marks[count / 2];
                markss = Math.Round(markss, 2);
                return markss;
            }
        }

    }

}

    class Students
    {
        private String name;
        private String surname;
        private List<int> homeWorkMarks;
        private int examMark;

        public Students()
        {
        }

        public Students(string name, string surname, List<int> homeWorkMarks, int examMark)
        {
            this.Name = name;
            this.Surname = surname;
            this.HomeWorkMarks = homeWorkMarks;
            this.ExamMark = examMark;
        }

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public List<int> HomeWorkMarks { get => homeWorkMarks; set => homeWorkMarks = value; }
        public int ExamMark { get => examMark; set => examMark = value; }
    }


