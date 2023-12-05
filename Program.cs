using System.Data.SqlClient;


namespace Lab1Kurs2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\.;Initial Catalog=Lab1Kurs2;Integrated Security=True";
            bool is_running = true;
            string menu = " 1.Fetch all students \n 2.Fetch all students in a class \n 3.Add Staff \n 4.Fetch Staff \n 5.Fetch all grades last month \n 6.Average grade course \n 7. Add Student \n 8. Exit \n \n Enter command: ";

            while (is_running)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the student database");
                Console.Write(menu);

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string input = Console.ReadLine();
                        DbClass dbClass = new DbClass();

                        switch (input)
                        {
                            case "1":

                                dbClass.Fetch_all_Students(connection);
                                Console.WriteLine("Press Enter to return to menu...");
                                Console.ReadLine();
                                break;
                            case "2":

                                dbClass.Fetch_Students_class(connection);
                                Console.WriteLine("Press Enter to return to menu...");
                                Console.ReadLine();
                                break;

                            case "3":
                                dbClass.Add_Teacher(connection);
                                Console.WriteLine("Press Enter to return to menu...");
                                Console.ReadLine();
                                break;

                            case "4":
                                dbClass.Fetch_Teacher(connection);
                                Console.WriteLine("Press Enter to return to menu...");
                                Console.ReadLine();
                                break;

                            case "5":
                                dbClass.Fetch_All_Grades(connection);
                                Console.WriteLine("Press Enter to return to menu...");
                                Console.ReadLine();
                                break;
                            case "6":
                                dbClass.Avg_Grade(connection);  
                                Console.WriteLine("Press Enter to return to menu...");
                                Console.ReadLine();
                                break;
                            case "7":
                                dbClass.Add_Student(connection);
                                Console.WriteLine("Press Enter to return to menu...");
                                Console.ReadLine();
                                break;
                            case "8":
                                Console.WriteLine("Exiting system...");
                                is_running = false;
                                break;
                            default:
                                // Handle invalid input
                                Console.WriteLine("Invalid input. Please enter a number between 1 and 8.");
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                    Console.ReadLine();
                }
            }




        }
    }
}