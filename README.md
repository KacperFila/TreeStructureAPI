# TreeStructureAPI
Uruchamianie projektu:

Przed przystąpieniem do uruchamiania upewnij się, że posiadasz zainstalowany .NET 7.0 SDK oraz SQL Server Management Studio (ja korzystałem z wersji v18.12.1).

1. Utwórz folder w którym umieścisz projekt.
2. Otwórz w tym folderze terminal i wpisz: git clone https://github.com/KacperFila/TreeStructureAPI.git
3. W razie potrzeby w pliku appsettings.json skonfiguruj ConnectionString do swojej konfiguracji MsSQL.
4. Przejdź do katalogu z projektem wpisując: cd TreeStructureAPI/TreeStructureAPI/TreeStructureAPI
5. Wykonaj migrację wpisując polecenie: dotnet ef database update
6. Zbuduj projekt wpisując: dotnet build
7. Uruchom projekt wpisując: dotnet run --urls "https://localhost:44313"