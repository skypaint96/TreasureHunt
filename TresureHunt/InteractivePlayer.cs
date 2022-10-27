// <copyright file="InteractivePlayer.cs" company="MLK AH CompSci Solutions">
// Copyright (c) MLK AH CompSci Solutions. All rights reserved.
// </copyright>

namespace TresureHunt
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// <inheritdoc/>
    /// An interactive player has support for console input outputs.
    /// </summary>
    internal class InteractivePlayer : Player
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InteractivePlayer"/> class.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="mobile">The mobile phone number of the player.</param>
        public InteractivePlayer(string name, string mobile)
            : base(name, mobile)
        {
        }

        /// <summary>
        /// Gets a new <see cref="InteractivePlayer"/> using the console.
        /// </summary>
        internal static InteractivePlayer Create
        {
            get
            {
                Console.Write(Environment.NewLine + "Please enter the following data." + Environment.NewLine);
                return new InteractivePlayer(GetNewNameFromUser(), GetNewPhoneFromUser());
            }
        }

        /// <summary>
        /// Gets the player's input for this round of tresure hunt.
        /// </summary>
        /// <param name="tresureHuntRound">The round that the players are to be added to.</param>
        internal static void PlayerInput(TresureHuntRound tresureHuntRound)
        {
            bool isAllPlayersNotCreated = true;
            while (isAllPlayersNotCreated)
            {
                Console.WriteLine("Do you want to add a player?(y/n)");
                if (Console.ReadKey(false).KeyChar.ToString().ToLower() == "y")
                {
                    InteractivePlayer player = InteractivePlayer.Create;
                    player.GetPlayerToChooseTheirCells(tresureHuntRound);
                }
                else
                {
                    Console.WriteLine();
                    isAllPlayersNotCreated = false;
                }
            }
        }

        /// <summary>
        /// Requests the user to select an empty cell to mark their name and contact details now on.
        /// </summary>
        /// <param name="tresureHuntRound">The round for the user to select a cell on.</param>
        internal void GetPlayerToChooseTheirCells(TresureHuntRound tresureHuntRound)
        {
            Console.Write($"{Environment.NewLine}Please choose a cell from the following table:{Environment.NewLine}{Environment.NewLine}{tresureHuntRound}{Environment.NewLine}Coordinates: ");
            string coords = Console.ReadLine().ToLower();
            if (!Regex.IsMatch(coords, "^[a-" + (char)(tresureHuntRound.Width + 97) + "][1-" + tresureHuntRound.Height + "]$"))
            {
                string failHint = "Incorrect input, format should be: ^[a - " + (char)(tresureHuntRound.Width + 97) + "][1 - " + tresureHuntRound.Height + "]$";
                Console.WriteLine(failHint);
                this.GetPlayerToChooseTheirCells(tresureHuntRound);
            }

            Console.WriteLine($"You were " + (tresureHuntRound.TryAdd(coords[0] - 97, int.Parse(coords[1].ToString()) - 1, this) ? string.Empty : "un") + "succsessfully added.");
            Console.WriteLine("Do you want to choose another square? (y/n)");
            if (Console.ReadKey(false).KeyChar.ToString().ToLower() == "y")
            {
                this.GetPlayerToChooseTheirCells(tresureHuntRound);
            }

            Console.WriteLine();
        }

        private static string GetNewNameFromUser()
        {
            Console.Write("Name: ");
            return Console.ReadLine();
        }

        private static string GetNewPhoneFromUser()
        {
            Console.Write("Mobile: ");
            string phone = Console.ReadLine();
            if (!Regex.IsMatch(phone, "(07)([0-9]{9})"))
            {
                Console.WriteLine("mobile number must be in the format ^07(9[0-9])$");
                phone = GetNewPhoneFromUser();
            }

            return phone;
        }
    }
}