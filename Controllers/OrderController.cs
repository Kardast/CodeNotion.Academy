using System.Diagnostics;
using CodeNotion.Academy.OrderScheduling.Database;
using CodeNotion.Academy.OrderScheduling.Models;
using CodeNotion.Academy.OrderScheduling.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeNotion.Academy.OrderScheduling.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IDbOrderRepository _orderRepository;
    
    // Dependency Injection
    public OrderController (IDbOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    // CREATE
    [HttpPost]
    public IActionResult CreateOrder(Order order)
    {
        _orderRepository.StartTime();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _orderRepository.Create(order);
        _orderRepository.EndTime();
        return Ok(order);
    }
    
    // READ
    [Route("[action]")]
    [HttpGet]
    public IActionResult List()
    {
        _orderRepository.StartTime();
        List<Order> orders = _orderRepository.All();

        _orderRepository.EndTime();
        return Ok(orders);
    }
    
    // UPDATE
    [Route("[action]/{id}")]
    [HttpPost]
    public IActionResult Update(int id, Order order)
    {
        _orderRepository.StartTime();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Order orderFromDb = _orderRepository.GetById(id);
        _orderRepository.Update(orderFromDb, order);
        _orderRepository.EndTime();
        return Ok(order);
    }
    
    // DELETE
    [Route("[action]/{id}")]
    [HttpPost]
    public IActionResult Delete(int id)
    {
        _orderRepository.StartTime();
        Order order = _orderRepository.GetById(id);
        _orderRepository.Delete(order);
        _orderRepository.EndTime();
        return Ok(order);
    }
}