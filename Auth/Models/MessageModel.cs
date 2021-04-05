namespace Auth.Components
{
    public class MessageModel
    {

        public MessageModel(string username, string body, bool mine, string key, string iv)
        {
            Username = username;
            Body = body;
            Mine = mine;
            Key = key;
            IV = iv;
        }
        public MessageModel(string username, string body, bool mine)
        {
            Username = username;
            Body = body;
            Mine = mine;
        }

        public string Username { get; set; }
        public string Body { get; set; }
        public bool Mine { get; set; }
        public string Key { get; set; }
        public string IV { get; set; }
        public bool IsNotice => Body.StartsWith("[Notice]");
        public string CSS => Mine ? "sent" : "received";
    }

}