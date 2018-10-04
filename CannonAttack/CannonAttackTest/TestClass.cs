using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CannonAttack;

namespace CannonAttackTest {
    [TestFixture]
    public class TestClass {
        [Test]
        public void CannonIDValid() {
            Cannon cannon = Cannon.GetInstance();
            Assert.IsNotNull(cannon.ID);
        }

        [Test]
        public void TestCannonMultipleInstances() {
            Cannon cannon = Cannon.GetInstance();
            Cannon cannon2 = Cannon.GetInstance();
            Assert.IsTrue(cannon == cannon2);
        }

        [Test]
        public void TestCannonShootIncorrectAngle() {
            Cannon cannon = Cannon.GetInstance();
            var shot = cannon.Shoot(95, 100);
            Assert.IsFalse(shot.Item1); // Item1?
        }

        [Test]
        public void TestCAnnonShootVelocityGreaterThanSpeedOfLight() {
            Cannon cannon = Cannon.GetInstance();
            var shot = cannon.Shoot(95, 100);
            Assert.IsFalse(shot.Item1);
        }
    }
}
