using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecomerce_Project20260324.Migrations
{
    /// <inheritdoc />
    public partial class updateCustomer_Feedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseProducts_CustomerFeedbacks_Customer_Feedback_Id",
                table: "BaseProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_CustomerFeedbacks_Customer_Feedback_Id",
                table: "Statuses");

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

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFeedbacks_Product_Id",
                table: "CustomerFeedbacks",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFeedbacks_Status_Id",
                table: "CustomerFeedbacks",
                column: "Status_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerFeedbacks_BaseProducts_Product_Id",
                table: "CustomerFeedbacks",
                column: "Product_Id",
                principalTable: "BaseProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerFeedbacks_Statuses_Status_Id",
                table: "CustomerFeedbacks",
                column: "Status_Id",
                principalTable: "Statuses",
                principalColumn: "Status_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerFeedbacks_BaseProducts_Product_Id",
                table: "CustomerFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerFeedbacks_Statuses_Status_Id",
                table: "CustomerFeedbacks");

            migrationBuilder.DropIndex(
                name: "IX_CustomerFeedbacks_Product_Id",
                table: "CustomerFeedbacks");

            migrationBuilder.DropIndex(
                name: "IX_CustomerFeedbacks_Status_Id",
                table: "CustomerFeedbacks");

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

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Customer_Feedback_Id",
                table: "Statuses",
                column: "Customer_Feedback_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BaseProducts_Customer_Feedback_Id",
                table: "BaseProducts",
                column: "Customer_Feedback_Id");

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
    }
}
