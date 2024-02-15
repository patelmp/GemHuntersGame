// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
namespace GemHunters
{
    //  declares a public class named "Position"
    public class Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    // declares a public class named Player.
    public class Player
    {

        // declares a read-only property Name 

        public string Name { get; }
        public Position Position { get; set; }
        public int GemCount { get; set; }

        public Player(string name, Position position)
        {
            Name = name;
            Position = position;
            GemCount = 0;
        }
        //defines a method named Move within the Player class.

        public void Move(char direction)
        {
            switch (direction)
            {
                case 'U':
                    Position = new Position(Position.X, Position.Y - 1);
                    break;
                case 'D':
                    Position = new Position(Position.X, Position.Y + 1);
                    break;
                case 'L':
                    Position = new Position(Position.X - 1, Position.Y);
                    break;
                case 'R':
                    Position = new Position(Position.X + 1, Position.Y);
                    break;
                default:
                    Console.WriteLine("Invalid direction!");
                    break;
            }
        }
    }
    //defines a Cell class
    public class Cell
    {
        public string Occupant { get; set; }

        public Cell(string occupant)
        {
            Occupant = occupant;
        }
    }

    //defines a Board class
    public class Board
    {
        public Cell[,] Grid { get; }

        public Board()
        {
            Grid = new Cell[6, 6];
            // Initialize the board with empty cells
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Grid[i, j] = new Cell("-");
                }
            }
        }
        //defines a void Display 
        public void Display()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Console.Write(Grid[i, j].Occupant + " ");
                }
                Console.WriteLine();
            }
        }
        //defines a public class for move with switch condition       

        public bool IsValidMove(Player player, char direction)
        {
            // Check if the move is within the bounds of the board
            switch (direction)
            {
                case 'U':
                    return player.Position.Y > 0 && Grid[player.Position.Y - 1, player.Position.X].Occupant != "O";
                case 'D':
                    return player.Position.Y < 5 && Grid[player.Position.Y + 1, player.Position.X].Occupant != "O";
                case 'L':
                    return player.Position.X > 0 && Grid[player.Position.Y, player.Position.X - 1].Occupant != "O";
                case 'R':
                    return player.Position.X < 5 && Grid[player.Position.Y, player.Position.X + 1].Occupant != "O";
                default:
                    return false;
            }
        }

        public void CollectGem(Player player)
        {
            if (Grid[player.Position.Y, player.Position.X].Occupant == "G")
            {
                player.GemCount++;
                Grid[player.Position.Y, player.Position.X].Occupant = "-";
            }
        }
    }
}