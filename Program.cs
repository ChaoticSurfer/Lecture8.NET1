using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class Program {
    public static void Main(string[] args)
        {
        Console.WriteLine("Hello, World!");

        using (var dbContext = new ApplicationDbContext())
            {
            dbContext.Database.EnsureCreated();

            var blog1 = new Blog() { Name = "Blog1", Description = "Desciprtin1" };
                dbContext.Add(blog1);
                dbContext.SaveChanges();
            foreach (var blog in dbContext.Blogs)
            {
                Console.WriteLine($"blog -> id {blog.Id} name{blog.Name} desc {blog.Description}");
            }

            var post1 = new Post() { Title = "Topuria on top of Makachaev", Content = "Ilia won" };
           // dbContext.Blogs.First(blog => blog.Id == 1);
            post1.BlogId = 1;
            dbContext.Posts.Add(post1);
            dbContext.SaveChanges();

            foreach (var post in dbContext.Posts)
            {
                Console.WriteLine($"post -> id {post.Id} sub: {post.Title} blog {post.Id}, related blog Title: {post.Blog.Name}");
            }

        }
    }
}


public class ApplicationDbContext: DbContext {
    
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source = C:\\Users\\PC\\Desktop\\SabaDotnet\\ConsoleApp2\\database.db");
        base.OnConfiguring(optionsBuilder);
    }
}

public class Blog {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int rating { get; set; } = 0;
    public List<Post> Posts { get; set; } = new();
}
public class Post
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Blog Blog { get; set; }
    public int BlogId { get; set; }
}