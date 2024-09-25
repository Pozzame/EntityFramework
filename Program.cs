
using Microsoft.EntityFrameworkCore;

class Program
{
    public static void Main()
    {
        Database db = new Database();
        View view= new View(db);
        Controller controller = new Controller(db, view);

        controller.MainMenu();
    }       
}

class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

class Database : DbContext
{
    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder option)
    {
        option.UseSqlite("Data Source=db.db");
    }
}

class View
{
    private Database _db;
    public View(Database db)
    {
        _db=db;
    }
    public void ShowMainMenu()
    {
        Console.WriteLine("1. Aggiungi user");
        Console.WriteLine("2. Leggi user");
        Console.WriteLine("3. Modifica user");
        Console.WriteLine("4. Elimina user");
        Console.WriteLine("5. Esci");
    }

    public void ShowUsers(List<User> users)
    {
        foreach(var user in users)
            Console.WriteLine(user.Name);
    }
    public string GeInput()
    {
        return Console.ReadLine()!;
    }
}

class Controller
{
    private Database _db;
    private View _view;
    public Controller(Database db,View view)
    {
        _db=db;
        _view=view;
    }

    public void MainMenu()
    {
        while (true)
        {
            _view.ShowMainMenu();
            var input = _view.GeInput();
            if (input == "1")
            {
                AddUser();
            }
            else if (input == "2")
            {
                ShowUser();
            }
            else if (input == "3")
            {
                UpdateUser();
            }
            else if (input == "4")
            {
                DeleteUser();
            }
            else if (input == "5")
            {
                break;
            }
        }
    }
    private void AddUser()
    {
        Console.WriteLine("Enter user name");
        var name = _view.GeInput();
        _db.Users.Add(new User { Name = name});
        _db.SaveChanges();
    }
    private void ShowUser()
    {
        var users = _db.Users.ToList();
        _view.ShowUsers(users);
    }
    private void UpdateUser()
    {
        Console.WriteLine("Enter user name");
        var oldName = _view.GeInput();
        Console.WriteLine("Enter new user name");
        var newName = _view.GeInput();
        User user = null;
        foreach (var u in _db.Users)
        {
            if (u.Name == oldName)
            {
                user = u;
                break;
            }
        }
        if (user == null)
        {
            user.Name = newName;
            _db.SaveChanges();
        }
    }

    private void DeleteUser()
    {
        Console.WriteLine("Enter user name");
        var name = _view.GeInput();
        User UserToDelete = null;
        foreach(var user in _db.Users)
        {
            if(user.Name == name)
            {
                UserToDelete = user;
                break;
            }
        }
        if(UserToDelete != null)
        {
            _db.Users.Remove(UserToDelete);
            _db.SaveChanges();
        }
    }
}