namespace UniversitySystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using UniversitySystem.Entities;
    using UniversitySystem.Hasher;

    internal sealed class Configuration : DbMigrationsConfiguration<UniversitySystem.Models.UniversitySystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}
