using Models;

namespace DeliverIT
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new DeliverITDBContext();
            db.Database.EnsureCreated();
        }
    }
}
