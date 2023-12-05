using System;
using System.Data.SqlClient;

namespace Lab1Kurs2
{
    internal class DbClass
    {
        public void Fetch_all_Students(SqlConnection connection)
        {
            Console.Write("Do you want to sort on 1. firstname or 2.last name: ");
            string orderInput = Console.ReadLine();
            string sortingColumn = (orderInput == "1") ? "FirstName" : (orderInput == "2") ? "LastName" : "";

            if (string.IsNullOrEmpty(sortingColumn))
            {
                Console.WriteLine("Invalid input");
                return;
            }

            Console.Write("Do you want to sort on 1. ASC or 2.DESC: ");
            string ascDescInput = Console.ReadLine();
            string sortingDirection = (ascDescInput == "1") ? "ASC" : (ascDescInput == "2") ? "DESC" : "";

            if (string.IsNullOrEmpty(sortingDirection))
            {
                Console.WriteLine("Invalid input");
                return;
            }

            string queryString = $"SELECT [FirstName], [LastName], [Class] FROM [dbo].[Students] ORDER BY {sortingColumn} {sortingDirection};";

            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string _firstname = reader.GetString(reader.GetOrdinal("FirstName"));
                        string _lastname = reader.GetString(reader.GetOrdinal("LastName"));
                        string _class = reader.GetString(reader.GetOrdinal("Class"));
                        
                        Console.WriteLine($"Firstname: {_firstname} Lastname: {_lastname} Class: {_class}");
                    }
                }
            }
        }


        public void Fetch_Students_class(SqlConnection connection)
        {

            using (SqlCommand command = new SqlCommand("SELECT DISTINCT Class FROM dbo.Students", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {                      
                         string _class = reader.GetString(reader.GetOrdinal("Class"));  
                        Console.WriteLine($"Class: {_class}");
                    }
                }
            }
            using (SqlCommand command = new SqlCommand("SELECT FirstName, Class FROM dbo.Students WHERE Class = @Class ", connection))
            {
                Console.Write("Class to view: ");
                string class_input = Console.ReadLine();
                command.Parameters.AddWithValue("@Class", class_input);  
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string _firstname = reader.GetString(reader.GetOrdinal("FirstName"));
                        Console.WriteLine($"Firstname: {_firstname}");
                    }
                }
            }

        }
        
        public void Add_Teacher(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO dbo.Teachers (TeacherName, Class) VALUES (@TeacherName, @Class);", connection))
            {
                Console.WriteLine("Enter some details of the teahcer");
                Console.Write("Teachername: ");
                string teacherName_input = Console.ReadLine();
                Console.Write("Class: ");
                string class_input = Console.ReadLine();
                command.Parameters.AddWithValue("@TeacherName", teacherName_input);
                command.Parameters.AddWithValue("@Class", class_input);

                command.ExecuteNonQuery();
                Console.WriteLine($"Added {teacherName_input} to the database.");
            }

            

        }

        public void Fetch_Teacher(SqlConnection connection)
        {
            Console.WriteLine("Choose your view");
            Console.WriteLine("1. For All Teachers");
            Console.WriteLine("2. For Class Teachers ");
            string input = Console.ReadLine();

            if (input == "1") {
                using (SqlCommand command = new SqlCommand("SELECT TeacherName, Class FROM dbo.Teachers ORDER BY TeacherName ASC;", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string _teachername = reader.GetString(reader.GetOrdinal("TeacherName"));
                            string _class = reader.GetString(reader.GetOrdinal("Class"));

                            Console.WriteLine($"Teacher: {_teachername} Class: {_class}");
                        }
                    }
                }

            } else if(input == "2")
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.Teachers WHERE Class = @Class ", connection))
                {
                    Console.Write("Class to view: ");
                    string class_input = Console.ReadLine();
                    command.Parameters.AddWithValue("@Class", class_input);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string _teachersname = reader.GetString(reader.GetOrdinal("TeacherName"));
                            string _class = reader.GetString(reader.GetOrdinal("Class"));
                            
                            Console.WriteLine($"Class: {_class}");
                            Console.WriteLine($"Teachername: {_teachersname}");
                            Console.WriteLine();
                            
                        }
                    }
                }

            }
            
        }
        public void Fetch_All_Grades(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT FirstName, MAX(Grade) AS MaxGrade, Course FROM dbo.Students GROUP BY FirstName, Course;", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string _firstname = reader.GetString(reader.GetOrdinal("FirstName"));
                        int _maxGrade = reader.GetInt32(reader.GetOrdinal("MaxGrade"));
                        string _course = reader.GetString(reader.GetOrdinal("Course"));

                        Console.WriteLine($"Name: {_firstname} Course: {_course} Grade: {_maxGrade.ToString()}");
                    }
                }
            }
        }

        public void Avg_Grade(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT Course, AVG(Grade) AS AverageGrade, MAX(Grade) AS MaxGrade, MIN(Grade) AS MinGrade FROM dbo.Students GROUP BY Course;", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string _course = reader.GetString(reader.GetOrdinal("Course"));
                        int _avgGrade = reader.GetInt32(reader.GetOrdinal("AverageGrade"));
                        int _maxGrade = reader.GetInt32(reader.GetOrdinal("MaxGrade")); 
                        int _minGrade = reader.GetInt32(reader.GetOrdinal("MinGrade")); 

                        Console.WriteLine($"Course: {_course}");
                        Console.WriteLine($"AverageGrade: {_avgGrade}");
                        Console.WriteLine($"Higest grade: {_maxGrade}");
                        Console.WriteLine($"Lowest grade: {_minGrade}");
                        Console.WriteLine();
                    }
                }
            }
        }

        public void Add_Student(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO dbo.Students (FirstName, LastName, Class, Course, Grade) VALUES (@FirstName, @LastName, @Class, @Course, @Grade);", connection))
            {
                Console.WriteLine("Enter some details of the Student");
                Console.Write("Firstname: ");
                string FirstName_input = Console.ReadLine();
                Console.Write("Lastname: ");
                string LastName_input = Console.ReadLine();
                Console.Write("Class: ");
                string class_input = Console.ReadLine();
                Console.Write("Course: ");
                string course_input = Console.ReadLine();
                Console.Write("Grade: ");
                string grade_input = Console.ReadLine();


                command.Parameters.AddWithValue("@FirstName", FirstName_input);
                command.Parameters.AddWithValue("@LastName", LastName_input);
                command.Parameters.AddWithValue("@Class", class_input);
                command.Parameters.AddWithValue("@Course",course_input);
                command.Parameters.AddWithValue("@Grade", grade_input);
                command.ExecuteNonQuery();
                Console.WriteLine($"Added {FirstName_input} to the student database.");
            }

        }




    }
}
