using NUnit.Framework;
using System.Collections.Generic;
namespace csharp {
    [TestFixture]
    public class GildedRoseTest {
        public const int InitialHighQuality = 40;
        public const int InitialLowQuality = 10;
        public const int InitialSellIn = 10;

        public const int PassQualityFirstThreshold = 10;
        public const int PassQualitySecondThreshold = 5;

        public const int SulfurasQuality = 80;
        public const int MaxQuality = 50;
        
        public const string BackstagePass = "Backstage passes to a TAFKAL80ETC concert";
        public const string Brie = "Aged Brie";
        public const string Sulfuras = "Sulfuras, Hand of Ragnaros";

        [Test]
        public void DegradesTwiceAsFast_Once_OutOfDate() {
            var item = new Item {Name = "foo", SellIn = InitialSellIn, Quality = InitialHighQuality};
            IList<Item> Items = new List<Item> {item};
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 10; i++) {
                app.UpdateQuality();
                Assert.AreEqual(InitialHighQuality - i, item.Quality);
            }

            for (int i = 1; i <= 10; i++) {
                app.UpdateQuality();
                Assert.AreEqual(InitialHighQuality - 10 - 2 * i, item.Quality);
            }
        }

        [Test]
        public void ConjuredItemsDegradeTwiceAsFast() {
            var item = new Item {Name = "Conjured foo", SellIn = InitialSellIn, Quality = InitialHighQuality};
            IList<Item> Items = new List<Item> {item};
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 10; i++) {
                app.UpdateQuality();
                Assert.AreEqual(InitialHighQuality - 2 * i, item.Quality);
            }

            for (int i = 1; i <= 5; i++) {
                app.UpdateQuality();
                Assert.AreEqual(InitialHighQuality - 20 - 4 * i, item.Quality);
            }
        }

        [Test]
        public void QualityNeverNegative() {
            var item = new Item {Name = "foo", SellIn = 0, Quality = 0};
            var item2 = new Item
                {Name = BackstagePass, SellIn = 0, Quality = 0};
            var item3 = new Item {Name = Brie, SellIn = 0, Quality = 0};
            var item4 = new Item {Name = "Conjured foo", SellIn = 0, Quality = 0};
            IList<Item> Items = new List<Item> { item, item2, item3, item4 };
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 10; i++) {
                app.UpdateQuality();
            }
            Assert.GreaterOrEqual(item.Quality, 0);
            Assert.GreaterOrEqual(item2.Quality, 0);
            Assert.GreaterOrEqual(item3.Quality, 0);
            Assert.GreaterOrEqual(item4.Quality, 0);
        }

        [Test]
        public void SellByDecrementsEachDay() {
            var item1 = new Item {Name = "foo", SellIn = 10, Quality = 10};
            var item2 = new Item {Name = BackstagePass, SellIn = 10, Quality = 10};
            var item3 = new Item {Name = Brie, SellIn = 10, Quality = 10};
            var item4 = new Item {Name = "Conjured foo", SellIn = 10, Quality = 10};
            IList<Item> Items = new List<Item> { item1, item2, item3, item4 };
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 50; i++) {
                app.UpdateQuality();
            }
            foreach (var item in Items) {
                Assert.AreEqual(-40, item.SellIn);
            }
        }

        [Test]
        public void AgedBrieGetsBetter() {
            var item = new Item {Name = Brie, SellIn = InitialSellIn, Quality = InitialLowQuality};
            IList<Item> Items = new List<Item> {item};
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 10; i++) {
                app.UpdateQuality();
                Assert.AreEqual(InitialLowQuality + i, item.Quality);
            }

            for (int i = 1; i <= 10; i++) {
                app.UpdateQuality();
                Assert.AreEqual(InitialLowQuality + 10 + 2 * i, item.Quality);
            }
        }

        [Test]
        public void QualityIsCapped() {
            var item = new Item {Name = Brie, SellIn = InitialSellIn, Quality = MaxQuality};
            IList<Item> Items = new List<Item> {item};
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 20; i++) {
                app.UpdateQuality();
            }

            Assert.AreEqual(MaxQuality, item.Quality);
        }

        [Test]
        public void SulfurasHasFixedQuality() {
            var item1 = new Item {Name = Sulfuras, SellIn = 0, Quality = SulfurasQuality};
            var item2 = new Item {Name = Sulfuras, SellIn = -1, Quality = SulfurasQuality};
            var item3 = new Item {Name = Sulfuras, SellIn = 1, Quality = SulfurasQuality};
            IList<Item> Items = new List<Item> {
                item1, item2, item3
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
                Assert.AreEqual(0, item1.SellIn);
                Assert.AreEqual(-1, item2.SellIn);
                Assert.AreEqual(1, item3.SellIn);
                Assert.AreEqual(SulfurasQuality, item1.Quality);
                Assert.AreEqual(SulfurasQuality, item2.Quality);
                Assert.AreEqual(SulfurasQuality, item3.Quality);
        }

        [Test]
        public void PassQualityIncreasesByOne_Initially() {
            var item = new Item
                {Name = BackstagePass, SellIn = PassQualityFirstThreshold + 1, Quality = InitialLowQuality};
            IList<Item> Items = new List<Item> {
                item
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(InitialLowQuality + 1, item.Quality);
        }

        [Test]
        public void PassQualityIncreasesByTwo_AfterFirstThreshold() {
            var item = new Item
                {Name = BackstagePass, SellIn = PassQualitySecondThreshold + 1, Quality = InitialLowQuality};
            IList<Item> Items = new List<Item> {
                item
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(InitialLowQuality + 2, item.Quality);
        }

        [Test]
        public void PassQualityIncreasesByThree_AfterSecondThreshold() {
            var item = new Item
                {Name = BackstagePass, SellIn = 1, Quality = InitialLowQuality};
            IList<Item> Items = new List<Item> {
                item
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(InitialLowQuality + 3, item.Quality);
        }

        [Test]
        public void PassQualityZeroAfterSellby() {
            var item = new Item
                {Name = BackstagePass, SellIn = 0, Quality = InitialHighQuality};
            IList<Item> Items = new List<Item> {
                item
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(0, item.Quality);
        }
    }
}