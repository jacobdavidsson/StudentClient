using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using StudentClient;

class Program
{
    static async Task Main(string[] args)
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("What would you like to do? Get/Post/Delete/Put/Exit:");
            string input = Console.ReadLine().ToLower();

            switch (input)
            {
                case "get":
                    await Get();
                    break;
                case "post":
                    await Post();
                    break;
                case "delete":
                    await Delete();
                    break;
                case "put":
                    await Put();
                    break;
                case "exit":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    Console.WriteLine();
                    break;
            }
        }
    }

    static async Task Get()
    {
        string apiUrl = "https://localhost:7082/api/students";
        HttpClient client = new HttpClient();

        try
        {
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(jsonResponse);

            Console.WriteLine();
            Console.WriteLine("List of students:");
            foreach (Student student in students)
            {
                Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Enrollment Date: {student.EnrollmentDate}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred: " + e.Message);
        }
        Console.WriteLine();
    }

    static async Task Post()
    {
        string apiUrl = "https://localhost:7082/api/students";
        HttpClient client = new HttpClient();

        try
        {
            Console.WriteLine();
            Console.WriteLine("Enter the student name:");
            string name = Console.ReadLine();

            Student newStudent = new Student { Name = name, EnrollmentDate = DateTime.Now };

            HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, newStudent);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            Student createdStudent = JsonConvert.DeserializeObject<Student>(jsonResponse);

            Console.WriteLine();
            Console.WriteLine("New student created:");
            Console.WriteLine($"ID: {createdStudent.Id}, Name: {createdStudent.Name}, Enrollment Date: {createdStudent.EnrollmentDate}");
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred: " + e.Message);
        }
        Console.WriteLine();
    }

    static async Task Delete() 
    {
        string apiUrl = "https://localhost:7082/api/students";
        HttpClient client = new HttpClient();

        try 
        {
            Console.WriteLine();
            Console.WriteLine("Enter the ID of the student you want to remove:");
            int studentId = int.Parse(Console.ReadLine());

            HttpResponseMessage response = await client.DeleteAsync($"{apiUrl}/{studentId}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occured: " + e.Message);
        }
        Console.WriteLine();
    }

    static async Task Put() 
    {
        string apiUrl = "https://localhost:7082/api/students";
        HttpClient client = new HttpClient();

        try 
        {
            Console.WriteLine();
            Console.WriteLine("Enter the ID of the student you want to update:");
            int studentId = int.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine("Enter the new name of the student:");
            string newName = Console.ReadLine();

            Student updatedStudent = new Student { Id = studentId, Name = newName, EnrollmentDate = DateTime.Now };

            HttpResponseMessage response = await client.PutAsJsonAsync($"{apiUrl}/{studentId}", updatedStudent);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e) 
        {
            Console.WriteLine("An error occured: " + e.Message);
        }
        Console.WriteLine();
    }
}
