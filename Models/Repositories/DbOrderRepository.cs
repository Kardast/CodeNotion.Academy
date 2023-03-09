using System.Diagnostics;
using CodeNotion.Academy.OrderScheduling.Database;
using Microsoft.EntityFrameworkCore;

namespace CodeNotion.Academy.OrderScheduling.Models.Repositories;

public class DbOrderRepository : IDbOrderRepository
{
    private DatabaseContext db;
    
    public DbOrderRepository(DatabaseContext _db)
    {
        db = _db;
    }
    
    private readonly Stopwatch _stopwatch = new Stopwatch();

    public void StartTime()
    {
        _stopwatch.Start();
    }
    
    public void EndTime()
    {
        _stopwatch.Stop();
        Console.WriteLine($"Time taken: {_stopwatch.ElapsedMilliseconds}ms");
    }
    
    public Order GetById(int id)
    {
        return db.Orders.Where(o => o.Id == id).FirstOrDefault();
    }
    
    public void Create(Order order)
    {
        // Save order to database
        db.Orders.Add(order);
        db.SaveChanges();
    }
    
    public List<Order> All()
    {
        return db.Orders.ToList();
    }

    public void Update(Order order, Order modifiedOrder)
    {
        order.Customer = modifiedOrder.Customer;
        order.Order_number = modifiedOrder.Order_number;
        order.Cutting_date = modifiedOrder.Cutting_date;
        order.Preparation_date = modifiedOrder.Preparation_date;
        order.Bending_date = modifiedOrder.Bending_date;
        order.Assembly_date = modifiedOrder.Assembly_date;

        db.SaveChanges();
    }
    
    public void Delete(Order order)
    {
        db.Orders.Remove(order);
        db.SaveChanges();
    }
}