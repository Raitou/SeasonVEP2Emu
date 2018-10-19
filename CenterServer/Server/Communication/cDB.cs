using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;

namespace libcomservice
{
    public class Database
    {
        public bool Connected = false;
        private SqlConnection connection;
        private string server = "";
        private string database = "";
        private string uid = "";
        private string password = "";
        public string connectionString;


        public Database(string _server, string _database, string _user, string _password)
        {
            server = _server;
            database = _database;
            uid = _user;
            password = _password;
            Initialize();
            Connected = true;
        }

        private string Cryptography(string PasswordCriptographed)
        {
            List<string> criptography;
            string[] Result;
            string returnResult = "";


            criptography = new List<string>();
            criptography.AddRange(new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "@", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" });

            Result = new string[PasswordCriptographed.Length];
            if (PasswordCriptographed.Length > 0)
            {
                for (int i = 0; i < PasswordCriptographed.Length; i++)
                {
                    string currentlyric = PasswordCriptographed.Substring(i, 1);
                    if (currentlyric == "A")
                        Result[i] = criptography[62];
                    else if (currentlyric == "B")
                        Result[i] = criptography[61];
                    else if (currentlyric == "C")
                        Result[i] = criptography[60];
                    else if (currentlyric == "D")
                        Result[i] = criptography[59];
                    else if (currentlyric == "E")
                        Result[i] = criptography[58];
                    else if (currentlyric == "F")
                        Result[i] = criptography[57];
                    else if (currentlyric == "G")
                        Result[i] = criptography[56];
                    else if (currentlyric == "H")
                        Result[i] = criptography[55];
                    else if (currentlyric == "I")
                        Result[i] = criptography[54];
                    else if (currentlyric == "J")
                        Result[i] = criptography[53];
                    else if (currentlyric == "K")
                        Result[i] = criptography[52];
                    else if (currentlyric == "L")
                        Result[i] = criptography[51];
                    else if (currentlyric == "M")
                        Result[i] = criptography[50];
                    else if (currentlyric == "N")
                        Result[i] = criptography[49];
                    else if (currentlyric == "O")
                        Result[i] = criptography[48];
                    else if (currentlyric == "P")
                        Result[i] = criptography[47];
                    else if (currentlyric == "Q")
                        Result[i] = criptography[46];
                    else if (currentlyric == "R")
                        Result[i] = criptography[45];
                    else if (currentlyric == "S")
                        Result[i] = criptography[44];
                    else if (currentlyric == "T")
                        Result[i] = criptography[43];
                    else if (currentlyric == "U")
                        Result[i] = criptography[42];
                    else if (currentlyric == "V")
                        Result[i] = criptography[41];
                    else if (currentlyric == "W")
                        Result[i] = criptography[40];
                    else if (currentlyric == "X")
                        Result[i] = criptography[39];
                    else if (currentlyric == "Y")
                        Result[i] = criptography[38];
                    else if (currentlyric == "Z")
                        Result[i] = criptography[37];

                    else if (currentlyric == "@")
                        Result[i] = criptography[36];
                    else if (currentlyric == "a")
                        Result[i] = criptography[35];
                    else if (currentlyric == "b")
                        Result[i] = criptography[34];
                    else if (currentlyric == "c")
                        Result[i] = criptography[33];
                    else if (currentlyric == "d")
                        Result[i] = criptography[32];
                    else if (currentlyric == "e")
                        Result[i] = criptography[31];
                    else if (currentlyric == "f")
                        Result[i] = criptography[30];
                    else if (currentlyric == "g")
                        Result[i] = criptography[29];
                    else if (currentlyric == "h")
                        Result[i] = criptography[28];
                    else if (currentlyric == "i")
                        Result[i] = criptography[27];
                    else if (currentlyric == "j")
                        Result[i] = criptography[26];
                    else if (currentlyric == "k")
                        Result[i] = criptography[25];
                    else if (currentlyric == "l")
                        Result[i] = criptography[24];
                    else if (currentlyric == "m")
                        Result[i] = criptography[23];
                    else if (currentlyric == "n")
                        Result[i] = criptography[22];
                    else if (currentlyric == "o")
                        Result[i] = criptography[21];
                    else if (currentlyric == "p")
                        Result[i] = criptography[20];
                    else if (currentlyric == "q")
                        Result[i] = criptography[19];
                    else if (currentlyric == "r")
                        Result[i] = criptography[18];
                    else if (currentlyric == "s")
                        Result[i] = criptography[17];
                    else if (currentlyric == "t")
                        Result[i] = criptography[16];
                    else if (currentlyric == "u")
                        Result[i] = criptography[15];
                    else if (currentlyric == "v")
                        Result[i] = criptography[14];
                    else if (currentlyric == "w")
                        Result[i] = criptography[13];
                    else if (currentlyric == "x")
                        Result[i] = criptography[12];
                    else if (currentlyric == "y")
                        Result[i] = criptography[11];
                    else if (currentlyric == "z")
                        Result[i] = criptography[10];
                    else if (currentlyric == "0")
                        Result[i] = criptography[9];
                    else if (currentlyric == "1")
                        Result[i] = criptography[8];
                    else if (currentlyric == "2")
                        Result[i] = criptography[7];
                    else if (currentlyric == "3")
                        Result[i] = criptography[6];
                    else if (currentlyric == "4")
                        Result[i] = criptography[5];
                    else if (currentlyric == "5")
                        Result[i] = criptography[4];
                    else if (currentlyric == "6")
                        Result[i] = criptography[3];
                    else if (currentlyric == "7")
                        Result[i] = criptography[2];
                    else if (currentlyric == "8")
                        Result[i] = criptography[1];
                    else if (currentlyric == "9")
                        Result[i] = criptography[0];
                }
                for (int i = 0; i < Result.Length; i++)
                {
                    returnResult += Result[i];
                }
            }
            return returnResult;
        }

        public void Initialize()
        {
            connectionString = string.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", server, database, uid, password);

            connection = new SqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }


        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("", ex);
                return false;
            }
        }

        //Insert statement
        public void Insert()
        {
            string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                SqlCommand cmd = new SqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update()
        {
            string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                SqlCommand cmd = new SqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete(string exec)
        {
            string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            if (this.OpenConnection() == true)
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void Exec(DataSet ds, string query, params object[] argv)
        {
            using (SqlConnection connection = new SqlConnection(string.Format("SERVER={0}; UID={1}; PASSWORD={2}; DATABASE={3};", server, uid, password, database)))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(string.Format(query, argv), connection);
                adapter.Fill(ds);
                connection.Close();
            }
        }


        //Select statement
        public List<string>[] Select(string command)
        {
            string query = command;

            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                SqlCommand cmd = new SqlCommand(query, connection);
                //Create a data reader and Execute the command
                SqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["id"] + "");
                    list[1].Add(dataReader["name"] + "");
                    list[2].Add(dataReader["age"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                SqlCommand cmd = new SqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public void Backup()
        {
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                //Save file to C:\ with the current date as a filename
                string path;
                path = string.Format("C:\\{0}-{1}-{2}-{3}-{4}-{5}-{6}.sql", year, month, day, hour, minute, second, millisecond);
                StreamWriter file = new StreamWriter(path);


                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error , unable to backup!", ex);
            }
        }

        //Restore
        public void Restore()
        {
            try
            {
                //Read file from C:\
                string path;
                path = "C:\\MySqlBackup.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();


                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;


                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error , unable to Restore!", ex);
            }
        }
    }

}
