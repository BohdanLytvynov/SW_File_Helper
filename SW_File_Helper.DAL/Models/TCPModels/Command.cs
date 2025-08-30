using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.DAL.Models.TCPModels
{
    public abstract class Command : Message
    {
        public Command()
        {
            MessageType = Enums.MessageType.Command;
            Text = string.Empty;
        }
    }
}
