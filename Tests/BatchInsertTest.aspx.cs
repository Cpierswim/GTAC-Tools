using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class BatchInsertTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string connectionString = GetConnectionString();
        // Open a connection to the AdventureWorks database.
        using (SqlConnection connection =
                   new SqlConnection(connectionString))
        {
            connection.Open();

            // Perform an initial count on the destination table.
            SqlCommand commandRowCount = new SqlCommand(
                "SELECT COUNT(*) FROM " +
                "Attendance;",
                connection);
            long countStart = System.Convert.ToInt32(
                commandRowCount.ExecuteScalar());
            String message1 = "Starting row count = " + countStart;

            // Create a table with some rows. 
            DataTable newProducts = MakeTable();

            // Create the SqlBulkCopy object. 
            // Note that the column positions in the source DataTable 
            // match the column positions in the destination table so 
            // there is no need to map columns. 
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
            {
                bulkCopy.DestinationTableName =
                    "Attendance";

                try
                {
                    bulkCopy.BatchSize = 100000;
                    // Write from the source to the destination.
                    bulkCopy.WriteToServer(newProducts);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // Perform a final count on the destination 
            // table to see how many rows were added.
            long countEnd = System.Convert.ToInt32(
                commandRowCount.ExecuteScalar());
            Console.WriteLine("Ending row count = {0}", countEnd);
            Console.WriteLine("{0} rows were added.", countEnd - countStart);
            Console.WriteLine("Press Enter to finish.");
            Console.ReadLine();

        }
    }

    private DataTable MakeTable()
    // Create a new DataTable named NewProducts. 
    {
        DataTable newAttendances = new DataTable("NewProducts");

        // Add three column objects to the table. 
        DataColumn AttendanceID = new DataColumn();
        AttendanceID.DataType = System.Type.GetType("System.Int32");
        AttendanceID.ColumnName = "AttendanceID";
        AttendanceID.AutoIncrement = true;
        newAttendances.Columns.Add(AttendanceID);

        DataColumn USAID = new DataColumn();
        USAID.DataType = System.Type.GetType("System.String");
        USAID.ColumnName = "USAID";
        newAttendances.Columns.Add(USAID);

        DataColumn Date = new DataColumn();
        Date.DataType = System.Type.GetType("System.DateTime");
        Date.ColumnName = "Date";
        newAttendances.Columns.Add(Date);

        DataColumn PracticeoftheDay = new DataColumn();
        PracticeoftheDay.DataType = System.Type.GetType("System.Int32");
        PracticeoftheDay.ColumnName = "PracticeoftheDay";
        newAttendances.Columns.Add(PracticeoftheDay);

        DataColumn GroupIDColumn = new DataColumn();
        GroupIDColumn.DataType = System.Type.GetType("System.Int32");
        GroupIDColumn.ColumnName = "GroupID";
        newAttendances.Columns.Add(GroupIDColumn);

        DataColumn AttendanceType = new DataColumn();
        AttendanceType.DataType = System.Type.GetType("System.String");
        AttendanceType.ColumnName = "AttendanceType";
        AttendanceType.MaxLength = 2;
        AttendanceType.AllowDBNull = true;
        newAttendances.Columns.Add(AttendanceType);

        DataColumn Note = new DataColumn();
        Note.DataType = System.Type.GetType("System.String");
        Note.ColumnName = "Note";
        Note.AllowDBNull = true;
        newAttendances.Columns.Add(Note);

        DataColumn Lane = new DataColumn();
        Lane.DataType = System.Type.GetType("System.Int32");
        Lane.ColumnName = "Lane";
        Lane.AllowDBNull = true;
        newAttendances.Columns.Add(Lane);

        DataColumn Yards = new DataColumn();
        Yards.DataType = System.Type.GetType("System.Int32");
        Yards.ColumnName = "Yards";
        Yards.AllowDBNull = true;
        newAttendances.Columns.Add(Yards);

        DataColumn Meters = new DataColumn();
        Meters.DataType = System.Type.GetType("System.Int32");
        Meters.ColumnName = "Meters";
        Meters.AllowDBNull = true;
        newAttendances.Columns.Add(Meters);

        // Create an array for DataColumn objects.
        DataColumn[] keys = new DataColumn[1];
        keys[0] = AttendanceID;
        newAttendances.PrimaryKey = keys;


        //Create 10 random swimmers for
        //List<String> SwimmersUSAIDs = new List<string>();
        //for (int i = 0; i < 10; i++)
        //    SwimmersUSAIDs.Add(RandomUSAIDCreator.GetRandomUSAID(i));
        int GroupID = 7;
        SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        SwimTeamDatabase.SwimmersDataTable Swimmers = SwimmersAdapter.GetSwimmersByGroupID(GroupID);

        List<String> SwimmersUSAIDs = new List<string>();
        for (int i = 0; i < Swimmers.Count; i++)
            SwimmersUSAIDs.Add(Swimmers[i].USAID);


        DateTime AttendanceDate = new DateTime(2011, 1, 1);

        for (int i = 0; i < 30; i++)
        {
            AttendanceDate = GetNextWeekDay(AttendanceDate);
            Random rand = new Random(i);
            int PracticesToday = rand.Next(0, 9);
            if (PracticesToday != 8)
                PracticesToday = 1;
            else
                PracticesToday = 2;
            for (int k = 1; k <= PracticesToday; k++)
            {
                for (int j = 0; j < SwimmersUSAIDs.Count; j++)
                {
                    DataRow row = newAttendances.NewRow();

                    row["USAID"] = SwimmersUSAIDs[j];
                    row["Date"] = AttendanceDate;
                    row["PracticeoftheDay"] = k;
                    row["GroupID"] = GroupID;
                    Random rand2 = new Random(i + i);
                    int RandomNumber = rand.Next(0, 6);
                    if (RandomNumber == 5)
                        row["AttendanceType"] = "A";
                    else
                        row["AttendanceType"] = "X";
                    if (RandomNumber != 6)
                        newAttendances.Rows.Add(row);
                    
                }
            }
        }

        // Add some new rows to the collection. 
        //DataRow row; 
        //for (int i = 0; i < 10000; i++)
        //{
        //    row = newAttendances.NewRow();
        //    row["USAID"] = "B";
        //    row["Date"] = new DateTime(2011, 1, 1);
        //    row["PracticeoftheDay"] = 1;
        //    row["GroupID"] = 1;
        //    row["AttendanceType"] = "X";
        //    newAttendances.Rows.Add(row);
        //}


        newAttendances.AcceptChanges();

        // Return the new DataTable. 
        return newAttendances;
    }

    // To avoid storing the connection string in your code, 
    // you can retrieve it from a configuration file. 
    private string GetConnectionString()
    {




        return "Data Source=gtacregistration.db.3253734.hostedresource.com; Initial Catalog=gtacregistration; User ID=gtacregistration; Password='SW8765im!';";
    }

    private static DateTime GetNextWeekDay(DateTime StartDate)
    {
        do
        {
            StartDate = StartDate.Add(new TimeSpan(1, 0, 0, 0));
        } while (StartDate.DayOfWeek == DayOfWeek.Saturday ||
                 StartDate.DayOfWeek == DayOfWeek.Sunday);

        return StartDate;
    }
}