using CannonAttack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannonAttackTest {

    public sealed class Cannon {

        static void Main(string[] args) {
            // Declaring Variables.
            bool Exit = false;
            bool hit = false;
            int Angle, Speed, Distance, Counter = 0;
            char Choice;

            // Creating a random number genorator.
            Random rand = new Random();

            // Creating the cannon.
            Cannon cannon = new Cannon();

            // While loop until the user chooses to exit after a game.
            while (!Exit) {
                // Creating a radom distance and setting the target at that distance and printing it out to the user.
                Distance = rand.Next(1, 20000);
                cannon.SetTarget(Distance);
                Console.WriteLine("Target Distance: " + Distance);
                
                // While loop until the user hits the target or gets within 50 feet of the target.
                while (!hit) {
                    // Asking the users angle and speed they wish to fire at.
                    Console.Write("Please Enter Angle: ");
                    Angle = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Please Enter Speed: ");
                    Speed = Convert.ToInt32(Console.ReadLine());

                    // Shooting the cannon and changing 'hit' to whether it hit or not and printing out where the cannon fired to.
                    hit = cannon.Shoot(Angle, Speed).Item1;
                    Console.WriteLine(cannon.Shoot(Angle, Speed).Item2);

                    // Incrementing a counter to keep track of how many shots the user has taken.
                    Counter++;
                }

                // Printing out how many shots it took the user to hit the target and asking if they would like to play again.
                Console.WriteLine("Hit it in " + Counter + "!");
                Console.Write("Press Y to to play again!");

                //Changing the input to lowercase and only taking the first character.
                Choice = Char.ToLower(Console.ReadLine()[0]);

                // If the user put 'y' or 'Y' then restart the game otherwise exit.
                if (Choice == 'y') {
                    hit = false;
                    Counter = 0;
                }
                else {
                    Exit = true;
                }
            }
            // Printing to the user thanks for playing and awaits a keypress to end the program.
            Console.WriteLine("Thank you for playing!");
            Console.ReadKey();
        }

        // Declaring Variables.
        private readonly string CANNONID = "Human";
        private string CannonID;
        public static readonly int MAXANGLE = 90;
        public static readonly int MINANGLE = 1;
        private readonly int MAXVELOCITY = 300000000;
        private int distanceOfTarget;
        private readonly double GRAVITY = 9.8;
        private readonly int MAXDISTANCEOFTARGET = 20000;
        private readonly int BURSTRADIUS = 50;

        public string ID {
            get {
                return (String.IsNullOrWhiteSpace(CannonID)) ? CANNONID : CannonID;
            }
            set {
                CannonID = value;
            }
        }

        public int DistanceOfTarget {
            get { return distanceOfTarget; }
            set { distanceOfTarget = value; }
        }


        private static Cannon cannonSingletonInstance;
        static readonly object padlock = new object();
        private Cannon() {
            
        }
        
        public static Cannon GetInstance() {
            lock (padlock) {
                if (cannonSingletonInstance == null) {
                    cannonSingletonInstance = new Cannon();
                }
            }
            return cannonSingletonInstance;
        }

        public Tuple<bool, string> Shoot(int angle, int velocity) {
            if (angle > MAXANGLE || angle < MINANGLE) {
                return Tuple.Create(false, "Angle Incorrect");
            }
            if (velocity > MAXVELOCITY) {
                return Tuple.Create(false, "Shot cannot travel faster than the speed of light");
            }
            string message;
            bool hit;
            int distanceOfShot = CalculateDistanceOfCannonShot(angle, velocity);
            if (distanceOfShot.Within(this.distanceOfTarget, BURSTRADIUS)) {
                message = "Hit!";
                hit = true;
            }
            else {
                message = String.Format("Missed cannonball landed at {0} meters", distanceOfShot);
                hit = false;
            }
            return Tuple.Create(hit, message);
        }

        public void SetTarget(int distanceOfTarget) {
            if (!distanceOfTarget.Between(0, MAXDISTANCEOFTARGET)) {
                throw new ApplicationException(String.Format("Target distance must be between 1 and {0} meters", MAXDISTANCEOFTARGET));
            }
            this.distanceOfTarget = distanceOfTarget;
        }

        public int CalculateDistanceOfCannonShot(int angle, int velocity) {
            int time = 0;
            double height = 0;
            double distance = 0;
            double angleInRadians = (3.1415926536 / 180) * angle;
            while(height >= 0) {
                time++;
                distance = velocity * Math.Cos(angleInRadians) * time;
                height = (velocity * Math.Sin(angleInRadians) * time) - (GRAVITY * Math.Pow(time, 2)) / 2;
            }
            return (int)distance;
        }
    }
}