using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserCRM.DTO;
using UserCRM.Models;

namespace UserCRM.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webenv;
    public DashboardController(ApplicationDbContext context, IWebHostEnvironment webenv)
    {
        _context = context;
        _webenv = webenv;
    }
    private Users? ActiveUser
    {
        get
        {
            return _context.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("Id"));
        }
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }
        ViewBag.Jobs = await _context.Jobs.ToListAsync();
        ViewBag.Users = await _context.Users.ToListAsync();
        ViewBag.Customers = await _context.Customers.ToListAsync();
        ViewBag.Products = await _context.Products.ToListAsync();
        ViewBag.Campaigns = await _context.Campaigns.ToListAsync();
        ViewBag.JobCount = await _context.Jobs.CountAsync();
        ViewBag.UserCount = await _context.Users.CountAsync();
        ViewBag.CustomerCount = await _context.Customers.CountAsync();
        ViewBag.ProductCount = await _context.Products.CountAsync();
        ViewBag.user = ActiveUser;
        return View();
    }

    [HttpGet("Admin/Dashboard/UserDetails/{id}")]
    public async Task<IActionResult> UserDetails(int id)
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }
        var theUser = await _context.Users
            .Include(jt => jt.JobUserTasks)
            .ThenInclude(jt => jt.Job)
            .Include(jt => jt.JobUserTasks)
            .ThenInclude(jt => jt.Task)
            .Include(jt => jt.JobUserNotes)
            .ThenInclude(jt => jt.Job)
            .Include(jt => jt.JobUserNotes)
            .ThenInclude(jt => jt.Notes)
            .Include(jt => jt.CampaignUserTasks)
            .ThenInclude(jt => jt.Campaign)
            .Include(jt => jt.CampaignUserTasks)
            .ThenInclude(jt => jt.Task)
            .Include(jt => jt.CampaignUserNotes)
            .ThenInclude(jt => jt.Campaign)
            .Include(jt => jt.CampaignUserNotes)
            .ThenInclude(jt => jt.Notes)
            .Include(jt => jt.UserTaskNotes)
            .ThenInclude(jt => jt.Task)
            .Include(jt => jt.UserTaskNotes)
            .ThenInclude(jt => jt.Note)
            .FirstOrDefaultAsync(u => u.Id == id);
        if (theUser == null) 
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

        ViewBag.Details = theUser;
        ViewBag.user = ActiveUser;
        return View(theUser);
    }
    
    [HttpPost("{id}")]
    public async Task<IActionResult> ProcessUpdateUser(int id, string firstName, string middleName, string lastName, string email, string userName, DateTime dob, DateTime hireDate, bool isActive, IFormFile imageUrl)
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }

        try
        {
            if (ModelState.IsValid)
            {
                var userToUpdate = await _context.Users.Where(u => u.Id == id).SingleOrDefaultAsync();
                if (userToUpdate != null)
                {
                    string uploadsFolder = Path.Combine(_webenv.WebRootPath, "Uploads");
                    if (imageUrl != null && imageUrl.Length > 0)
                    {
                        string uniqueFileName1 = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageUrl.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName1);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageUrl.CopyToAsync(fileStream);
                        }
                        userToUpdate.ImageUrl = uniqueFileName1;
                    }

                    userToUpdate.FirstName = firstName;
                    userToUpdate.MiddleName = middleName;
                    userToUpdate.LastName = lastName;
                    userToUpdate.Email = email;
                    userToUpdate.UserName = userName;
                    userToUpdate.DOB = dob;
                    userToUpdate.HireDate = hireDate;
                    userToUpdate.IsActive = isActive;
                    _context.Users.Update(userToUpdate);
                    await _context.SaveChangesAsync();
                    ViewBag.user = ActiveUser;
                    return RedirectToAction("UserDetails", "Dashboard", new { area = "Admin", Id = id });
                }
                else
                {
                    Console.WriteLine("Cannot update user");
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
            }
        }
        catch (DbUpdateConcurrencyException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return RedirectToAction("Index", "Dashboard", new {area = "Admin"});
    }

    [HttpDelete("Admin/Dashboard/UserDelete/{id}")]
    public async Task<IActionResult> UserDelete(int id)
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return RedirectToAction("Index", "Dashboard", new {area = "Admin"});
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        ViewBag.user = ActiveUser;
        return RedirectToAction("Index", "Dashboard", new {area = "Admin"});
    }

    [HttpGet("Admin/Dashboard/JobUserTaskDetails/{id}")]
    public async Task<IActionResult> JobUserTaskDetails(int id)
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }
        var jobUserTask = await _context.JobUserTasks
            .Include(jt => jt.Job)
            .Include(jt => jt.Task)
            .Include(jt => jt.User)
            .FirstOrDefaultAsync(jut => jut.JobUserTaskId == id);
        if (jobUserTask == null)
        {
            return RedirectToAction("Index", "Dashboard", new {area = "Admin"});
        }
        ViewBag.user = ActiveUser;
        return View(jobUserTask);
    }

    [HttpGet("Admin/Dashboard/CampaignUserTaskDetails/{id}")]
    public async Task<IActionResult> CampaignUserTaskDetails(int id)
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }
        var campaignUserTask = await _context.CampaignUserTasks
            .Include(jt => jt.Campaign)
            .Include(jt => jt.Task)
            .Include(jt => jt.User)
            .FirstOrDefaultAsync(jut => jut.CampaignUserTaskId == id);
        if (campaignUserTask == null)
        {
            return RedirectToAction("Index", "Dashboard", new {area = "Admin"});
        }
        ViewBag.user = ActiveUser;
        return View(campaignUserTask);
    }

    [HttpGet("Admin/Dashboard/CustomerDetails/{id}")]
    public async Task<IActionResult> CustomerDetails(int id)
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }
        var customer = await _context.Customers
            .Include(c => c.CustomerJobs)
            .ThenInclude(c => c.Jobs)
            .Include(c => c.CustomerOrders)
            .ThenInclude(c => c.Order)
            .Include(c => c.CustomerUsers)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(c => c.CustomerId == id);
        if (customer == null)
        {
            return RedirectToAction("Index", "Dashboard", new {area = "Admin"});
        }
        ViewBag.user = ActiveUser;
        return View(customer);
    }

    [HttpGet("Admin/Dashboard/OrderDetails/{id}")]
    public async Task<IActionResult> OrderDetails(int id)
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }

        var order = await _context.Orders
            .Include(o => o.CustomerOrders)
            .ThenInclude(c => c.Customer)
            .Include(c => c.OrdersOrderItems)
            .ThenInclude(c => c.OrderItem)
            .ThenInclude(c => c.Products)
            .FirstOrDefaultAsync(c => c.OrderId == id);
        if (order == null)
        {
            return RedirectToAction("Index", "Dashboard", new {area = "Admin"});
        }
        ViewBag.user = ActiveUser;
        return View(order);
    }
    [HttpGet("Admin/Dashboard/ProductDetails/{id}")]
    public async Task<IActionResult> ProductDetails(int id)
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }

        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        if (product == null)
        {
            return RedirectToAction("Index", "Dashboard", new {area = "Admin"});
        }
        ViewBag.user = ActiveUser;
        return View(product);
    }
}