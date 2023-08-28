using CommandSystem;
using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _3test
{
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class offtesla : CommandSystem.ICommand
    {
        public IEnumerator<float> MyCoroutine(int x)
        {
            for (; ; )
            {
                yield return Timing.WaitForSeconds(x);
            }
        }
        public string Command => "offtesla";

        public string[] Aliases => new string[] { "offt", "ot" };
        public string Description => "откючает теслу рядом с вами";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var PlayerGet = Player.Get(sender);
            PlayerGet.CurrentRoom.TeslaGate.InactiveTime = 60;
            response = "успешно";
            return true;
        }
    }
}
