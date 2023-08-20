using CommandSystem;
using Exiled.API.Features;
using System;

namespace ClassLibrary1
{
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class changenick : ICommand
    {
        public string Command => "changenick";

        public string[] Aliases => new string[] { "cn" };
        public string Description => "Меняет ваш ник";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string nick = string.Join(" ", arguments); //переводим аргументы в строку 
            if (string.IsNullOrWhiteSpace(nick)) //проверяем что игрок ввёл хоть что-то
            {
                response = "Введите ник, .cn NICKNAME";
                return false;
            }
            else
            {
                var PlayerGet = Player.Get(sender);
                PlayerGet.DisplayNickname = nick; //присваеваем имя 
                response = "Успешно";
                return true;
            }
        }
    }
}
