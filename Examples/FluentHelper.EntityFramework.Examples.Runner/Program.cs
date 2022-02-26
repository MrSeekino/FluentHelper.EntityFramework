using FluentHelper.EntityFramework.Examples.Models;
using FluentHelper.EntityFramework.Examples.Repositories;
using System;
using System.Linq;

namespace FluentHelper.EntityFramework.Examples.Runner
{
    class Program
    {

        TestData exampleData = new TestData
        {
            Id = Guid.NewGuid(),
            Name = "ExampleData",
            CreationDate = DateTime.UtcNow,
            Active = true
        };

        TestDataRepository testDataRepository { get; set; }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.StartProgram();
        }

        public Program()
        {
            testDataRepository = new TestDataRepository();
        }

        void StartProgram()
        {
            try
            {
                var testDataList = testDataRepository.GetAll().ToList();
                Console.WriteLine($"Table contains {testDataList.Count} rows");

                PressToContinue();

                Console.WriteLine($"Adding 1 row..");
                testDataRepository.Add(exampleData);

                testDataList = testDataRepository.GetAll().ToList();
                Console.WriteLine($"Table contains {testDataList.Count} rows");

                PressToContinue();

                Console.WriteLine($"Removing 1 row..");
                testDataRepository.Remove(exampleData.Id);

                testDataList = testDataRepository.GetAllWithCustomQuery().ToList();
                Console.WriteLine($"Table contains {testDataList.Count} rows");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            PressToContinue();
        }

        void PressToContinue()
        {
            Console.WriteLine("Enter any key to continue..");
            Console.ReadLine();
        }
    }
}
