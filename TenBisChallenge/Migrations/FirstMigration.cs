namespace TenBisChallenge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        TxId = c.Int(nullable: false, identity: true),
                        OrderTime = c.DateTime(nullable: false),
                        TxValue = c.Double(nullable: false),
                        MoneyCardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TxId)
                .ForeignKey("dbo.MoneyCards", t => t.MoneyCardId, cascadeDelete: true)
                .Index(t => t.MoneyCardId);
            
            CreateTable(
                "dbo.MoneyCards",
                c => new
                    {
                        MoneyCardId = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        LastFourDigits = c.String(),
                    })
                .PrimaryKey(t => t.MoneyCardId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserFullName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Banks", "MoneyCardId", "dbo.MoneyCards");
            DropForeignKey("dbo.MoneyCards", "UserId", "dbo.Users");
            DropForeignKey("dbo.MoneyCards", "CompanyId", "dbo.Companies");
            DropIndex("dbo.MoneyCards", new[] { "UserId" });
            DropIndex("dbo.MoneyCards", new[] { "CompanyId" });
            DropIndex("dbo.Banks", new[] { "MoneyCardId" });
            DropTable("dbo.Users");
            DropTable("dbo.Companies");
            DropTable("dbo.MoneyCards");
            DropTable("dbo.Banks");
        }
    }
}
