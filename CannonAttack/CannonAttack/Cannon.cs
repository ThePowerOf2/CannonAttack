using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannonAttackTest {

    public sealed class Cannon {

        // Declaring Variables.
        private readonly string CANNONID = "Human";
        private string CannonID;

        public string ID {
            get {
                return (String.IsNullOrWhiteSpace(CannonID)) ? CANNONID : CannonID;
            }
            set {
                CannonID = value;
            }
        }
    }
}