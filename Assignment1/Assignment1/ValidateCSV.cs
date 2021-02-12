using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Assignment1
{
    class ValidateCSV
    {
        public static List<Customer> customerData = new List<Customer>();
        public static int skippedRecord = 0;
        public static int goodRecord = 0;
        public void WalkDir(String path)
        {
            try
            {
                string[] list = Directory.GetDirectories(path);
                ValidateCSV fw1 = new ValidateCSV();
                if (list == null) return;
                foreach (string dirpath in list)
                {
                    if (Directory.Exists(dirpath))
                    {
                        WalkDir(dirpath);
                    }
                }
                string[] fileList = Directory.GetFiles(path);
                foreach (string filepath in fileList)
                {
                    if (System.IO.Path.GetExtension(filepath) == ".csv")
                    {
                        String[] splitDir = filepath.Split("\\");

                        int length = splitDir.Length;

                        String fileDate = splitDir[length - 3] + "-" + splitDir[length - 2] + "-" + splitDir[length - 4];

                        fw1.Parse(filepath, fileDate);
                    }
                    else
                    {
                        using (StreamWriter w = File.AppendText("../../../../Log/log.txt"))
                        {
                            w.WriteLine("Not processed file: Reason -  " + filepath + " is not a .csv file");
                        }
                    }


                }
            }
            catch (FileNotFoundException)
            {
                writeLog("../../../../Log/log.txt", "The file or directory cannot be found.");
            }
            catch (DirectoryNotFoundException)
            {
                writeLog("../../../../Log/log.txt", "The file or directory cannot be found.");
            }
            catch (DriveNotFoundException)
            {
                writeLog("../../../../Log/log.txt", "The drive specified in 'path' is invalid.");
            }
            catch (PathTooLongException)
            {
                writeLog("../../../../Log/log.txt", "'path' exceeds the maxium supported path length.");
            }
            catch (UnauthorizedAccessException)
            {
                writeLog("../../../../Log/log.txt", "You do not have permission to create this file.");
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32)
            {
                writeLog("../../../../Log/log.txt", "There is a sharing violation.");
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 80)
            {
                writeLog("../../../../Log/log.txt", "The file already exists.");
            }
            catch (IOException e)
            {
                writeLog("../../../../Log/log.txt", $"An exception occurred:\nError code: " +
                                  $"{e.HResult & 0x0000FFFF}\nMessage: {e.Message}");
            }
            catch (Exception e)
            {
                writeLog("../../../../Log/log.txt", e.Message);
            }
        }

        public void Parse(String fileName, String fileDate)
        {
            try
            {
                Boolean isHeader = true;
                using var streamReader = File.OpenText(fileName);
                using var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);

                /*csvReader.Read() will ignore the header so that the headers will not get 
                counted in both Processed records and skipped records*/
                csvReader.Read();

                while (csvReader.Read())
                {
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }
                    string firstName = csvReader.GetField(0);
                    string lastName = csvReader.GetField(1);
                    string streetNumber = csvReader.GetField(2);
                    string street = csvReader.GetField(3);
                    string city = csvReader.GetField(4);
                    string province = csvReader.GetField(5);
                    string postalCode = csvReader.GetField(6);
                    string country = csvReader.GetField(7);
                    string phoneNumber = csvReader.GetField(8);
                    string emailAddress = csvReader.GetField(9);

                    if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(streetNumber) || string.IsNullOrEmpty(street) || string.IsNullOrEmpty(city) ||
                         string.IsNullOrEmpty(province) || string.IsNullOrEmpty(postalCode) || string.IsNullOrEmpty(country) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(emailAddress))
                    {
                        skippedRecord++;
                    }
                    else
                    {
                        Regex regexEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                        Match emailMatch = regexEmail.Match(emailAddress);
                        if (emailMatch.Success && Regex.Match(firstName, @"^[a-zA-Z\s.]*$").Success && Regex.Match(lastName, @"^[a-zA-Z\s.]*$").Success
                            && Regex.Match(province, @"^[a-zA-Z\s.]*$").Success && Regex.Match(country, @"^[a-zA-Z\s.]*$").Success
                           )

                        {
                            customerData.Add(new Customer(firstName, lastName, streetNumber, street, city, province, postalCode, country, phoneNumber, emailAddress, Convert.ToDateTime(fileDate)));
                            goodRecord++;
                        }
                        else
                        {
                            skippedRecord++;
                        }
                    }
                }

            }
            catch (FileNotFoundException)
            {
                writeLog("../../../../Log/log.txt", "The file or directory cannot be found.");
            }
            catch (DirectoryNotFoundException)
            {
                writeLog("../../../../Log/log.txt", "The file or directory cannot be found.");
            }
            catch (DriveNotFoundException)
            {
                writeLog("../../../../Log/log.txt", "The drive specified in 'path' is invalid.");
            }
            catch (PathTooLongException)
            {
                writeLog("../../../../Log/log.txt", "'path' exceeds the maxium supported path length.");
            }
            catch (UnauthorizedAccessException)
            {
                writeLog("../../../../Log/log.txt", "You do not have permission to create this file.");
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32)
            {
                writeLog("../../../../Log/log.txt", "There is a sharing violation.");
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 80)
            {
                writeLog("../../../../Log/log.txt", "The file already exists.");
            }
            catch (IOException e)
            {
                writeLog("../../../../Log/log.txt", $"An exception occurred:\nError code: " +
                                  $"{e.HResult & 0x0000FFFF}\nMessage: {e.Message}");
            }
            catch (Exception e)
            {
                writeLog("../../../../Log/log.txt", e.Message);
            }
        }

        public void WriteOutput()
        {
            try
            {
                using var mem = new MemoryStream();
                using var writer = new StreamWriter("../../../../Output/output.csv");
                using var csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture);

                csvWriter.WriteField("First Name");
                csvWriter.WriteField("Last Name");
                csvWriter.WriteField("street Number");
                csvWriter.WriteField("Street");
                csvWriter.WriteField("City");
                csvWriter.WriteField("Province");
                csvWriter.WriteField("Country");
                csvWriter.WriteField("Country");
                csvWriter.WriteField("Phone Number");
                csvWriter.WriteField("Email Address");
                csvWriter.WriteField("File Dated");

                csvWriter.NextRecord();

                foreach (var customer in customerData)
                {
                    csvWriter.WriteField(customer.firstName);
                    csvWriter.WriteField(customer.lastName);
                    csvWriter.WriteField(customer.streetNumber);
                    csvWriter.WriteField(customer.street);
                    csvWriter.WriteField(customer.city);
                    csvWriter.WriteField(customer.province);
                    csvWriter.WriteField(customer.postalCode);
                    csvWriter.WriteField(customer.country);
                    csvWriter.WriteField(customer.phoneNumber);
                    csvWriter.WriteField(customer.emailAddress);
                    csvWriter.WriteField(customer.fileDate);
                    csvWriter.NextRecord();
                }

                writer.Flush();
                var result = Encoding.UTF8.GetString(mem.ToArray());
                Console.WriteLine(result);
            }
            catch (FileNotFoundException)
            {
                writeLog("../../../../Log/log.txt", "The file or directory cannot be found.");
            }
            catch (DirectoryNotFoundException)
            {
                writeLog("../../../../Log/log.txt", "The file or directory cannot be found.");
            }
            catch (DriveNotFoundException)
            {
                writeLog("../../../../Log/log.txt", "The drive specified in 'path' is invalid.");
            }
            catch (PathTooLongException)
            {
                writeLog("../../../../Log/log.txt", "'path' exceeds the maxium supported path length.");
            }
            catch (UnauthorizedAccessException)
            {
                writeLog("../../../../Log/log.txt", "You do not have permission to create this file.");
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32)
            {
                writeLog("../../../../Log/log.txt", "There is a sharing violation.");
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 80)
            {
                writeLog("../../../../Log/log.txt", "The file already exists.");
            }
            catch (IOException e)
            {
                writeLog("../../../../Log/log.txt", $"An exception occurred:\nError code: " +
                                  $"{e.HResult & 0x0000FFFF}\nMessage: {e.Message}");
            }
            catch (Exception e)
            {
                writeLog("../../../../Log/log.txt", e.Message);
            }
        }

        public void writeLog(String path, String text)
        {
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine("Exception occured. Exception:  " + text);
            }
        }
        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            using (StreamWriter w = File.CreateText("../../../../Log/log.txt"))
            {
                w.WriteLine("Log Messages - CSV Validation");
            }
            ValidateCSV fw = new ValidateCSV();
            fw.WalkDir("../../../../Sample Data");
            fw.WriteOutput();
            using (StreamWriter w = File.AppendText("../../../../Log/log.txt"))
            {
                w.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");
                w.WriteLine("Total Number of Processed Rows: " + goodRecord);
                w.WriteLine("Total Number of Skipped Rows: " + skippedRecord);
                w.Close();
            }
            // Display the result in console
            Console.WriteLine("Program Output in Console");
            Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");
            Console.WriteLine("Total Number of Processed Rows: " + goodRecord);
            Console.WriteLine("Total Number of Skipped Rows: " + skippedRecord);
            watch.Stop();
        }
    }
}
