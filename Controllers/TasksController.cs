using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Data;
using TaskManagementApp.Models;

namespace TaskManagementApp.Controllers;

public class TasksController : Controller
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        List<TaskItem> tasks = _context.Tasks.ToList();
        return View(tasks);

    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(TaskItem task)
    {
        if(!ModelState.IsValid) 
        {
            return View(task); 
        }
        _context.Tasks.Add(task);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        TaskItem? task = _context.Tasks.Find(id);

        if(task == null)
        {
            return NotFound();
        }

        return View(task); 
    }

    [HttpPost]
    public IActionResult Edit(TaskItem task)
    {
        if(!ModelState.IsValid)
        {
            return View(task);
        } 

        _context.Tasks.Update(task);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        TaskItem? task = _context.Tasks.Find(id);
        if(task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    [HttpPost]
    public IActionResult Delete(TaskItem task)
    {
        TaskItem? existingTask = _context.Tasks.Find(task.Id);

        if(existingTask == null)
        {
            return NotFound();
        }

        _context.Tasks.Remove(existingTask);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
