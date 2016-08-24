using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesisApplication.Migrations
{
    public partial class _0807_2028 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "visualization_flowDirectionX",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_flowDirectionY",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_flowDirectionZ",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_flowSpeed",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_normal1x",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_normal1y",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_normal1z",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_normal2x",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_normal2y",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_normal2z",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_normal3x",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_normal3y",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_normal3z",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "visualization_numCuts",
                table: "userCase",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "visualization_numProbes",
                table: "userCase",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "visualization_operatingPressure",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_operatingtemperature",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_point1x",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_point1y",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_point1z",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_point2x",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_point2y",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_point2z",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_point3x",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_point3y",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_point3z",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_probe1x",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_probe1y",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_probe1z",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_probe2x",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_probe2y",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_probe2z",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_probe3x",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_probe3y",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "visualization_probe3z",
                table: "userCase",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "visualization_flowDirectionX",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_flowDirectionY",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_flowDirectionZ",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_flowSpeed",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_normal1x",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_normal1y",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_normal1z",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_normal2x",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_normal2y",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_normal2z",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_normal3x",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_normal3y",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_normal3z",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_numCuts",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_numProbes",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_operatingPressure",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_operatingtemperature",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_point1x",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_point1y",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_point1z",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_point2x",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_point2y",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_point2z",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_point3x",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_point3y",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_point3z",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_probe1x",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_probe1y",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_probe1z",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_probe2x",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_probe2y",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_probe2z",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_probe3x",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_probe3y",
                table: "userCase");

            migrationBuilder.DropColumn(
                name: "visualization_probe3z",
                table: "userCase");
        }
    }
}
