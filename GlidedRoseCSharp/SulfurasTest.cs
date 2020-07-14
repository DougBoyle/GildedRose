using NUnit.Framework;
using System.Collections.Generic;
using static csharp.GildedRoseTest;

namespace csharp {
    [TestFixture]
    public class SulfurasTest {
        [Test]
        public void SulfurasHasFixedQuality() {
            var item1 = new Item {Name = Sulfuras, SellIn = 0, Quality = SulfurasQuality};
            var item2 = new Item {Name = Sulfuras, SellIn = -1, Quality = SulfurasQuality};
            var item3 = new Item {Name = Sulfuras, SellIn = 1, Quality = SulfurasQuality};
            IList<Item> Items = new List<Item> {
                item1, item2, item3
            };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(SulfurasQuality, item1.Quality);
            Assert.AreEqual(SulfurasQuality, item2.Quality);
            Assert.AreEqual(SulfurasQuality, item3.Quality);
        }
        
        [Test]
        public void SulfurasHasFixedSellIn() {
            var item1 = new Item {Name = Sulfuras, SellIn = 0, Quality = SulfurasQuality};
            var item2 = new Item {Name = Sulfuras, SellIn = -1, Quality = SulfurasQuality};
            var item3 = new Item {Name = Sulfuras, SellIn = 1, Quality = SulfurasQuality};
            IList<Item> Items = new List<Item> {
                item1, item2, item3
            };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(0, item1.SellIn);
            Assert.AreEqual(-1, item2.SellIn);
            Assert.AreEqual(1, item3.SellIn);
        }
    }
}