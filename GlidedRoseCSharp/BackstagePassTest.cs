using NUnit.Framework;
using System.Collections.Generic;

namespace csharp {
    [TestFixture]
    public class BackstagePassTest {
        [Test]
        public void PassQualityIncreasesByOne_Initially() {
            var item = new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 20, Quality = 20};
            IList<Item> Items = new List<Item> {
                item
            };
            var app = new GildedRose(Items);
            for (var i = 1; i <= 10; i++) {
                app.UpdateQuality();
            }
            Assert.AreEqual(30, item.Quality);
        }

        [Test]
        public void PassQualityIncreasesByTwo_Last10Days() {
            var item = new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20};
            var Items = new List<Item> {
                item
            };
            var app = new GildedRose(Items);
            for (var i = 1; i <= 5; i++) {
                app.UpdateQuality();
            }
            Assert.AreEqual(30, item.Quality);
        }

        [Test]
        public void PassQualityIncreasesByThree_Last5ays() {
            var item = new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 20};
            var Items = new List<Item> {
                item
            };
            var app = new GildedRose(Items);
            for (var i = 1; i <= 5; i++) {
                app.UpdateQuality();
            }
            Assert.AreEqual(35, item.Quality);
        }

        [Test]
        public void PassQualityZeroAfterSellby() {
            var item = new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20};
            var Items = new List<Item> {
                item
            };
            var app = new GildedRose(Items);
            for (var i = 1; i <= 5; i++) {
                app.UpdateQuality();
            }
            Assert.AreEqual(0, item.Quality);
        }
    }
}