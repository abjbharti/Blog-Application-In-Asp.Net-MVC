namespace AbhijeetMvcTestBlogApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Blogs", name: "Users_UserId", newName: "User_UserId");
            RenameIndex(table: "dbo.Blogs", name: "IX_Users_UserId", newName: "IX_User_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Blogs", name: "IX_User_UserId", newName: "IX_Users_UserId");
            RenameColumn(table: "dbo.Blogs", name: "User_UserId", newName: "Users_UserId");
        }
    }
}
