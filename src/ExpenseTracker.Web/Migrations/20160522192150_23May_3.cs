using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTracker.Web.Migrations
{
    public partial class _23May_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseItem_Invoice_InvoiceId",
                table: "ExpenseItem");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "ExpenseItem",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseItem_Invoice_InvoiceId",
                table: "ExpenseItem",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseItem_Invoice_InvoiceId",
                table: "ExpenseItem");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "ExpenseItem",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseItem_Invoice_InvoiceId",
                table: "ExpenseItem",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
