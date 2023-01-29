using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PlayerDbInitializer : DropCreateDatabaseAlways<SoccerContext>
    {
        protected override void Seed(SoccerContext context)
        {
            var menuItems = new List<MenuItem>
            {
                new MenuItem{ Id = 1, Header = "Main", Url = "/Players/Index", Order = 1 },
                new MenuItem{ Id = 2, Header = "Create Player", Url = "/Players/Create", Order = 2 },
                new MenuItem{ Id = 3, Header = "Create Team", Url = "/Players/CreateTeam", Order = 3 },
                new MenuItem{ Id = 4, Header = "Child 1", Url = "#", Order = 1, ParentId = 2 },
                new MenuItem{ Id = 5, Header = "Child 2", Url = "#", Order = 2, ParentId = 2 },
                new MenuItem{ Id = 6, Header = "Child 1", Url = "#", Order = 1, ParentId = 4 },
                new MenuItem{ Id = 7, Header = "Child 2", Url = "#", Order = 2, ParentId = 4 },
            };

            context.MenuItems.AddRange(menuItems);
            context.SaveChanges();




            base.Seed(context);
            /*Player s1 = new Player { Id = 1, Name = "Jhon", Age = 20, Position = "Midfielder", TeamId = 1 };
            Player s2 = new Player { Id = 2, Name = "Alex", Age = 20, Position = "Midfielder", TeamId = 1 };
            Player s3 = new Player { Id = 3, Name = "Smith", Age = 20, Position = "Midfielder", TeamId = 2 };
            Player s4 = new Player { Id = 4, Name = "Anna", Age = 20, Position = "Midfielder", TeamId = 2 };
            Player s5 = new Player { Id = 5, Name = "Ruslan", Age = 20, Position = "Midfielder", TeamId = 2 };
            Player s6 = new Player { Id = 6, Name = "Adam", Age = 20, Position = "Midfielder", TeamId = 1 };

            context.Players.Add(s1);
            context.Players.Add(s2);
            context.Players.Add(s3);
            context.Players.Add(s4);
            context.Players.Add(s5);
            context.Players.Add(s6);

            base.Seed(context);*/
        }
    }
}