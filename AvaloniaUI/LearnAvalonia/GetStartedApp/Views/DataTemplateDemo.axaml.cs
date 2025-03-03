using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GetStartedApp.Models;
using System.Collections.Generic;

namespace GetStartedApp.Views;

public partial class DataTemplateDemo : UserControl
{
    public List<Person> People { get; init; }
    public DataTemplateDemo()
    {
        People = new List<Person>()
        {
            new Teacher
            {
                FirstName = "Mr.",
                LastName = "X",
                Age = 55,
                Sex=Sex.Diverse,
                Subject = "My Favorite Subject"
            },
            new Student
            {
                FirstName = "Hello",
                LastName = "World",
                Age = 17,
                Sex= Sex.Male,
                Grade=12
            },
            new Student
            {
                FirstName = "Hello",
                LastName = "Kitty",
                Age = 12,
                Sex= Sex.Female,
                Grade=6
            }
        };
        this.DataContext = this;
        InitializeComponent();
    }
}