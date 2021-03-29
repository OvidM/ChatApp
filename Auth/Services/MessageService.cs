using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Auth.Components;

namespace Auth.Services
{
    public interface IMessageService
    {
        int AddMessage(MessageModel message, string chatName);
        int ExecuteWrite(string query, Dictionary<string, object> args);
        List<MessageModel> GetMessages(string _username);
        List<MessageModel> GetMessages(string _username, string chatName);
    }

    public class MessageService : IMessageService
    {
        public List<MessageModel> GetMessages(string _username)
        {
            List<MessageModel> messages = new List<MessageModel>();
            const string query = "SELECT * FROM (SELECT * FROM Messages ORDER BY Field1 DESC LIMIT 10) Var1 ORDER BY Field1 ASC;";
            using var con = new SQLiteConnection(@"URI=file:/home/ovidiu/Documents/Projects/AlbertoBonnuci/ChatApp/Auth/Messages.db");
            con.Open();
            using var cmd = new SQLiteCommand(query, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr.GetString(1) == _username)
                {
                    MessageModel message = new MessageModel(rdr.GetString(1), rdr.GetString(2), true);
                    Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetString(2)}");
                    messages.Add(message);
                }
                else
                {
                    MessageModel message = new MessageModel(rdr.GetString(1), rdr.GetString(2), false);
                    Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetString(2)}");
                    messages.Add(message);
                }

            }
            return messages;
        }
        public List<MessageModel> GetMessages(string _username, string chatName)
        {
            List<MessageModel> messages = new List<MessageModel>();
            string query = "SELECT * FROM (SELECT * FROM " + chatName + " ORDER BY Field1 DESC LIMIT 10) Var1 ORDER BY Field1 ASC;";
            using var con = new SQLiteConnection(@"URI=file:/home/ovidiu/Documents/Projects/AlbertoBonnuci/ChatApp/Auth/Messages.db");
            con.Open();
            using var cmd = new SQLiteCommand(query, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr.GetString(1) == _username)
                {
                    MessageModel message = new MessageModel(rdr.GetString(1), rdr.GetString(2), true);
                    Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetString(2)}");
                    messages.Add(message);
                }
                else
                {
                    MessageModel message = new MessageModel(rdr.GetString(1), rdr.GetString(2), false);
                    Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetString(2)}");
                    messages.Add(message);
                }

            }
            return messages;
        }

        public int ExecuteWrite(string query, Dictionary<string, object> args)
        {
            int numberOfRowsAffected;
            using (var con = new SQLiteConnection(@"URI=file:/home/ovidiu/Documents/Projects/AlbertoBonnuci/ChatApp/Auth/Messages.db"))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(query, con))
                {
                    foreach (var pair in args)
                    {
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                    }
                    numberOfRowsAffected = cmd.ExecuteNonQuery();
                }
                return numberOfRowsAffected;
            }
        }
        public int AddMessage(MessageModel message, string chatName)
        {
            string query = "INSERT INTO " + chatName + "(Username, Message) VALUES(@Username, @Body)";
            var args = new Dictionary<string, object>
            {
                {"@Username", message.Username},
                {"@Body", message.Body}
            };
            return ExecuteWrite(query, args);
        }

    }
}