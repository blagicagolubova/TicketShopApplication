using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketShop.Repository.Migrations
{
    public partial class emailModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketInOrder_Tickets_OrderId",
                table: "TicketInOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketInOrder_Order_TicketId",
                table: "TicketInOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketInOrder",
                table: "TicketInOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "TicketInOrder",
                newName: "TicketInOrders");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_TicketInOrder_OrderId",
                table: "TicketInOrders",
                newName: "IX_TicketInOrders_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketInOrders",
                table: "TicketInOrders",
                columns: new[] { "TicketId", "OrderId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EmailMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MailTo = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailMessages", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInOrders_Tickets_OrderId",
                table: "TicketInOrders",
                column: "OrderId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInOrders_Orders_TicketId",
                table: "TicketInOrders",
                column: "TicketId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketInOrders_Tickets_OrderId",
                table: "TicketInOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketInOrders_Orders_TicketId",
                table: "TicketInOrders");

            migrationBuilder.DropTable(
                name: "EmailMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketInOrders",
                table: "TicketInOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "TicketInOrders",
                newName: "TicketInOrder");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_TicketInOrders_OrderId",
                table: "TicketInOrder",
                newName: "IX_TicketInOrder_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Order",
                newName: "IX_Order_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketInOrder",
                table: "TicketInOrder",
                columns: new[] { "TicketId", "OrderId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInOrder_Tickets_OrderId",
                table: "TicketInOrder",
                column: "OrderId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInOrder_Order_TicketId",
                table: "TicketInOrder",
                column: "TicketId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
