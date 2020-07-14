using NUnit.Framework;
using System.Collections.Generic;
using static csharp.GildedRoseTest;

namespace csharp {
    [TestFixture]
    public class BackstagePassTest {
        [Test]
        public void PassQualityIncreasesByOne_Initially() {
            var item = new Item {Name = BackstagePass, SellIn = PassQualityFirstThreshold + 1, Quality = InitialLowQuality};
            IList<Item> Items = new List<Item> {
                item
            };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(InitialLowQuality + 1, item.Quality);
        }

        [Test]
        public void PassQualityIncreasesByTwo_AfterFirstThreshold() {
            var item = new Item {Name = BackstagePass, SellIn = PassQualitySecondThreshold + 1, Quality = InitialLowQuality};
            var Items = new List<Item> {
                item
            };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(InitialLowQuality + 2, item.Quality);
        }

        [Test]
        public void PassQualityIncreasesByThree_AfterSecondThreshold() {
            var item = new Item {Name = BackstagePass, SellIn = PassQualitySecondThreshold, Quality = InitialLowQuality};
            var Items = new List<Item> {
                item
            };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(InitialLowQuality + 3, item.Quality);
        }

        [Test]
        public void PassQualityZeroAfterSellby() {
            var item = new Item {Name = BackstagePass, SellIn = 0, Quality = InitialLowQuality};
            var Items = new List<Item> {
                item
            };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(0, item.Quality);
        }
    }
}