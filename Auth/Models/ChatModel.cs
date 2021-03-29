using System.Collections.Generic;

namespace Auth.Components
{
    public class ChatModel
    {
        public ChatModel()
        {

        }
        public string Name { get; set; }
        public int nrOfUsers { get; set; }
        public List<UserModel> users { get; set; }
        public List<MessageModel> Messages { get; set; }

    }
}