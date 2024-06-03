using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SSISConsole.Data;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient { BaseAddress = new Uri("http://localhost:5209/api/") };

            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. View Subjects");
                Console.WriteLine("2. Add Subject");
                Console.WriteLine("3. Add Book");
                Console.WriteLine("4. Import Subject from JSON file");
                Console.WriteLine("5. Exit");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ViewSubjects(client);
                        break;
                    case "2":
                        await AddSubject(client);
                        break;
                    case "3":
                        await AddBook(client);
                        break;
                    case "4":
                        await ImportSubjects(client);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static async Task ViewSubjects(HttpClient client)
        {
            var response = await client.GetAsync("subject");
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var subjects = JsonSerializer.Deserialize<List<Subject>>(content, options);

            if (subjects != null)
            {
                foreach (var subject in subjects)
                {
                    Console.WriteLine($"Id: {subject.Id}, Name: {subject.Name}");
                }

                Console.WriteLine("Enter Subject Id to view details:");
                var id = Console.ReadLine();
                await ViewSubjectDetails(client, id);
            }
            else
            {
                Console.WriteLine("No subjects found.");
            }
        }

        private static async Task ViewSubjectDetails(HttpClient client, string id)
        {
            var response = await client.GetAsync($"subject/{id}");
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var subject = JsonSerializer.Deserialize<Subject>(content, options);

            if (subject != null)
            {
                Console.WriteLine($"Id: {subject.Id}, Name: {subject.Name}, Description: {subject.Description}, NumberOfWeeklyClasses: {subject.NumberOfWeeklyClasses}");

                Console.WriteLine("Books:");
                foreach (var book in subject.LiteratureUsed)
                {
                    Console.WriteLine($"Book Id: {book.BookId}, Book Name: {book.BookName}");
                }

                Console.WriteLine("Enter Book Id to view details or press Enter to return:");
                var bookId = Console.ReadLine();
                if (!string.IsNullOrEmpty(bookId))
                {
                    await ViewBookDetails(client, bookId);
                }
            }
            else
            {
                Console.WriteLine("Subject not found.");
            }
        }

        private static async Task ViewBookDetails(HttpClient client, string bookId)
        {
            var response = await client.GetAsync($"book/{bookId}");
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var book = JsonSerializer.Deserialize<Book>(content, options);

            if (book != null)
            {
                Console.WriteLine($"Book Id: {book.BookId}, Book Name: {book.BookName}");

                Console.WriteLine("Fetching additional info from OpenLibrary...");
                response = await client.GetAsync($"book/info/{book.BookName}");
                var bookInfoContent = await response.Content.ReadAsStringAsync();
                var bookInfo = JsonSerializer.Deserialize<BookInfo>(bookInfoContent, options);

                if (bookInfo != null)
                {
                    Console.WriteLine("Book Info:");
                    Console.WriteLine($"Title: {bookInfo.Title}");
                    Console.WriteLine($"Authors: {string.Join(", ", bookInfo.Authors)}");
                    Console.WriteLine($"Publisher: {string.Join(", ", bookInfo.Publishers)}");
                    Console.WriteLine($"First Publish Year: {bookInfo.FirstPublishYear}");
                    Console.WriteLine($"ISBN: {string.Join(", ", bookInfo.ISBNs)}");
                    Console.WriteLine($"Number of Pages: {bookInfo.NumberOfPages}");
                    Console.WriteLine($"Cover URL: {bookInfo.CoverUrl}");
                }
                else
                {
                    Console.WriteLine("No additional information found for this book.");
                }
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        private static async Task AddSubject(HttpClient client)
        {
            Console.WriteLine("Enter Subject Name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter Subject Description:");
            var description = Console.ReadLine();
            Console.WriteLine("Enter Number of Weekly Classes:");
            var numberOfWeeklyClasses = int.Parse(Console.ReadLine());

            var newSubject = new
            {
                Name = name,
                Description = description,
                NumberOfWeeklyClasses = numberOfWeeklyClasses
            };

            var content = new StringContent(JsonSerializer.Serialize(newSubject), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("subject", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Subject added successfully.");
            }
            else
            {
                Console.WriteLine("Error adding subject.");
            }
        }

        private static async Task AddBook(HttpClient client)
        {
            Console.WriteLine("Enter Book Name:");
            var bookName = Console.ReadLine();
            Console.WriteLine("Enter Subject Id:");
            var subjectId = int.Parse(Console.ReadLine());

            var newBook = new
            {
                BookName = bookName,
                SubjectId = subjectId
            };

            var content = new StringContent(JsonSerializer.Serialize(newBook), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("book", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Book added successfully.");
            }
            else
            {
                Console.WriteLine("Error adding book.");
            }
        }

        private static async Task ImportSubjects(HttpClient client)
        {
            Console.WriteLine("Enter the path to the JSON file:");
            var filePath = Console.ReadLine();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            var json = await File.ReadAllTextAsync(filePath);
            Console.WriteLine($"Read JSON file content: {json}");
            var subjects = JsonSerializer.Deserialize<List<CreateSubjectFromJsonDto>>(json);

            var serializedSubjects = JsonSerializer.Serialize(subjects);
            Console.WriteLine($"Serialized subjects: {serializedSubjects}");

            var content = new StringContent(serializedSubjects, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("subject/import", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Subjects imported successfully.");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error importing subjects: {errorMessage}");
            }
        }

    }

}
