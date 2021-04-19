using System;
using System.Collections.Generic;

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
                        catch(InvalidCastException e)
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
                        goto Start;                          
                    }
                case "3":
                    {
                        break;
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
                for(int j = 0; j < students[i].HomeWorkMarks.Count; j++)
                {
                    sum += students[i].HomeWorkMarks[j];
                }
                average = Math.Round(average, 2);
                average = sum / students[i].HomeWorkMarks.Count;

                Console.WriteLine(students[i].Name + " " + students[i].Surname + "                       " + average + "    /   " + GetMedian(students[i].HomeWorkMarks));
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


