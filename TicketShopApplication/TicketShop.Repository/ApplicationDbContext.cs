using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.Identity;
using TicketShop.Domain.DomainModels;

namespace TicketShop.Repository
{
    public class ApplicationDbContext : IdentityDbContext<TicketShopApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<TicketInShoppingCart> TicketsInShoppingCarts { get; set; }
        public virtual DbSet<TicketInOrder> TicketInOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Ticket>()
               .Property(z => z.Id)
               .ValueGeneratedOnAdd();

            builder.Entity<ShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Movie>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Order>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<TicketInShoppingCart>()
                .HasKey(z => new { z.TicketId, z.ShoppingCartId });

            builder.Entity<Movie>()
                .HasMany(c => c.Tickets)
                .WithOne(e => e.Movie);

            builder.Entity<Ticket>()
                .HasOne(e => e.Movie)
                .WithMany(c => c.Tickets)
                .HasForeignKey(z => z.MovieId);

            builder.Entity<TicketShopApplicationUser>()
                .HasMany(c => c.Orders)
                .WithOne(e => e.User);

            builder.Entity<Order>()
                .HasOne(e => e.User)
                .WithMany(c => c.Orders)
                .HasForeignKey(z => z.UserId);

            builder.Entity<TicketInShoppingCart>()
                .HasOne(z => z.Ticket)
                .WithMany(z => z.TicketInShoppingCarts)
                .HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<TicketInShoppingCart>()
                .HasOne(z => z.ShoppingCart)
                .WithMany(z => z.TicketInShoppingCarts)
                .HasForeignKey(z => z.TicketId);

            builder.Entity<ShoppingCart>()
                .HasOne<TicketShopApplicationUser>(z => z.Owner)
                .WithOne(z => z.UserCart)
                .HasForeignKey<ShoppingCart>(z => z.OwnerId);

            builder.Entity<TicketInOrder>()
                .HasKey(z => new { z.TicketId, z.OrderId });

            builder.Entity<TicketInOrder>()
                .HasOne(z => z.OrderedTicket)
                .WithMany(z => z.Orders)
                .HasForeignKey(z => z.OrderId);

            builder.Entity<TicketInOrder>()
                .HasOne(z => z.UserOrder)
                .WithMany(z => z.Tickets)
                .HasForeignKey(z => z.TicketId);

        }
    }
}
