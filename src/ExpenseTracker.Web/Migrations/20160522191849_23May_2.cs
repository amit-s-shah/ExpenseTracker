using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTracker.Web.Migrations
{
    public partial class _23May_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReOccurringExpenseItem_Invoice_InvoiceId",
                table: "ReOccurringExpenseItem");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "ReOccurringExpenseItem",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReOccurringExpenseItem_Invoice_InvoiceId",
                table: "ReOccurringExpenseItem",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReOccurringExpenseItem_Invoice_InvoiceId",
                table: "ReOccurringExpenseItem");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "ReOccurringExpenseItem",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ReOccurringExpenseItem_Invoice_InvoiceId",
                table: "ReOccurringExpenseItem",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
