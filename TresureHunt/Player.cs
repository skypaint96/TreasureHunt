// <copyright file="Player.cs" company="MLK AH CompSci Solutions">
// Copyright (c) MLK AH CompSci Solutions. All rights reserved.
// </copyright>

namespace TresureHunt
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The class representing someone who can be marked down in a square for the tresure hunt.
    /// </summary>
    internal class Player
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="mobile">The mobile phone number of the player.</param>
        public Player(string name, string mobile)
        {
            this.Name = name;
            this.Mobile = mobile;
        }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        internal string Mobile { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name.Split(' ')[0] + " " + this.Mobile;
        }

        /// <summary>
        /// Gets a collection of random <see cref="Player"/>s.
        /// </summary>
        /// <returns>Returns a collection of <see cref="Player"/>.</returns>
        internal static Stack<Player> GetRandomPlayers()
        {
            string[] fullNames = Properties.Resources.RandomNames.Split(Environment.NewLine);
            Stack<Player> players = new Stack<Player>();
            Random random = new Random();
            for (int i = 0; i < random.Next(fullNames.Length); i++)
            {
                string[] name = fullNames[i].Split(',');
                Player player = new Player(name[0].Trim('"'), "07" + random.Next(10000000, 100000000).ToString());
                players.Push(player);
            }

            return players;
        }
    }
}