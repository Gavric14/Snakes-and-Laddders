using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;

namespace Snakes_and_Laddders
{
    public partial class MainWindow : Window
    {
        Rectangle landingRec; // landing rec допоможе визначити прямокутники на дошці
        Rectangle player; 
        Rectangle opponent; 
        List<Rectangle> Moves = new List<Rectangle>(); // список прямокутників для зберігання шматків дошки
        DispatcherTimer gameTimer = new DispatcherTimer(); // ігровий таймер
        ImageBrush playerImage = new ImageBrush(); // image brush імпортування GIF-зображення гравця і прикріплення його до прямокутника гравця
        ImageBrush opponentImage = new ImageBrush(); // image brush імпортування GIF-зображення суперника і прикріплення його до прямокутника суперника
        // int I та J буде використовуватися для гравця та опонента
        // вони допоможуть зрозуміти, де на дошці знаходяться гравець і опонент
        // за замовчуванням при завантаженні гри значення буде -1 для обох
        int i = -1;
        int j = -1;
        // створення позиції та поточної позиції з цілими чіслами для гравця
        int position;
        int currentPosition;
        // створення позиції та поточної позиції з цілими чіслами для опонента
        int opponentPosition;
        int opponentCurrentPosition;
        // це ціле число images буде використовуватися для відображення зображень дошки під час її створення
        int images = -1;
        // створення кубіка 
        Random rand = new Random();
        // два булевих значення, які визначатимуть, чий хід у грі
        bool playerOneRound, playerTwoRound;
        // Відображення поточної позиції гравця та суперника у графічному інтерфейсі
        int tempPos;
        public MainWindow()
        {
            InitializeComponent();
            SetupGame(); // запуск гри для функцій налаштування зсередини цього конструктора
        }
        private void OnClickEvent(object sender, MouseButtonEventArgs e)
        {
            // ця подія при натисканні пов'язана з полотном, тому гравець може натиснути будь-де на полотні, щоб почати відтворення
            // нижче наведено if, яка перевіряє, чи булеві player 1 і 2 спочатку мають значення false
            // якщо вони є, то ми можемо зробити наступне всередині if
            if (playerOneRound == false && playerTwoRound == false)
            {
                position = rand.Next(1, 7); // генерація випадкового числа для гравця
                txtPlayer.Content = "You Rolled a " + position; // відображення цього числа біля зображення гравця
                currentPosition = 0; // присвоєння 0 до поточної позиції
                //У наведеному нижче операторі if перевіряємо, чи i є поточною позицією гравця у грі
                
                if ((i + position) <= 99)
                {

                    playerOneRound = true; 
                    gameTimer.Start(); 
                }
                else
                {
                    
                    if (playerTwoRound == false)
                    {
                        
                        playerTwoRound = true; 
                        opponentPosition = rand.Next(1, 7); 
                        txtOpponent.Content = "Opponent Rolled a " + opponentPosition; 
                        opponentCurrentPosition = 0; 
                        gameTimer.Start(); 
                    }
                    else
                    {
                        // якщо гравець другого раунду вже є true, тоді
                        gameTimer.Stop(); // зупинка ігрового таймера
                        // змінити обидва булеві значення на false
                        playerOneRound = false;
                        playerTwoRound = false;
                    }
                }
            }
        }
        private void SetupGame()
        {
            // це функція налаштування гри. У цій функції ми налаштуємо ігрове поле, гравця та суперника
            // Для того, щоб створити дошку, нам потрібно створити 3 локальні змінні, наведені нижче
            int leftPos = 10; // left pos допоможе нам розташувати boxes справа наліво 
            int topPos = 600; // top pos допоможе нам розташувати boxes знизу вгору
            int a = 0; // int a допоможе нам викласти 10 boxes в ряд
            // два рядки нижче імпортують зображення гравця та суперника і приєднують їх до image brush
            playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/player.gif"));
            opponentImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/opponent.gif"));
            // це основний цикл for, в якому ми будемо створювати ігрове поле
            // цей цикл виконається 100 разів всередині цієї функції
            // він буде виконуватись саме так, тому що нам потрібно 100 плиток для роботи гри
            for (int i = 0; i < 100; i++)
            {
                // спочатку ми збільшуємо images integer, яке ми створили в програмі раніше
                images++;
                // створіть новий image brush під назвою tile images, це додасть зображення до прямокутників для дошки
                ImageBrush tileImages = new ImageBrush();
                // імпортуємо зображення прямокутників дошки всередину зображень плиток
                // всередині нового uri ви можете побачити, що ми додаємо images integer, це тому, що у нас є імена зображень від 0.jpg до 99.jpg
                // під час виконання циклу це ціле число images також буде збільшуватися, і ми зможемо захопити всі зображення для дошки
                tileImages.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/" + images + ".jpg"));
                // нижче ми створюємо новий прямокутник з назвою box
                // цей прямокутник матиме розміри 60x60 заввишки та завширшки, заповнення - зображення плитки та чорна рамка навколо нього
                Rectangle box = new Rectangle
                {
                    Height = 60,
                    Width = 60,
                    Fill = tileImages,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };
                // нам потрібно позначити прямокутник, створений у цьому циклі, тому ми дамо кожному блоку унікальне ім'я
                box.Name = "box" + i.ToString(); 
                this.RegisterName(box.Name, box); // регістрація ім'я всередині WPF-додатку
                Moves.Add(box); //додаємо новостворений bpx до списку прямокутників переміщення

                // нижче ми складаємо алгоритм, який нам потрібен для того, щоб викласти boxes по 10 в ряд
                // ми будемо складати boxes зліва направо, потім піднімемось вгору і зробимо зворотній хід

                if (a == 10)
                {
                    // це станеться, коли ми розмістимо 10 boxes зліва направо
                    topPos -= 60; // у цьому випадку відняти від top pos integer 
                    a = 30; // змініть значення a на 30, ми робимо це для того, щоб перемістити boxes справа наліво
                }
                
                if (a == 20)
                {

                    topPos -= 60; 
                    a = 0; 
                }
                
                if (a > 20)
                {

                    //цей оператор if допоможе нам розташувати boxes справа наліво
                    a--; // reduce 1 from a each loop
                    Canvas.SetLeft(box, leftPos); // set the box inside the canvas by the value of the left pos integer
                    leftPos -= 60; // reduce 60 from the left pos each loop
                }
                
                if (a < 10)
                {
                    // це станеться, коли ми захочемо розташувати boxes зліва направо

                    a++; // add 1 to a integer each loop
                    Canvas.SetLeft(box, leftPos); // встановлюємо ліву позицію поля у значення left pos
                    leftPos += 60; // додаємо 60 у значення left pos integer 
                    Canvas.SetLeft(box, leftPos); // set the box left position to the value of the left pos integer
                }
                Canvas.SetTop(box, topPos); //set the box top position to the value of top pos integer each loop
                MyCanvas.Children.Add(box); // finally add the box to the canvas display
                // end the loop
            }
            // налаштовуємо ігровий таймер
            gameTimer.Tick += GameTimerEvent; 
            gameTimer.Interval = TimeSpan.FromSeconds(.2);
            // set up the player rectangle
            // the player rectangle  матиме розміри 30x30 у висоту та ширину, буде заповнений player image та матиме рамку у 2 пікселі
            player = new Rectangle
            {
                Height = 30,
                Width = 30,
                Fill = playerImage,
                StrokeThickness = 2
            };
            // теж саме тільки для опонента 
            opponent = new Rectangle
            {
                Height = 30,
                Width = 30,
                Fill = opponentImage,
                StrokeThickness = 2
            };
            // add both player and the opponent to the canvas
            MyCanvas.Children.Add(player);
            MyCanvas.Children.Add(opponent);
            // запустити функцію переміщення фігури і посилатися на гравця та опонента всередині неї
            // також вказуємо, де ми хочемо розташувати гравця та опонента на початку гри
            MovePiece(player, "box" + 0);
            MovePiece(opponent, "box" + 0);
        }
        private void GameTimerEvent(object sender, EventArgs e)
        {
            // це подія ігрового таймеру, яка переміщує гравця та опонента на дошці
            // у нижченаведеному операторі if спочатку перевіряється, чи true, що the player one round is true, а player two round is false
            if (playerOneRound == true && playerTwoRound == false)
            {
                // if this condition is true then we will do the following
                // check if i is less than the total number of board pieces inside of the moves list
                if (i < Moves.Count)
                {
                    // якщо так, то перевіряємо, чи поточна позиція менша за позицію, яку ми згенерували за допомогою класу random
                    if (currentPosition < position)
                    {
                        // if so, now we add 1 to the current position with each tick
                        currentPosition++;
                        i++;// add 1 to the i integer with each tick
                        MovePiece(player, "box" + i); // update the player position using the move piece function
                    }
                    else
                    {
                        // якщо для player one round встановлено значення false, то зробіть наступне
                        playerTwoRound = true; // set player two round to true
                        // запускаємо i яка є позицією гравця через функцію змій та сходів
                        i = CheckSnakesOrLadders(i);
                        // update the player position on the move piece function
                        MovePiece(player, "box" + i);
                        // Тепер, коли ми закінчили раунд з гравцем, нам потрібно налаштувати CPU
                        opponentPosition = rand.Next(1, 7); // generate a random number for the cpu
                        txtOpponent.Content = "Opponent Rolled a " + opponentPosition; 
                        opponentCurrentPosition = 0; 
                        tempPos = i; 
                        txtPlayerPosition.Content = "Player is @ " + (tempPos + 1);
                        // дошка, яку ми генеруємо у грі, буде генерувати частину дошки від 0 до 99, тому ми додамо 1 до temp pos integer, щоб показати правильну інформацію про те, де гравець знаходиться на дошці
                    }
                }
                // цей оператор if, наведений нижче, перевіряє, чи гравець дійшов до вершини дошки
                if (i == 99)
                {
                    // якщо так, зупиніть час гри, покажіть повідомлення на екрані, і коли гравець натисне кнопку "ОК", перезапустіть гру
                    gameTimer.Stop();
                    MessageBox.Show("Game Over!, You Win" + Environment.NewLine + "Click Ok to Play Again", "GG");
                    RestartGame();
                }
            } // оператор if гравця закінчується тут
            // ця секція нижче призначена CPU, вона працюватиме лише тоді, коли для параметра player two round встановлено значення true
            if (playerTwoRound == true)
            {
                // так само, як і раніше, перевіряємо, чи позиція CPU менша за номери на дошці
                if (j < Moves.Count)
                {
                    // якщо так, то перевіряємо, чи поточна позиція суперника менша за згенеровану позицію
                    // ми перевіряємо, чи є у CPU ще ходи попереду, таким чином ми можемо не дати процесору робити ходи в останні хвилини і дозволити гравцю ходити після свого ходу
                    if (opponentCurrentPosition < opponentPosition && (j + opponentPosition < 101))
                    {
                        opponentCurrentPosition++; 
                        j++; 
                        MovePiece(opponent, "box" + j); // показувати рухи за допомогою функції фігури переміщення
                    }
                    else
                    {
                        // якщо the cpu дійшов до своєї черги, то ми робимо наступне
                        // встановити player one and two rounds to false
                        playerOneRound = false;
                        playerTwoRound = false;
                        // check CPU position with the snakes and ladders function
                        j = CheckSnakesOrLadders(j);
                        MovePiece(opponent, "box" + j);
                        // встановлювати темпи на позицію суперника і показувати її на дисплеї
                        tempPos = j;
                        txtOpponentPosition.Content = "Opponent is @ " + (tempPos + 1);
                        gameTimer.Stop();
                    }
                }
                // якщо опонент досяг 99 очок до кінця свого ходу, то ми завершуємо гру
                if (j == 99)
                {
                    // зупинити ігровий таймер, показати вікно з повідомленням і, коли гравець натисне кнопку "ОК", перезапустити гру
                    gameTimer.Stop();
                    MessageBox.Show("Game Over!, Opponent Wins" + Environment.NewLine + "Click Ok to Play Again", "GG");
                    RestartGame();
                }
            } // opponent if statement ends here
        }
        private void RestartGame()
        {
            // це функція перезапуску гри, вона поверне все до стандартних налаштувань під час запуску
            // if I and J back to -1 and встановлює гравця та опонента в 0 позицію на дошці
            i = -1;
            j = -1;
            MovePiece(player, "box" + 0);
            MovePiece(opponent, "box" + 0);
            position = 0;
            currentPosition = 0;
            opponentPosition = 0;
            opponentCurrentPosition = 0;
            // set player one and player two rounds to false
            playerOneRound = false;
            playerTwoRound = false;
            // повернути мітки гравця та суперника до стандартного вмісту
            txtPlayer.Content = "You Rolled a " + position;
            txtPlayerPosition.Content = "Player is @ 1";
            txtOpponent.Content = "Opponent Rolled a " + opponentPosition;
            txtOpponentPosition.Content = "Opponent is @ 1";
            gameTimer.Stop();
        }
        private int CheckSnakesOrLadders(int num)
        {
            // це функція перевірки змій або сходів. Мета цієї функції - перевірити, чи гравець
            // приземлився на нижній частині драбини або на вершині змії
            // ця функція повертає ціле число при виконанні, тому ми прив'язали її до рухів гравця та суперника
            // всередині цієї функції є декілька інструкцій if і ви можете перевірити, чи число, яке передається в цю функцію, збігається з будь-якою з умов
            // збігається з будь-якою з умов if, то вона змінить число на це кінцеве число 
            // таким чином ми можемо просто перевірити, чи гравець приземлився на драбину, а потім перемістити його туди, де закінчується драбина
            // а якщо він приземлився на змійку, то ми можемо перемістити його вниз, де закінчується змійка. 
            if (num == 1)
            {
                num = 17;
            }
            if (num == 8)
            {
                num = 27;
            }
            if (num == 24)
            {
                num = 46;
            }
            if (num == 26)
            {
                num = 4;
            }
            if (num == 29)
            {
                num = 49;
            }
            if (num == 37)
            {
                num = 16;
            }
            if (num == 38)
            {
                num = 40;
            }
            if (num == 44)
            {
                num = 56;
            }
            if (num == 50)
            {
                num = 72;
            }
            if (num == 51)
            {
                num = 32;
            }
            if (num == 61)
            {
                num = 59;
            }
            if (num == 64)
            {
                num = 84;
            }
            if (num == 65)
            {
                num = 45;
            }
            if (num == 78)
            {
                num = 97;
            }
            if (num == 88)
            {
                num = 90;
            }
            if (num == 89)
            {
                num = 69;
            }
            if (num == 92)
            {
                num = 66;
            }
            if (num == 94)
            {
                num = 74;
            }
            if (num == 96)
            {
                num = 57;
            }
            if (num == 98)
            {
                num = 80;
            }

            return num;
        }
        private void MovePiece(Rectangle player, string posName)
        {
            // ця функція переміщує гравця та суперника по дошці
            // спосіб, яким вона це робить, дуже простий, ми додали прямокутники дошки до списку ходів 
            // з кожного циклу нижче ми можемо перебрати усі прямокутники з цього списку
            // ми також перевіряємо, чи має якийсь з прямокутників posName, якщо так, то ми зв'яжемо прямокутник приземлення з цим прямокутником, знайденим всередині циклу for each
            // таким чином ми можемо переміщати прямокутник, який передається всередині цієї функції, і запускати його у події таймера, щоб анімувати при його запуску
            foreach (Rectangle rectangle in Moves)
            {
                if (rectangle.Name == posName)
                {
                    landingRec = rectangle;
                }
            }
            // два рядки тут помістять об'єкт "player", який передається у цій функції, у місце розташування landingRec
            Canvas.SetLeft(player, Canvas.GetLeft(landingRec) + player.Width / 2);
            Canvas.SetTop(player, Canvas.GetTop(landingRec) + player.Height / 2);
        }
    }
}

