using System.Collections.Generic;
using System.Data.SQLite;

namespace Auth.Services
{
    public interface IChatService
    {
        List<string> GetChats();
    }

    public class ChatService : IChatService
    {
        List<string> chats = new List<string>();
        public List<string> GetChats()
        {
            const string query = "SELECT * FROM sqlite_master ORDER BY name;";
            using var con = new SQLiteConnection(@"URI=file:/home/ovidiu/Documents/Projects/AlbertoBonnuci/ChatApp/Auth/Messages.db");
            con.Open();
            using var cmd = new SQLiteCommand(query, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                //  if (rdr.GetString(1) != "sqlite_sequence")
                chats.Add(rdr.GetString(1));
            }
            return chats;
        }
    }
}