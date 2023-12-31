﻿namespace AbhijeetMvcTestBlogApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        IdUser = c.Guid(nullable: false),
                        Users_UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Users_UserId)
                .Index(t => t.Users_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blogs", "Users_UserId", "dbo.Users");
            DropIndex("dbo.Blogs", new[] { "Users_UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Blogs");
        }
    }
}