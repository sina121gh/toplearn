using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopLearn.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class renamed_some_fields_for_courseEpisode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EpisodeTime",
                table: "CourseEpisodes",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "EpisodeFileName",
                table: "CourseEpisodes",
                newName: "FileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "CourseEpisodes",
                newName: "EpisodeTime");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "CourseEpisodes",
                newName: "EpisodeFileName");
        }
    }
}
