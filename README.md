Before starting this project follow the instructions for setting up the project from this repository: https://github.com/izabelaugrevska/damilah-graduate-challenge.

1. Clone the repository and navigate to the project folder
   git clone https://github.com/izabelaugrevska/damilah-challenge-console
   cd SSISConsole
2. Update the localhost port according to your own if needed in Program.cs file on this line:
    var client = new HttpClient { BaseAddress = new Uri("http://localhost:5209/api/") };
3. Run the application with the command:
   dotnet run

Upon starting the application, you will be presented with the following menu:

![image](https://github.com/izabelaugrevska/damilah-challenge-console/assets/98963569/5058cecb-cfaa-496c-a7aa-f7703dac0791)



You can choose between viewing a list of Subjects, Adding a Subject, Adding a Book for a Subject, Importing Subjects from a JSON FILE.
From the list of Subjects you can choose which subject you would like to see more details about with inputing the subject id.
From the list of books in LiteratureUsed for the subject you can choose a Book if you want to see more information about the book such as Author, ISBN etc.



