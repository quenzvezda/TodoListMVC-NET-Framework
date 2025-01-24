using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNet_Framework_WebApp.Pages
{
    public partial class Todo : Page
    {
        // Ganti connectionString dengan milik Anda
        private string connectionString =
            "Server=localhost,1433;Database=todolist;User Id=ramag123;Password=Aspireone1;TrustServerCertificate=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTodos();
            }
        }

        private void LoadTodos()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTodoItems", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                gvTodos.DataSource = dt;
                gvTodos.DataBind();
            }
        }

        /// <summary>
        /// Tambah Todo baru
        /// </summary>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertTodoItem", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@IsComplete", chkIsComplete.Checked);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            // Reset form
            txtTitle.Text = "";
            chkIsComplete.Checked = false;

            // Refresh GridView
            LoadTodos();
        }

        /// <summary>
        /// Event saat user klik tombol Edit pada salah satu row.
        /// </summary>
        protected void gvTodos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Tampilkan baris dalam mode edit
            gvTodos.EditIndex = e.NewEditIndex;
            LoadTodos();
        }

        /// <summary>
        /// Event saat user klik tombol Cancel setelah klik Edit.
        /// </summary>
        protected void gvTodos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Batalkan edit
            gvTodos.EditIndex = -1;
            LoadTodos();
        }

        /// <summary>
        /// Event saat user klik tombol Update
        /// </summary>
        protected void gvTodos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Ambil ID dari baris yang di-update
            int id = Convert.ToInt32(gvTodos.DataKeys[e.RowIndex].Value);

            // Ambil row saat ini
            GridViewRow row = gvTodos.Rows[e.RowIndex];

            // Ambil input Title dari TextBox edit
            var txtEditTitle = (TextBox)row.Cells[1].Controls[0];
            string title = txtEditTitle.Text;

            // Ambil input IsComplete dari CheckBox edit
            var chkEditComplete = (CheckBox)row.Cells[2].Controls[0];
            bool isComplete = chkEditComplete.Checked;

            // Lakukan update ke DB
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateTodoItem", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@IsComplete", isComplete);
                cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            // Selesai update, kembalikan GridView ke mode normal
            gvTodos.EditIndex = -1;

            // Reload data
            LoadTodos();
        }

        /// <summary>
        /// Event saat user klik tombol Delete
        /// </summary>
        protected void gvTodos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Ambil Id dari DataKeys
            if (e.RowIndex >= 0 && e.RowIndex < gvTodos.DataKeys.Count) // Validasi index
            {
                int id = Convert.ToInt32(gvTodos.DataKeys[e.RowIndex].Value);

                // Delete dari database
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DeleteTodoItem", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                // Refresh GridView setelah delete
                LoadTodos();
            }
        }

    }
}
