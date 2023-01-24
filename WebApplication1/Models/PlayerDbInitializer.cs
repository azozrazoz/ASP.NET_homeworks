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
            Player s1 = new Player { Id = 1, Name = "Jhon", Age = 20, Position = "Midfielder", TeamId = 1 };
            Player s2 = new Player { Id = 2, Name = "Alex", Age = 20, Position = "Midfielder", TeamId = 1 };
            Player s3 = new Player { Id = 3, Name = "Smith", Age = 20, Position = "Midfielder", TeamId = 1 };
            Player s4 = new Player { Id = 4, Name = "Anna", Age = 20, Position = "Midfielder", TeamId = 1 };
            Player s5 = new Player { Id = 5, Name = "Ruslan", Age = 20, Position = "Midfielder", TeamId = 1 };
            Player s6 = new Player { Id = 6, Name = "Adam", Age = 20, Position = "Midfielder", TeamId = 1 };

            context.Players.Add(s1);
            context.Players.Add(s2);
            context.Players.Add(s3);
            context.Players.Add(s4);
            context.Players.Add(s5);
            context.Players.Add(s6);

            base.Seed(context);
        }
    }
}