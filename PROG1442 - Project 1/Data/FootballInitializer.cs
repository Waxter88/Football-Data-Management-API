using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PROG1442___Project_1.Models;

namespace PROG1442___Project_1.Data
{
    public static class FootballInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            FootballContext context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<FootballContext>();
            try
            {
                //DELETE database if need to apply migration
                context.Database.EnsureDeleted();
                //apply migration
                context.Database.Migrate();


                if (!context.Leagues.Any())
                {
                    context.Leagues.AddRange(
                        new League
                        {
                            ID = "QB",
                            Name = "Qb-League"
                        },
                        new League
                        {
                            ID = "WR",
                            Name = "WR-League"
                        },
                        new League
                        {
                            ID = "TE",
                            Name = "TE-League"
                        },
                        new League
                        {
                            ID = "RB",
                            Name = "test"
                        });

                    context.SaveChanges();
                }

                if (!context.Teams.Any())
                {
                    context.Teams.AddRange(
                        new Team
                        {
                            Name = "Patriots",
                            Budget = 1000,
                            LeagueID = "QB",
                        },
                        new Team
                        {
                            Name = "Chiefs",
                            Budget = 1000,
                            LeagueID = "QB",
                            
                        },
                        new Team
                        {
                            Name = "Packers",
                            Budget = 1000,
                            LeagueID = "WR",
                            
                        },
                        new Team
                        {
                            Name = "Saints",
                            Budget = 1000,
                            LeagueID = "WR",
                            
                        },
                        new Team
                        {
                            Name = "Seahawks",
                            Budget = 1000,
                            LeagueID = "TE",
                            
                        },
                        new Team
                        {
                            Name = "Bills",
                            Budget = 1000,
                            LeagueID = "RB",
                            
                        });

                    context.SaveChanges();

                }

                if (!context.Players.Any())
                {
                    context.Players.AddRange(
                        new Player
                        {
                            FirstName = "Tom",
                            LastName = "Brady",
                            Jersey = "12",
                            DOB = new DateTime(2000, 12, 24),
                            FeePaid = 250.00,
                            EMail = "TomBrady@Mail.com",
                            TeamID = 0
                        },
                        new Player
                        {
                            FirstName = "Jack",
                            LastName = "Pipe",
                            Jersey = "10",
                            DOB = new DateTime(1990, 12, 20),
                            FeePaid = 500,
                            EMail = "JackPipe@mail.com",
                            TeamID = 1
                        });
                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
