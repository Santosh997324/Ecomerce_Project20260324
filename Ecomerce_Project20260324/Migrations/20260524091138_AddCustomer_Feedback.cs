using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecomerce_Project20260324.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomer_Feedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Customer_Feedback_Id",
                table: "Statuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Customer_Feedback_Id",
                table: "BaseProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerFeedbacks",
                columns: table => new
                {
                    Customer_Feedback_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    User_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status_Id = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Images_Upload = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerFeedbacks", x => x.Customer_Feedback_Id);
                    table.ForeignKey(
                        name: "FK_CustomerFeedbacks_AspNetUsers_User_Id",
                        column: x => x.User_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Customer_Feedback_Id",
                table: "Statuses",
                column: "Customer_Feedback_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BaseProducts_Customer_Feedback_Id",
                table: "BaseProducts",
                column: "Customer_Feedback_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFeedbacks_User_Id",
                table: "CustomerFeedbacks",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseProducts_CustomerFeedbacks_Customer_Feedback_Id",
                table: "BaseProducts",
                column: "Customer_Feedback_Id",
                principalTable: "CustomerFeedbacks",
                principalColumn: "Customer_Feedback_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_CustomerFeedbacks_Customer_Feedback_Id",
                table: "Statuses",
                column: "Customer_Feedback_Id",
                principalTable: "CustomerFeedbacks",
                principalColumn: "Customer_Feedback_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseProducts_CustomerFeedbacks_Customer_Feedback_Id",
                table: "BaseProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_CustomerFeedbacks_Customer_Feedback_Id",
                table: "Statuses");

            migrationBuilder.DropTable(
                name: "CustomerFeedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_Customer_Feedback_Id",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_BaseProducts_Customer_Feedback_Id",
                table: "BaseProducts");

            migrationBuilder.DropColumn(
                name: "Customer_Feedback_Id",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "Customer_Feedback_Id",
                table: "BaseProducts");
        }
    }
}
