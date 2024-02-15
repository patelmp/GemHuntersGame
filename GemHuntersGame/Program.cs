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
    //defines a  Game class
    public class Game
    {
        private readonly Board _board;
        private readonly Player _player1;
        private readonly Player _player2;
        private Player _currentTurn;
        private int _totalTurns;

        public Game()
        {
            _board = new Board();
            _player1 = new Player("P1", new Position(0, 0));
            _player2 = new Player("P2", new Position(5, 5));
            _currentTurn = _player1;
            _totalTurns = 0;
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            Random rand = new Random();
            // Place gems randomly on the board
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (rand.Next(0, 5) == 0 && _board.Grid[i, j].Occupant == "-")
                    {
                        _board.Grid[i, j].Occupant = "G";
                    }
                }
            }
            // Place obstacles randomly on the board
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (rand.Next(0, 6) == 0 && _board.Grid[i, j].Occupant == "-")
                    {
                        _board.Grid[i, j].Occupant = "O";
                    }
                }
            }
        }
    }