using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public enum CommandType { Join, Leave, Message }

    [Serializable]
    public class ClientCommand
    {
        public string? Text { get; set; }
        public CommandType Type { get; set; }

        // ... additional data ...

        public ClientCommand(string text)
        {
            Type = CommandType.Message;
            Text = text;
        }
        public ClientCommand(CommandType type)
        {
            Type = type;
        }
    }
}
