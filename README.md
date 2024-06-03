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

With choosing option number 1, a list of all the subjects from the database will be displayed with their name and id:

![image](https://github.com/izabelaugrevska/damilah-challenge-console/assets/98963569/c351be8d-bd35-4a85-b59a-5f08005f3189)

You can choose one of the Subjects to see more information by entering their ID:

![image](https://github.com/izabelaugrevska/damilah-challenge-console/assets/98963569/2cb8a306-e699-411e-9bd5-5f2a93c3848e)

From the list of books for the specific subject, you can enter its ID and more information will be fetched through OpenLibrary Api:

![image](https://github.com/izabelaugrevska/damilah-challenge-console/assets/98963569/849a9b17-7ee5-458e-b10d-d5022f7dc18e)

With choosing option number 2, you can insert a new Subject into the database with entering the Name, Description and Number of Weekly Classes:

![image](https://github.com/izabelaugrevska/damilah-challenge-console/assets/98963569/59137538-a5ad-4a59-8e95-4385720ec95f)

With choosing option number 3, you can add a book that is used by a subject:

![image](https://github.com/izabelaugrevska/damilah-challenge-console/assets/98963569/fadb6790-ab7a-42b3-9d2c-b1216abcf7d5)

With choosing option number 4, you can import a list of subjects from a json file by enetring the path of the file:

![image](https://github.com/izabelaugrevska/damilah-challenge-console/assets/98963569/6dc9a5ff-25be-4196-a509-2893e4b8de0d)




