using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Industry_4._0.Data.Migrations
{
    public partial class week9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscussionForum",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostDate = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    TopicTitle = table.Column<string>(nullable: false),
                    MessageContent = table.Column<string>(nullable: false),
                    Like = table.Column<int>(nullable: false),
                    canIncreaseLike = table.Column<bool>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Agree = table.Column<int>(nullable: false),
                    Disagree = table.Column<int>(nullable: false),
                    canIncreaseAgree = table.Column<bool>(nullable: false),
                    canIncreaseDisagree = table.Column<bool>(nullable: false),
                    Heading = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionForum", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscussionForum");
        }
    }
}
