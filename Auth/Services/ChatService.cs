using System.Collections.Generic;
using System.Data.SQLite;

namespace Auth.Services
{
    public interface IChatService
    {
        void addOrRemove(string chatName, string username, string toWhat);
        void addToUser(string chatName, string username);
        string CreateChat(string chatName, string username);
        List<string> GetChats();
        int getState(string chatName, string username);
    }

    public class ChatService : IChatService
    {
        List<string> chats;
        public List<string> GetChats()
        {
            chats = new List<string>();
            const string query = "SELECT * FROM sqlite_master ORDER BY name;";
            using var con = new SQLiteConnection(@"URI=file:/home/ovidiu/Documents/Projects/AlbertoBonnuci/ChatApp/Auth/Messages.db");
            con.Open();
            using var cmd = new SQLiteCommand(query, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr.GetString(1) != "sqlite_sequence")
                    chats.Add(rdr.GetString(1));
            }
            return chats;
        }

        public string CreateChat(string chatName, string username)
        {
            string query = "CREATE TABLE " + chatName + "(Field1 INTEGER PRIMARY KEY AUTOINCREMENT, Username char(20), Message char(100))";
            using var con = new SQLiteConnection(@"URI=file:/home/ovidiu/Documents/Projects/AlbertoBonnuci/ChatApp/Auth/Messages.db");
            con.Open();
            using var cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            addToUser(chatName, username);
            return chatName;
        }

        public void addToUser(string chatName, string username)
        {
            string query = "ALTER TABLE AspNetUsers ADD isIn" + chatName + " Integer;";
            using var con = new SQLiteConnection(@"URI=file:/home/ovidiu/Documents/Projects/AlbertoBonnuci/ChatApp/Auth/Auth.db");
            con.Open();
            using var cmd1 = new SQLiteCommand(query, con);
            cmd1.ExecuteNonQuery();
            query = "UPDATE AspNetUsers SET isIN" + chatName + " = 1 WHERE UserName = '" + username + "'";
            using var cmd2 = new SQLiteCommand(query, con);
            cmd2.ExecuteNonQuery();
        }

        public void addOrRemove(string chatName, string username, string toWhat)
        {
            string query = "UPDATE AspNetUsers SET isIn" + chatName + " = " + toWhat + " Where UserName = '" + username + "'";
            using var con = new SQLiteConnection(@"URI=file:/home/ovidiu/Documents/Projects/AlbertoBonnuci/ChatApp/Auth/Auth.db");
            con.Open();
            using var cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
        }

        public int getState(string chatName, string username)
        {
            string query = "Select isIn" + chatName + " From AspNetUsers Where UserName = '" + username + "'";
            using var con = new SQLiteConnection(@"URI=file:/home/ovidiu/Documents/Projects/AlbertoBonnuci/ChatApp/Auth/Auth.db");
            con.Open();
            using var cmd = new SQLiteCommand(query, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                return rdr.GetInt32(0);
            }
            return 0;
        }
    }
}