using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using System;
using System.Threading;
using System.Windows;

namespace Pong
{
    /// <summary>
    /// This class controls the trajectory of the pong.
    /// </summary>
    public class PongController : AgentBase
    {
        private Random rng;
        private int width;
        private int height;

        public PongController(ISpace ts) : base(string.Empty, ts)
        {
            this.width = TerminalInfo.GameboardColumns;
            this.height = TerminalInfo.GameboardRows;
            this.rng = new Random(Environment.TickCount);
        }

        protected override void DoWork()
        {
            // Wait until we can start
            this.Query(EntityType.SIGNAL, "start");
            int leftPlayerId = 1;
            int rightPlayerId = 2;

            // Read the player names
            PlayerInfo leftplayer = (PlayerInfo)this.Query(EntityType.PLAYERINFO, leftPlayerId, typeof(string), typeof(int));
            PlayerInfo rightplayer = (PlayerInfo)this.Query(EntityType.PLAYERINFO, rightPlayerId, typeof(string), typeof(int));

            // Keep iterating while the state is 'running'
            while (this.QueryP(EntityType.SIGNAL, "running", true) != null)
            {
                // Get the position so we can update it
                Pong pong = (Pong)this.Get(EntityType.PONG, typeof(double), typeof(double), typeof(double), typeof(double), typeof(double));

                // Calculate new position based on the direction
                Vector newPosition = pong.Position + pong.Direction * pong.Speed;

                // Check if the pong is beyond top and bottom walls
                // if is has hit the walls, then change the direction
                if (newPosition.Y < 0 || newPosition.Y >= this.height)
                {
                    pong.Direction = this.ChangeDirection(pong.Direction, false);
                    newPosition = pong.Position + pong.Direction * pong.Speed;
                }

                // Check if the poing has reached the left most wall
                if (newPosition.X <= 0)
                {
                    // Read the latest position of the player
                    Position playerPosition = (Position)this.Query(EntityType.POSITION, leftPlayerId, typeof(double), typeof(double));

                    // Check if the pong hit the player pad, then change the position and direction
                    // otherwise reward the other player
                    if ((int)playerPosition.Y == (int)newPosition.Y)
                    {
                        pong.Direction = this.ChangeDirection(pong.Direction, true);
                        newPosition = pong.Position + pong.Direction * pong.Speed;
                    }
                    else
                    {
                        this.IncreasePlayerScore(rightplayer.Name, leftplayer.Name);
                        continue;
                    }
                }

                // Check if the poing has reached the right most wall
                if (newPosition.X >= this.width)
                {
                    // Read the latest position of the player
                    Position playerPosition = (Position)this.Query(EntityType.POSITION, rightPlayerId, typeof(double), typeof(double));

                    // Check if the pong hit the player pad, then change the position and direction
                    // otherwise reward the other player
                    if ((int)playerPosition.Y == (int)newPosition.Y)
                    {
                        pong.Direction = this.ChangeDirection(pong.Direction, true);
                        newPosition = pong.Position + pong.Direction * pong.Speed;
                    }
                    else
                    {
                        this.IncreasePlayerScore(leftplayer.Name, rightplayer.Name);
                        continue;
                    }
                }

                // Update the pong information based on potential changes
                double pongY = Math.Max(newPosition.Y, 0d);
                pongY = Math.Min(pongY, (double)(this.height - 1));
                pong[1] = newPosition.X;
                pong[2] = pongY;
                this.Put(pong);
                Thread.Sleep(100);
            }
        }

        private void IncreasePlayerScore(string playername, string serving)
        {
            PlayerInfo playerinfo = (PlayerInfo)this.Get(EntityType.PLAYERINFO, typeof(int), playername, typeof(int));
            playerinfo.Score++;
            this.Put(playerinfo);
            this.Put(EntityType.SIGNAL, "serving", serving);
        }

        private Vector ChangeDirection(Vector currentDirection, bool xAxis)
        {
            Vector dir = new Vector();
            if (xAxis)
            {
                dir = new Vector(-currentDirection.X, -currentDirection.Y) + new Vector(rng.NextDouble() * 0.05, rng.NextDouble() * 0.05);
            }
            else
            {
                dir = new Vector(currentDirection.X, -currentDirection.Y) + new Vector(rng.NextDouble() * 0.05, rng.NextDouble() * 0.05);
            }
            dir.Normalize();
            return dir;
        }

    }
}
