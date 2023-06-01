using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestControls : System.Web.UI.Page
{
    //A global variable that will hold the current number of Rows
    //We set the values to 1 so that it will generate a default Row when the page loads
    private int numOfRows = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Generate the Rows on Initial Load
        if (!Page.IsPostBack)
        {
            GenerateTable(numOfRows);

            SchoolInfoBLL SchoolsInfoAdapter = new SchoolInfoBLL();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ViewState["RowsCount"] != null)
        {
            numOfRows = Convert.ToInt32(ViewState["RowsCount"].ToString());
            GenerateTable(numOfRows);
        }
    }

    private void SetPreviousData(int rowsCount, int colsCount)
    {
        Table table = (Table)Page.FindControl("Table1");
        if (table != null)
        {
            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < colsCount; j++)
                {
                    //Extracting the Dynamic Controls from the Table
                    TextBox tb = (TextBox)table.Rows[i].Cells[j].FindControl("TextBoxRow_" + i + "Col_" + j);
                    //Use Request objects for getting the previous data of the dynamic textbox
                    tb.Text = Request.Form["TextBoxRow_" + i + "Col_" + j];
                }
            }
        }
    }

    private void GenerateTable(int rowsCount)
    {

        //Creat the Table and Add it to the Page
        //Table table = new Table();
        //table.ID = "Table1";
        //Page.Form.Controls.Add(table);

        //The number of Columns to be generated
        const int colsCount = 3;//You can changed the value of 3 based on you requirements

        // Now iterate through the table and add your controls

        for (int i = 0; i < rowsCount; i++)
        {
            TableRow row = new TableRow();
            for (int j = 0; j < colsCount; j++)
            {
                TableCell cell = new TableCell();
                TextBox tb = new TextBox();

                // Set a unique ID for each TextBox added
                tb.ID = "TextBoxRow_" + i + "Col_" + j;
                // Add the control to the TableCell
                cell.Controls.Add(tb);
                // Add the TableCell to the TableRow
                row.Cells.Add(cell);
            }

            // And finally, add the TableRow to the Table
            Table1.Rows.Add(row);
        }

        //Set Previous Data on PostBacks
        SetPreviousData(rowsCount, colsCount);

        //Sore the current Rows Count in ViewState
        rowsCount++;
        ViewState["RowsCount"] = rowsCount;
    }

}