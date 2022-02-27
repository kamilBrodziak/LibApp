using System;
using System.Linq;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                SeedMembershipTypes(context);
                SeedCustomers(context);
                SeedBooks(context);

                context.SaveChanges();
            }
        }

        private static void SeedMembershipTypes(ApplicationDbContext context)
        {
            if(!context.MembershipTypes.Any())
            {
                context.MembershipTypes.AddRange(
                    new MembershipType
                    {
                        Id = 1,
                        SignUpFee = 0,
                        Name = "Pay as you go",
                        DurationInMonths = 0,
                        DiscountRate = 0
                    },
                    new MembershipType
                    {
                        Id = 2,
                        SignUpFee = 30,
                        Name = "Monthly",
                        DurationInMonths = 1,
                        DiscountRate = 10
                    },
                    new MembershipType
                    {
                        Id = 3,
                        SignUpFee = 90,
                        Name = "Quaterly",
                        DurationInMonths = 3,
                        DiscountRate = 15
                    },
                    new MembershipType
                    {
                        Id = 4,
                        SignUpFee = 300,
                        Name = "Yearly",
                        DurationInMonths = 12,
                        DiscountRate = 20
                    });
            }
            
        }

        private static void SeedCustomers(ApplicationDbContext context)
        {
            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                new Customer
                {
                    Name = "Kamil Brodziak",
                    HasNewsletterSubscribed = false,
                    MembershipTypeId = 1,
                    Birthdate = new DateTime(1998, 09, 18)
                },
                new Customer
                {
                    Name = "Janusz Januszowy",
                    HasNewsletterSubscribed = true,
                    MembershipTypeId = 2,
                    Birthdate = new DateTime(2001, 06, 25)
                });
                Console.WriteLine("Customers already seeded");
            }
        }

        private static void SeedBooks(ApplicationDbContext context)
        {
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                new Book
                {
                    Name = "Wiedźmin: Krew Elfów",
                    AuthorName = "Andrzej Sapkowski",
                    GenreId = 6,
                    DateAdded = new DateTime(2022, 02, 27),
                    ReleaseDate = new DateTime(1994, 02, 25),
                    NumberInStock = 15,
                    NumberAvailable = 4,
                },
                new Book
                {
                    Name = "Lord of the rings: The Return of the King",
                    AuthorName = "J.R.R. Tolkien",
                    GenreId = 6,
                    DateAdded = new DateTime(2022, 02, 11),
                    ReleaseDate = new DateTime(1955, 10, 20),
                    NumberInStock = 17,
                    NumberAvailable = 12,
                });
                Console.WriteLine("Books already seeded");
            }
        }
    }
}