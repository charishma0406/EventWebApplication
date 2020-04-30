using EventApp1.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp1.Data
{
    public class EventContext :DbContext
    {
        //where
        //in the parameters we gave dependency dbcontextoptions for where the sql db information it is depending on the db context
        //for db connection we are giving parameter in constructor and giving parent class constructor also
        public EventContext(DbContextOptions options) : base(options)
        {

        }

        // what to be converted into tables the data the properties we wrote should be converted into tables
        //dbset is the name for the database table.
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<EventLocations> EventLocations { get; set; }
        public DbSet<EventDetails> Event_Details { get; set; }

        //model builder is creating our tables,we need to tell model builder how to create the tables. 
        //override is the change the behaviour of parent class that is y we are using on model creating method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventType>(e =>
            {
                //id prop
                e.ToTable("EventTypes");
                e.Property(t => t.Id)
                .IsRequired()
                .UseHiLo("Event_Type_hilo");
                //type prop
                e.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(100);
            });

            //for location
            modelBuilder.Entity<EventLocations>(e =>
            {
                //id prop
                e.ToTable("EventLocations");
                e.Property(t => t.Id)
                .IsRequired()
                .UseHiLo("Event_Location_hilo");
                //type prop
                e.Property(t => t.Location)
                .IsRequired()
                .HasMaxLength(100);
            });

            modelBuilder.Entity<EventDetails>(e =>
            {
                //id prop
                e.ToTable("Event_Details");
                e.Property(d => d.Id)
                .IsRequired()
                .UseHiLo("Event_Details_hilo");

                //name prop
                e.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

                //venue prop
                e.Property(d => d.Venue)
                .IsRequired()
                .HasMaxLength(100);

                //price prop
                e.Property(d => d.Price)
                .IsRequired();

                //age prop
                e.Property(d => d.Age)
                .IsRequired();

                //occupancy prop
                e.Property(d => d.Occupancy)
                .IsRequired();

                //event type id this is foreign key relation ship to have relationship with event type table

                e.HasOne(d => d.EventType)
                  .WithMany()
                  .HasForeignKey(d => d.EventTypeId);

            });


        }
    }
}
