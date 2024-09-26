
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
    public bool Enable { get; set; }
}

class Abbonamento
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}

class Transazione
{
    public int Id { get; set; }
    public User? User { get; set; }
    public DateOnly Data { get; set; }
    public Abbonamento? Type { get; set; }
}

class Database : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Abbonamento> Abbonamenti { get; set; }
    public DbSet<Transazione> Transazioni { get; set; }
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
        Console.WriteLine("3. Leggi disabled user");
        Console.WriteLine("4. Modifica user");
        Console.WriteLine("5. Elimina user");
        Console.WriteLine("6. Toggle user");;
        Console.WriteLine("7. Visualizza tipi di abbonamento");
        Console.WriteLine("8. Crea nuovo tipo di abbonamento");
        Console.WriteLine("9. Elimina tipo di abbonamento");
        Console.WriteLine("10. Crea nuova transazione");
        Console.WriteLine("11. Elimina transazione");
        Console.WriteLine("12. Visualizza transazioni");
        Console.WriteLine("13. Esci");
    }

    public void ShowUsers(List<User> users, bool enable)
    {
        foreach(var user in users)
        {
            if (enable)
            {
                if (user.Enable)
                    Console.WriteLine(user.Name);
            }
            else
            {
                if (!user.Enable)
                    Console.WriteLine(user.Name);
            }
        }
    }
    public string GeInput()
    {
        return Console.ReadLine()!;
    }

    internal void ShowAbbonamenti(List<Abbonamento> abbonamento)
    {
        foreach(var item in abbonamento)
            Console.WriteLine($"Name:\t{item.Name}\tPrice:\t{item.Price}");
    }

    internal void ShowTransazioni(List<Transazione> transazioni)
    {
        foreach(var item in transazioni)
            Console.WriteLine($"ID:\t{item.Id}\tName:\t{item.Data}\tUser:\t{item.User.Name}\tType:\t{item.Type.Name}");
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
                ShowUser(true);
            }
            else if (input == "3")
            {
                ShowUser(false);
            }
            else if (input == "4")
            {
                UpdateUser();
            }
            else if (input == "5")
            {
                DeleteUser();
            }
            else if (input == "6")
            {
                ToggleUser();
            }
            else if (input == "7")
            {
                ShowAbbonamenti();
            }
            else if (input == "8")
            {
                NewAbbonamenti();
            }
            else if (input == "9")
            {
                DeleteAbbonamento();
            }
            else if (input == "10")
            {
                NewTransazione();
            }
            else if (input == "11")
            {
                DeleteTransazione();
            }
            else if (input == "12")
            {
                ShowTransazioni();
            }
            else if (input == "13")
            {
                break;
            }
        }
    }

    private void ShowTransazioni()
    {
        var transazioni = _db.Transazioni.ToList();
        _view.ShowTransazioni(transazioni);
    }

    private void DeleteTransazione()
    {
        Console.WriteLine("Enter Transazione ID");
        var id = Convert.ToInt32(_view.GeInput());
        Transazione TransToDelete = null;
        foreach(var tran in _db.Transazioni)
        {
            if(tran.Id == id)
            {
                TransToDelete = tran;
                break;
            }
        }
        if(TransToDelete != null)
        {
            _db.Transazioni.Remove(TransToDelete);
            _db.SaveChanges();
        }
    }

    private void NewTransazione()
    {
        Console.WriteLine("Enter user name");
        var name = _view.GeInput();
        
        User UserToSelect = null;
        foreach(var user in _db.Users)
        {
            if(user.Name == name)
            {
                UserToSelect = user;
                break;
            }
        }
        Console.WriteLine("Enter abbonamento type");
        var type = _view.GeInput();
        Abbonamento AbbToSelect = null;
        foreach(var abb in _db.Abbonamenti)
        {
            if(abb.Name == type)
            {
                AbbToSelect = abb;
                break;
            }
        }        
        _db.Transazioni.Add( new Transazione { User = UserToSelect, Data = DateOnly.FromDateTime(DateTime.Now), Type = AbbToSelect});
        _db.SaveChanges();
    }

    private void DeleteAbbonamento()
    {
        Console.WriteLine("Enter Abbonamento name");
        var name = _view.GeInput();
        Abbonamento AbbToDelete = null;
        foreach(var abb in _db.Abbonamenti)
        {
            if(abb.Name == name)
            {
                AbbToDelete = abb;
                break;
            }
        }
        if(AbbToDelete != null)
        {
            _db.Abbonamenti.Remove(AbbToDelete);
            _db.SaveChanges();
        }
    }

    private void NewAbbonamenti()
    {
        Console.WriteLine("Enter abbonamento type");
        var name = _view.GeInput();
        Console.WriteLine("Insert price:");
        var price = Convert.ToDecimal(_view.GeInput());
        _db.Abbonamenti.Add(new Abbonamento { Name = name, Price = price});
        _db.SaveChanges();
    }

    private void ShowAbbonamenti()
    {
        var abbonamento = _db.Abbonamenti.ToList();
        _view.ShowAbbonamenti(abbonamento);
    }

    private void AddUser()
    {
        Console.WriteLine("Enter user name");
        var name = _view.GeInput();
        _db.Users.Add(new User { Name = name, Enable = true});
        _db.SaveChanges();
    }
    private void ShowUser(bool enable)
    {
        var users = _db.Users.ToList();
        _view.ShowUsers(users, enable);
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
    private void ToggleUser()
    {
        Console.WriteLine("Enter user name");
        var name = _view.GeInput();
        User UserToToggle = null;
        foreach(var user in _db.Users)
        {
            if(user.Name == name)
            {
                UserToToggle = user;
                break;
            }
        }
        if(UserToToggle != null)
        {
            UserToToggle.Enable = !UserToToggle.Enable;
            _db.SaveChanges();
        }
    }
}