using LearnEF;
using System;
using System.Linq;

using var db = new BloggingContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

Console.WriteLine("Create a new Author Bob");
Author bob = new Author
{
    Name = "Bob",
    Email = "bob@test.com"
};
db.Add(bob);
db.SaveChanges(); //Bob Created

var bobFromDatabase = db.Authors.Where(a => a.Name == "Bob").First();

// Create
Console.WriteLine("Inserting a new blog");
Blog aNewBlog = new Blog { Url = "http://blogs.msdn.com/adonet"};
bobFromDatabase.Blogs.Add(aNewBlog);
//db.Blogs.Add(aNewBlog);
db.SaveChanges();

// Read
Console.WriteLine("Querying for a blog");
var blog = db.Blogs
    .OrderBy(b => b.BlogId)
    .First();

// Update
Console.WriteLine("Updating the blog and adding a post");
blog.Url = "https://devblogs.microsoft.com/dotnet";
blog.Posts.Add(
    new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
blog.Posts.Add(
    new Post { Title = "Post#2", Content = "Test 123" });
blog.Posts.Add(
    new Post { Title = "Post#3", Content = "Yet Another Post" });
db.SaveChanges();

// Delete
//Console.WriteLine("Delete the blog");
//db.Remove(blog);
//db.SaveChanges();