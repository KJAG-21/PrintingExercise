using System;
using System.Collections.Generic;
using System.IO;
using PrintingDevelopmentExercise.Model;
using System.Globalization;

namespace PrintingDevelopmentExercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string basepath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string inputFileName = "InputData.txt";
            string outputFileName = "OutputData.txt";
            string inputPathFile = $"{basepath}\\Incoming\\{inputFileName}";
            string outputPathFile = $"{basepath}\\Outgoing\\{outputFileName}";

            try
            {
                if (File.Exists(inputPathFile)) {
                    //Read the content of the file as individual lines
                    string[] fileLines = System.IO.File.ReadAllLines(inputPathFile);

                    var customerRecordList = new List<CustomerRecord>();

                    //Split each row of the file
                    for (int i = 1; i < fileLines.Length; i++)
                    {
                        CustomerRecord customerRecord = new CustomerRecord(fileLines[i]);
                        customerRecordList.Add(customerRecord);
                    }

                    //Build the report
                    string outputData = PrintCustomerListData(customerRecordList, inputFileName);

                    //Create the ouput file
                    CreateFile(outputPathFile, outputData);

                    //Backups
                    BackupFile(inputPathFile, $"{basepath}\\Backup\\{inputFileName}");
                    BackupFile(outputPathFile, $"{basepath}\\Backup\\{outputFileName}");
                }
                else
                {
                    Console.WriteLine($"Not found:{inputPathFile}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Finish.");
                Console.ReadLine();
            } 
        }

        /// <summary>
        /// Build the final report based on customer data
        /// </summary>
        /// <param name="customerRecordList">customer data</param>
        /// <param name="fileName">source file name</param>
        /// <returns>report content</returns>
        static string PrintCustomerListData (List<CustomerRecord> customerRecordList, string fileName)
        {
            string customerData = string.Empty;
            decimal customerTotalAmount = 0;
            DateTime today = DateTime.Now;
            for (int i = 0; i < customerRecordList.Count; i++)
            {
                customerData += customerRecordList[i].ToString();
                customerTotalAmount += customerRecordList[i].TotalAmount;

            }

            string str = $"\"HEADER_RECORD\", \"{fileName}\", {customerRecordList.Count}, \"{customerTotalAmount:C2}\", " +
                $"\"{today.ToString("yyyy-MM-dd",CultureInfo.CreateSpecificCulture("en-US"))}\",\"{today.ToString("HH:mm:ss: tt",CultureInfo.CreateSpecificCulture("en-US"))}\"\n";
            str += customerData;
            
            return str;
        }

        /// <summary>
        /// Create a file if not exists
        /// </summary>
        /// <param name="path">file path</param>
        /// <param name="content"> content file</param>
        public static void CreateFile(string path, string content)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(content);
            }

            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }

        /// <summary>
        /// Create a copy if the source file into the destiny path
        /// </summary>
        /// <param name="sourcePath"> source file path</param>
        /// <param name="destinyPath"> destiny file path</param>
        public static void BackupFile(string sourcePath, string destinyPath)
        {
            if (File.Exists(destinyPath))
                File.Delete(destinyPath);

             File.Copy(sourcePath, destinyPath); 
        }
        
    }
}
