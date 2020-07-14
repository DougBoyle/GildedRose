using NUnit.Framework;
using System.Collections.Generic;

namespace csharp {
    [TestFixture]
    public class SulfurasTest {
        [Test]
        public void SulfurasHasQuality80() {
            var item1 = new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80};
            var item2 = new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80};
            var item3 = new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 1, Quality = 80};
            IList<Item> Items = new List<Item> {
                item1, item2, item3
            };
            var app = new GildedRose(Items);
            for (var i = 1; i <= 100; i++) {
                app.UpdateQuality();
            }
            Assert.AreEqual(80, item1.Quality);
            Assert.AreEqual(80, item2.Quality);
            Assert.AreEqual(80, item3.Quality);
        }
        
        [Test]
        public void SulfurasHasFixedSellIn() {
            var item1 = new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80};
            var item2 = new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80};
            var item3 = new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 1, Quality = 80};
            IList<Item> Items = new List<Item> {
                item1, item2, item3
            };
            var app = new GildedRose(Items);
            for (var i = 1; i <= 100; i++) {
                app.UpdateQuality();
            }
            Assert.AreEqual(0, item1.SellIn);
            Assert.AreEqual(-1, item2.SellIn);
            Assert.AreEqual(1, item3.SellIn);
        }
    }
}