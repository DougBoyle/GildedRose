using NUnit.Framework;
using System.Collections.Generic;

namespace csharp {
    [TestFixture]
    public class GildedRoseTest {
        [Test]
        public void DegradesEachDay() {
            var item = new Item {Name = "foo", SellIn = 10, Quality = 40};
            IList<Item> Items = new List<Item> {item};
            var app = new GildedRose(Items);
            for (var i = 1; i <= 10; i++) {
                app.UpdateQuality();
            }
            Assert.AreEqual(30, item.Quality);
        }
        
        [Test]
        public void DegradesTwiceAsFast_Once_OutOfDate() {
            var item = new Item {Name = "foo", SellIn = 0, Quality = 30};
            IList<Item> Items = new List<Item> {item};
            var app = new GildedRose(Items);
            for (var i = 1; i <= 10; i++) {
                app.UpdateQuality();
            }
            Assert.AreEqual(10, item.Quality);
        }

        [Test]
        public void ConjuredItemsDegradeTwiceAsFast() {
            var item = new Item {Name = "Conjured foo", SellIn = 10, Quality = 40};
            IList<Item> Items = new List<Item> {item};
            var app = new GildedRose(Items);
            for (var i = 1; i <= 10; i++) {
                app.UpdateQuality();
            }
            Assert.AreEqual(20, item.Quality);

            for (var i = 1; i <= 5; i++) {
                app.UpdateQuality();
            }
            Assert.AreEqual(0, item.Quality);
        }

        [Test]
        public void QualityNeverNegative() {
            var item = new Item {Name = "foo", SellIn = 10, Quality = 10};
            var item2 = new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 10};
            var item3 = new Item {Name = "Aged Brie", SellIn = 10, Quality = 10};
            var item4 = new Item {Name = "Conjured foo", SellIn = 10, Quality = 10};
            IList<Item> Items = new List<Item> {item, item2, item3, item4};
            var app = new GildedRose(Items);
            for (var i = 1; i <= 100; i++) {
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
            var item2 = new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 10};
            var item3 = new Item {Name = "Aged Brie", SellIn = 10, Quality = 10};
            var item4 = new Item {Name = "Conjured foo", SellIn = 10, Quality = 10};
            IList<Item> Items = new List<Item> {item1, item2, item3, item4};
            var app = new GildedRose(Items);
            for (var i = 1; i <= 50; i++) {
                app.UpdateQuality();
            }
            Assert.AreEqual(-40, item1.SellIn);
            Assert.AreEqual(-40, item2.SellIn);
            Assert.AreEqual(-40, item3.SellIn);
            Assert.AreEqual(-40, item4.SellIn);
        }

        [Test]
        public void AgedBrieGetsBetter() {
            var item = new Item {Name = "Aged Brie", SellIn = 10, Quality = 10};
            IList<Item> Items = new List<Item> {item};
            var app = new GildedRose(Items);
            for (var i = 1; i <= 10; i++) {
                app.UpdateQuality();
            }
            Assert.AreEqual(20, item.Quality);

            for (var i = 1; i <= 10; i++) {
                app.UpdateQuality();
            }
            Assert.AreEqual(40, item.Quality);
        }

        [Test]
        public void MaxQualityIs50() {
            var item1 = new Item {Name = "Aged Brie", SellIn = 10, Quality = 40};
            var item2 = new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 30, Quality = 40};
            IList<Item> Items = new List<Item> {item1, item2};
            var app = new GildedRose(Items);
            for (var i = 1; i <= 30; i++) {
                app.UpdateQuality();
            }
            Assert.AreEqual(50, item1.Quality);
            Assert.AreEqual(50, item2.Quality);
        }
    }
}