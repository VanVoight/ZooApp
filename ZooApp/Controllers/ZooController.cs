using System;
using System.Collections.Generic;
using ZooApp.Models;
using ZooApp.Views;

namespace ZooApp.Controllers
{
    public class ZooController
    {
        private readonly UserController _userController;
        private readonly AnimalController _animalController;
        private readonly ZooView _zooView;

        private bool isAdminMenu = false;
        private bool isStandardMenu = false;

        public ZooController(UserController userController, AnimalController animalController, ZooView zooView)
        {
            _userController = userController;
            _animalController = animalController;
            _zooView = zooView;
        }

        private bool HandleAdminMenuOption(int selectedOption, ref User loggedInUser)
        {
            switch (selectedOption)
            {
                case 0:
                    // Zmien typ konta użytkownika
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Podaj nazwę użytkownika, którego chcesz zmienić: ");
                    Console.ResetColor();
                    string usernameToChange = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Podaj nowy typ konta (admin/standard): ");
                    Console.ResetColor();
                    string newAccountType = Console.ReadLine();

                    // Zmiana typu konta
                    bool isAccountTypeChanged = _userController.ChangeAccountType(usernameToChange, newAccountType);

                    if (isAccountTypeChanged)
                    {
                        _zooView.ShowMessage($"Typ konta użytkownika {usernameToChange} został zmieniony na {newAccountType}");
                    }
                    else
                    {
                        _zooView.ShowMessage($"Nie udało się zmienić typu konta użytkownika {usernameToChange}");
                    }

                    Console.WriteLine("Naciśnij dowolny klawisz aby wrócić do menu");
                    Console.ReadKey();
                    return true;

                case 1:
                    Console.Clear();
                    // Wyświetl listę zwierząt
                    List<Animal> animalList = _animalController.GetAnimalList();
                    _zooView.ShowAnimalList(animalList);

                    Console.WriteLine("Naciśnij dowolny klawisz aby wrócić do menu");
                    Console.ReadKey();
                    return true;

                case 2:
                    // Dodaj zwierzę do bazy danych
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Podaj nazwę zwierzęcia: ");
                    Console.ResetColor();
                    string name = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Podaj gatunek zwierzęcia: ");
                    Console.ResetColor();
                    string species = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Podaj środowisko zwierzęcia: ");
                    Console.ResetColor();
                    string habitat = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Podaj opis zwierzęcia: ");
                    Console.ResetColor();
                    string description = Console.ReadLine();

                    // Dodaj zwierzę do bazy danych
                    _animalController.AddAnimal(name, species, habitat, description);

                    _zooView.ShowMessage($"Zwierzę {name} zostało dodane do bazy danych");
                    Console.WriteLine("Naciśnij dowolny klawisz aby wrócić do menu");
                    Console.ReadKey();
                    return true;
                case 3:
                    // Usuń zwierzę z bazy danych
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Podaj nazwę zwierzęcia do usunięcia: ");
                    Console.ResetColor();
                    string removename = Console.ReadLine();
                    _animalController.RemoveAnimal(removename);
                    _zooView.ShowMessage($"Zwierzę {removename} zostało usunięte z bazy danych");
                    Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu");
                    Console.ReadKey();
                    return true;

                case 4:
                    // Wyloguj się
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    _zooView.ShowMessage("Wylogowano.");
                    Console.ResetColor();
                    Console.WriteLine("Naciśnij dowolny klawisz aby wrócić do menu");
                    Console.ReadKey();
                    loggedInUser = null;
                    return false;
            }
            return false;
        }
        private bool HandleStandardMenuOption(int selectedOption, ref User loggedInUser)
        {
            switch (selectedOption)
            {
                case 0:
                    Console.Clear();
                    // Wyświetl listę zwierząt
                    List<Animal> animalList = _animalController.GetAnimalList();
                    _zooView.ShowAnimalList(animalList);

                    Console.WriteLine("Naciśnij dowolny klawisz aby wrócić do menu");
                    Console.ReadKey();
                    return true;

                case 1:
                    // Wyloguj się
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    _zooView.ShowMessage("Wylogowano.");
                    Console.ResetColor();
                    Console.WriteLine("Naciśnij dowolny klawisz aby wrócić do menu");
                    Console.ReadKey();
                    loggedInUser = null;
                    return false;
            }
            return false;
        }
        private string GetHiddenConsoleInput()
        {
            string input = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Obsługa klawisza Backspace
                if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b"); // Kasuj ostatnią gwiazdkę
                }
                // Ignoruj klawisze kontrolne
                else if (!char.IsControl(key.KeyChar))
                {
                    input += key.KeyChar;
                    Console.Write("*");
                }

            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Przejście do nowej linii po wprowadzeniu hasła

            return input;
        }

        private void HandleMenuOption(int selectedOption, ref User loggedInUser, out User user)
        {
            user = null;
            switch (selectedOption)
            {
                case 0:
                    // Zaloguj się
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Podaj nazwę użytkownika: ");
                    Console.ResetColor();
                    string username = Console.ReadLine();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Podaj hasło: ");
                    Console.ResetColor();
                    string password = GetHiddenConsoleInput();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Podaj typ konta (admin/standard): ");
                    Console.ResetColor();
                    string accountType = Console.ReadLine();


                    // Authenticate user
                    user = _userController.AuthenticateUser(username, password, accountType);

                    if (user != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        _zooView.ShowMessage("Zalogowano.");
                        
                        Console.WriteLine("Naciśnij dowolny klawisz aby wrócić do menu");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        _zooView.ShowMessage("Nieprawidłowe dane logowania lub typ konta. Zaloguj się ponownie lub utwórz nowe konto");
                        Console.ResetColor();
                        Console.WriteLine("Naciśnij dowolny klawisz aby wrócić do menu");
                        Console.ReadKey();
                    }
                    break;

                case 1:
                    // Zarejestruj się
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Podaj nazwę użytkownika: ");
                    Console.ResetColor();
                    string username_register = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Podaj hasło: ");
                    Console.ResetColor();
                    string password_register = Console.ReadLine();
                    // Register user
                    bool isUserRegistered = _userController.RegisterUser(username_register, password_register);

                    if (isUserRegistered)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        _zooView.ShowMessage("Użytkownik został utworzony");
                        Console.ResetColor();
                        Console.WriteLine("Naciśnij dowolny klawisz aby wrócić do menu");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        _zooView.ShowMessage("Użytkownik o podanej nazwie już istnieje w bazie danych. Spróbuj jeszcze raz lub zaloguj się");
                        Console.ResetColor();
                        Console.WriteLine("Naciśnij dowolny klawisz aby wrócić do menu");
                        Console.ReadKey();
                    }
                    break;

                case 2:
                    // Zamknij program
                    _zooView.ShowMessage("Naciśnij dowolny klawisz, aby zakończyć...");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
            }
        }

        public void RunMenu()
        {
            Console.CursorVisible = false;
            int selectedOption = 0;
            string[] generalMenuOptions = { "Zaloguj się", "Zarejestruj się", "Zakończ" };
            string[] adminMenuOptions = { "Zmień typ konta użytkownika", "Wyświetl listę zwierząt", "Dodaj nowe zwierzę", "Usuń zwierzę", "Wyloguj się" };
            string[] standardMenuOptions = { "Wyświetl listę zwierząt", "Wyloguj się" };
            string[] currentMenuOptions = generalMenuOptions;
            ConsoleKeyInfo key;
            User loggedInUser = null;
            bool isLoggedInAsAdmin = false;
            bool isLoggedInAsStandard = false;

            do
            {
                if (isLoggedInAsAdmin)
                {
                    Console.Clear();
                    currentMenuOptions = adminMenuOptions;
                    _zooView.DrawMenu(adminMenuOptions, selectedOption);
                }
                else if (isLoggedInAsStandard)
                {
                    Console.Clear();
                    currentMenuOptions = standardMenuOptions;
                    _zooView.DrawMenu(standardMenuOptions, selectedOption);
                }
                else
                {
                    currentMenuOptions = generalMenuOptions;
                    _zooView.DrawMenu(generalMenuOptions, selectedOption);
                }

                key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedOption > 0)
                            selectedOption--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedOption < currentMenuOptions.Length - 1)
                            selectedOption++;
                        break;

                    case ConsoleKey.Enter:
                        if (loggedInUser != null && loggedInUser.Typ_konta == "admin")
                        {
                            isLoggedInAsAdmin = HandleAdminMenuOption(selectedOption, ref loggedInUser);

                            // Po wylogowaniu admina, przywróć pierwotne opcje menu
                            currentMenuOptions = generalMenuOptions;
                            selectedOption = 0;
                        }
                        else if (loggedInUser != null && loggedInUser.Typ_konta == "standard")
                        {
                            isLoggedInAsStandard = HandleStandardMenuOption(selectedOption, ref loggedInUser);

                            // Po wylogowaniu standardowego użytkownika, przywróć pierwotne opcje menu
                            currentMenuOptions = generalMenuOptions;
                            selectedOption = 0;
                        }
                        else
                        {
                            User user = null;
                            HandleMenuOption(selectedOption, ref loggedInUser, out user);

                            // Aktualizacja loggedInUser po zalogowaniu/rejestracji
                            loggedInUser = user;

                            // Jeśli zalogowano jako admin, ustaw menu admina
                            if (loggedInUser != null && loggedInUser.Typ_konta == "admin")
                            {
                                currentMenuOptions = adminMenuOptions;
                                isLoggedInAsAdmin = true;
                                isLoggedInAsStandard = false;
                            }
                            else if (loggedInUser != null && loggedInUser.Typ_konta == "standard")
                            {
                                currentMenuOptions = standardMenuOptions;
                                isLoggedInAsStandard = true;
                                isLoggedInAsAdmin = false;
                            }
                        }
                        break;
                }
            } while (key.Key != ConsoleKey.Escape);
        }
    }
}