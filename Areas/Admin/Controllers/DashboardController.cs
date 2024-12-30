using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UserCRM.DTO;
using UserCRM.Models;

namespace UserCRM.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]")]
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
        get { return _context.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("Id")); }
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

    [HttpGet("UserDetails/{id}")]
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

        ViewBag.user = ActiveUser;
        return View(theUser);
    }

    [HttpGet("AddUserPage")]
    public IActionResult AddUserPage()
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }
        ViewBag.user = ActiveUser;
        return View();
    }

    [HttpPost("AddUser")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddUser(UserDTO user)
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }

        try
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName3 = null;
                if (user.ImageUrl != null && user.ImageUrl.Length > 0)
                {
                    // Validate the file (optional but recommended)
                    var permittedExtensions3 = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension3 = Path.GetExtension(user.ImageUrl.FileName).ToLowerInvariant();

                    if (string.IsNullOrEmpty(extension3) || !permittedExtensions3.Contains(extension3))
                    {
                        ModelState.AddModelError("ImageUrl", "Invalid file type. Only images are allowed.");
                        return View(nameof(UserUpdatePage));
                    }

                    // Create a unique filename to prevent overwriting
                    string fileName3 = Path.GetFileNameWithoutExtension(user.ImageUrl.FileName);
                    uniqueFileName3 = $"{fileName3}_{Guid.NewGuid()}{extension3}";

                    // Combine the path with the Uploads folder
                    string uploadsFolder3 = Path.Combine(_webenv.WebRootPath, "Uploads");

                    // Ensure the Uploads folder exists
                    if (!Directory.Exists(uploadsFolder3))
                    {
                        Directory.CreateDirectory(uploadsFolder3);
                    }

                    // Full path to save the file
                    string filePath = Path.Combine(uploadsFolder3, uniqueFileName3);

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await user.ImageUrl.CopyToAsync(fileStream);
                    }
                }

                PasswordHasher<UserDTO> Hasher = new PasswordHasher<UserDTO>();
                Users users = new Users
                {
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    Password = Hasher.HashPassword(user, user.Password),
                    Phone = user.Phone,
                    Role = user.Role,
                    DOB = user.DOB,
                    ImageUrl = uniqueFileName3 != null ? Path.Combine("Uploads", uniqueFileName3) : null,
                    HireDate = user.HireDate,
                    CreatedAt = DateTime.Now
                };
                _context.Users.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQLEXCEPTION: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
    }

    [HttpGet("UserUpdatePage/{id}")]
    public async Task<IActionResult> UserUpdatePage(int id)
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }
        ViewBag.user = ActiveUser;
        ViewBag.updateUser = user;
        return View();
    }

    [HttpPost("ProcessUpdateUser/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProcessUpdateUser(int id, [FromForm] UpdateUserDTO userDto)
    {
        if (ActiveUser == null)
        {
            ViewBag.ErrorMessage = $"Could not access the page, please login to continue.";
            return RedirectToAction("LoginPage", "Account");
        }

        try
        {
            if (ModelState.IsValid)
            {
                var userToUpdate = await _context.Users.FindAsync(id);
                if (userToUpdate == null)
                {
                    ViewBag.ErrorMessage = $"Could not find the user with the ID: {id}";
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                /*string uploadsFolder = Path.Combine(_webenv.WebRootPath, "Uploads");
                if (imageUrl != null && imageUrl.Length > 0)
                {
                    string uniqueFileName1 = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageUrl.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName1);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageUrl.CopyToAsync(fileStream);
                    }

                    userToUpdate.ImageUrl = uniqueFileName1;
                }*/
                /*string uniqueFileName3 = null;
                if (imageUrl != null && imageUrl.Length > 0)
                {
                    // Validate the file (optional but recommended)
                    var permittedExtensions3 = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension3 = Path.GetExtension(imageUrl.FileName).ToLowerInvariant();

                    if (string.IsNullOrEmpty(extension3) || !permittedExtensions3.Contains(extension3))
                    {
                        ModelState.AddModelError("imageUrl", "Invalid file type. Only images are allowed.");
                        return View(nameof(UserUpdatePage));
                    }

                    // Create a unique filename to prevent overwriting
                    string fileName3 = Path.GetFileNameWithoutExtension(imageUrl.FileName);
                    uniqueFileName3 = $"{fileName3}_{Guid.NewGuid()}{extension3}";

                    // Combine the path with the Uploads folder
                    string uploadsFolder3 = Path.Combine(_webenv.WebRootPath, "Uploads");

                    // Ensure the Uploads folder exists
                    if (!Directory.Exists(uploadsFolder3))
                    {
                        Directory.CreateDirectory(uploadsFolder3);
                    }

                    // Full path to save the file
                    string filePath = Path.Combine(uploadsFolder3, uniqueFileName3);

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageUrl.CopyToAsync(fileStream);
                    }
                    userToUpdate.ImageUrl = uniqueFileName3;
                }*/

                string uploadsFolder = Path.Combine(_webenv.WebRootPath, "Uploads");
                if (userDto.ImageUrl != null && userDto.ImageUrl.Length > 0)
                {
                    string uniqueFileName1 =
                        Guid.NewGuid().ToString() + "_" + Path.GetFileName(userDto.ImageUrl.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName1);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await userDto.ImageUrl.CopyToAsync(fileStream);
                    }

                    userToUpdate.ImageUrl = uniqueFileName1;
                }

                userToUpdate.FirstName = userDto.FirstName;
                userToUpdate.MiddleName = userDto.MiddleName;
                userToUpdate.LastName = userDto.LastName;
                userToUpdate.Email = userDto.Email;
                userToUpdate.UserName = userDto.UserName;
                userToUpdate.DOB = userDto.DOB;
                userToUpdate.HireDate = userDto.HireDate;
                userToUpdate.Phone = userDto.Phone;
                userToUpdate.Role = userDto.Role;
                userToUpdate.UpdatedAt = DateTime.Now;
                _context.Users.Update(userToUpdate);
                await _context.SaveChangesAsync();
                ViewBag.user = ActiveUser;
                ViewBag.SuccessMessage = $"Success! Updated the user with the ID: {id}";
                return RedirectToAction("UserDetails", "Dashboard", new { area = "Admin", id = userToUpdate.Id });
            }

            ViewBag.ErrorMessage = $"Could not update the user with the ID: {id}";
            ModelState.AddModelError(string.Empty, $"Could not update the user with the provided ID: {id}");
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }
        catch (DbUpdateConcurrencyException ex)
        {
            ViewBag.ErrorMessage =
                $"Could not update the user with the ID: {id} | DbUpdateConcurrencyException: {ex.Message}";
            Console.WriteLine(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            ViewBag.ErrorMessage =
                $"Could not update the user with the ID: {id} | DbUpdateException: {ex.Message}";
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Could not update the user with the ID: {id} | Exception: {ex.Message}";
            Console.WriteLine(ex.Message);
        }

        return Redirect($"Admin/Dashboard/UserDetails/{id}");
    }

    [HttpDelete("UserDelete/{id}")]
    public async Task<IActionResult> UserDelete(int id)
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        ViewBag.user = ActiveUser;
        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
    }

    [HttpGet("JobUserTaskDetails/{id}")]
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
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

        ViewBag.user = ActiveUser;
        return View(jobUserTask);
    }

    [HttpGet("CampaignUserTaskDetails/{id}")]
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
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

        ViewBag.user = ActiveUser;
        return View(campaignUserTask);
    }

    [HttpGet("CustomerDetails/{id}")]
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
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

        ViewBag.user = ActiveUser;
        return View(customer);
    }

    [HttpGet("OrderDetails/{id}")]
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
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

        ViewBag.user = ActiveUser;
        return View(order);
    }

    [HttpGet("ProductDetails/{id}")]
    public async Task<IActionResult> ProductDetails(int id)
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }

        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        if (product == null)
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

        ViewBag.user = ActiveUser;
        return View(product);
    }

    [HttpGet("JobDetails/{id}")]
    public async Task<IActionResult> JobDetails(int id)
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }
        var job = await _context.Jobs
            .Include(j => j.CustomerJobs)
            .ThenInclude(j => j.Customers)
            .Include(j => j.JobUserNotes)
            .ThenInclude(ju => ju.User)
            .Include(j => j.JobUserNotes)
            .ThenInclude(ju => ju.Notes)
            .Include(jt => jt.JobUserTasks)
            .ThenInclude(jt => jt.User)
            .Include(jt => jt.JobUserTasks)
            .ThenInclude(jt => jt.Task)
            .FirstOrDefaultAsync(j => j.JobId == id);
        if (job == null)
        {
            ViewBag.ErrorMessage = "Job not found";
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }
        ViewBag.user = ActiveUser;
        return View(job);
    }

    [HttpGet("CustomerJobDetails/{id}")]
    public async Task<IActionResult> CustomerJobDetails(int id)
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }
        var customerJob = await _context.CustomerJobs
            .Include(c => c.Customers)
            .Include(c => c.Jobs)
            .FirstOrDefaultAsync(c => c.JobId == id);
        if (customerJob == null)
        {
            ViewBag.ErrorMessage = "Customer Job not found";
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }
        return View(customerJob);
    }
    
    [HttpGet("JobUserNotesDetails/{id}")]
    public async Task<IActionResult> JobUserNotesDetails(int id)
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }
        var jobNotes = await _context.JobUserNotes
            .Include(ju => ju.Notes)
            .Include(ju => ju.User)
            .Include(ju => ju.Job)
            .FirstOrDefaultAsync(c => c.JobId == id);
        if (jobNotes == null)
        {
            ViewBag.ErrorMessage = "Job user note not found";
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }
        return View(jobNotes);
    }
    
    
    
    
    [HttpGet("ProductUpdatePage/{id}")]
    public async Task<IActionResult> ProductUpdatePage(int id)
    {
        if (ActiveUser == null)
        {
            return RedirectToAction("LoginPage", "Account");
        }
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        if (product == null)
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }
        ViewBag.user = ActiveUser;
        ViewBag.updateProduct = product;
        return View();
    }

    [HttpPost("ProcessUpdateProduct/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProcessUpdateProduct(int id, [FromForm] UpdateProductDTO productDto)
    {
        if (ActiveUser == null)
        {
            ViewBag.ErrorMessage = $"Could not access the page, please login to continue.";
            return RedirectToAction("LoginPage", "Account");
        }

        try
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
                if (product == null)
                {
                    ViewBag.ErrorMessage = $"Could not find the product with the ID: {id}";
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                string uploadsFolder = Path.Combine(_webenv.WebRootPath, "Uploads/products");
                if (productDto.ImageUrl != null && productDto.ImageUrl.Length > 0)
                {
                    string uniqueFileName1 =
                        Guid.NewGuid().ToString() + "_" + Path.GetFileName(productDto.ImageUrl.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName1);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await productDto.ImageUrl.CopyToAsync(fileStream);
                    }

                    product.ImageUrl = uniqueFileName1;
                }

                product.Name = productDto.Name;
                product.Price = productDto.Price;
                product.Description = productDto.Description;
                product.UPC = productDto.UPC;
                product.Currency = productDto.Currency;
                product.QuantityInStock = productDto.QuantityInStock;
                product.Category = productDto.Category;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                ViewBag.user = ActiveUser;
                ViewBag.SuccessMessage = $"Success! Updated the product with the ID: {id}";
                return RedirectToAction("ProductDetails", "Dashboard", new { area = "Admin", id = product.ProductId });
            }

            ViewBag.ErrorMessage = $"Could not update the user with the ID: {id}";
            ModelState.AddModelError(string.Empty, $"Could not update the user with the provided ID: {id}");
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }
        catch (DbUpdateConcurrencyException ex)
        {
            ViewBag.ErrorMessage =
                $"Could not update the user with the ID: {id} | DbUpdateConcurrencyException: {ex.Message}";
            Console.WriteLine(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            ViewBag.ErrorMessage =
                $"Could not update the user with the ID: {id} | DbUpdateException: {ex.Message}";
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Could not update the user with the ID: {id} | Exception: {ex.Message}";
            Console.WriteLine(ex.Message);
        }

        return Redirect($"Admin/Dashboard/ProductDetails/{id}");
    }
    
    
    
}