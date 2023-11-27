using Microsoft.EntityFrameworkCore;
using ZooApp.Controllers;
using ZooApp.Data;
using ZooApp.Views;

namespace ZooApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ZooAppDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            var options = new DbContextOptionsBuilder<ZooContext>()
                .UseSqlServer(connectionString)
                .Options;

            using (var context = new ZooContext(options))
            {
                var userController = new UserController(context);
                var animalController = new AnimalController(context);
                var zooView = new ZooView();
                var zooController = new ZooController(userController, animalController, zooView);

                zooController.RunMenu();
            }
        }
    }
}