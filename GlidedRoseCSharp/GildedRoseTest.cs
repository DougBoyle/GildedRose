using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        [Test]
        public void DegradesTwiceAsFast_Once_OutOfDate() {
            var item = new Item {Name = "foo", SellIn = 10, Quality = 40};
            IList<Item> Items = new List<Item> { item };
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 10; i++) {
                app.UpdateQuality();
                Assert.AreEqual(item.Quality, 40-i);
            }
            for (int i = 1; i <= 10; i++) {
                app.UpdateQuality();
                Assert.AreEqual(item.Quality, 30-2*i);
            }
        }

        [Test]
        public void QualityNeverNegative() {
            var item = new Item {Name = "foo", SellIn = 10, Quality = 10};
            var item2 = new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 10};
            var item3 = new Item {Name = "Aged Brie", SellIn = 10, Quality = 10};
            IList<Item> Items = new List<Item> { item, item2, item3 };
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 100; i++) {
                app.UpdateQuality();
            }
            Assert.GreaterOrEqual(item.Quality, 0);
            Assert.GreaterOrEqual(item2.Quality, 0);
            Assert.GreaterOrEqual(item3.Quality, 0);
        }
        
        [Test]
        public void SellByDecrementsEachDay() {
            var item = new Item {Name = "foo", SellIn = 10, Quality = 40};
            IList<Item> Items = new List<Item> { item };
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 50; i++) {
                app.UpdateQuality();
            }
            Assert.AreEqual(item.SellIn, -40);
        }
        
        [Test]
        public void AgedBrieGetsBetter() {
            var item = new Item {Name = "Aged Brie", SellIn = 10, Quality = 5};
            IList<Item> Items = new List<Item> { item };
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 10; i++) {
                app.UpdateQuality();
                Assert.AreEqual(item.Quality, 5+i);
            }
            for (int i = 1; i <= 10; i++) {
                app.UpdateQuality();
                Assert.AreEqual(item.Quality, 15+2*i);
            }
        }
        
        [Test]
        public void MaxQualityIs50() {
            var item = new Item {Name = "Aged Brie", SellIn = 10, Quality = 5};
            IList<Item> Items = new List<Item> { item };
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 100; i++) {
                app.UpdateQuality();
            }
            Assert.AreEqual(item.Quality, 50);
        }
        
        [Test]
        public void SulfurasHasQuality80() {
            var item1 = new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80};
            var item2 = new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80};
            var item3 = new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 1, Quality = 80};
            IList<Item> Items = new List<Item> {
               item1, item2, item3
            };
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 100; i++) {
                app.UpdateQuality();
                Assert.AreEqual(item1.SellIn, 0);
                Assert.AreEqual(item2.SellIn, -1);
                Assert.AreEqual(item3.SellIn, 1);
                Assert.AreEqual(item1.Quality, 80);
                Assert.AreEqual(item2.Quality, 80);
                Assert.AreEqual(item3.Quality, 80);
            }
        }
        
        [Test]
        public void PassQualityIncreasesByOne_Initially() {
            var item = new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 20, Quality = 20};
            IList<Item> Items = new List<Item> {
                item
            };
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 10; i++) {
                app.UpdateQuality();
                Assert.AreEqual(item.Quality, 20+i);
            }
        }
        
        [Test]
        public void PassQualityIncreasesByTwo_Last10Days() {
            var item = new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20};
            IList<Item> Items = new List<Item> {
                item
            };
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 5; i++) {
                app.UpdateQuality();
                Assert.AreEqual(item.Quality, 20+2*i);
            }
        }
        
        [Test]
        public void PassQualityIncreasesByThree_Last5ays() {
            var item = new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 20};
            IList<Item> Items = new List<Item> {
                item
            };
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 5; i++) {
                app.UpdateQuality();
                Assert.AreEqual(item.Quality, 20+3*i);
            }
        }
        
        [Test]
        public void PassQualityZeroAfterSellby() {
            var item = new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20};
            IList<Item> Items = new List<Item> {
                item
            };
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 10; i++) {
                app.UpdateQuality();
                Assert.AreEqual(item.Quality, 0);
            }
        }
    }
}
