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
                case 'U': //UP
                    return player.Position.Y > 0 && Grid[player.Position.Y - 1, player.Position.X].Occupant != "O";
                case 'D'://Down
                    return player.Position.Y < 5 && Grid[player.Position.Y + 1, player.Position.X].Occupant != "O";
                case 'L'://Left
                    return player.Position.X > 0 && Grid[player.Position.Y, player.Position.X - 1].Occupant != "O";
                case 'R'://Right
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

        public void Start()
        {
            while (!IsGameOver())
            {
                Console.WriteLine($"Turn {_totalTurns + 1} - {_currentTurn.Name}'s turn:");
                _board.Display();
                Console.Write("Enter direction (U/D/L/R): ");
                char direction = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();
                if (_board.IsValidMove(_currentTurn, direction))
                {
                    _currentTurn.Move(direction);
                    _board.CollectGem(_currentTurn);
                }
                else
                {
                    Console.WriteLine("Invalid move!");
                }
                _totalTurns++;
                SwitchTurn();
            }
            AnnounceWinner();
        }

        private void SwitchTurn()
        {
            _currentTurn = _currentTurn == _player1 ? _player2 : _player1;
        }

        private bool IsGameOver()
        {
            return _totalTurns >= 30;
        }

        private void AnnounceWinner()
        {
            Console.WriteLine("Game Over!");
            Console.WriteLine($"Player 1 collected {_player1.GemCount} gems.");
            Console.WriteLine($"Player 2 collected {_player2.GemCount} gems.");
            if (_player1.GemCount > _player2.GemCount)
            {
                Console.WriteLine("Player 1 wins!");
            }
            else if (_player1.GemCount < _player2.GemCount)
            {
                Console.WriteLine("Player 2 wins!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
        }
    }
}