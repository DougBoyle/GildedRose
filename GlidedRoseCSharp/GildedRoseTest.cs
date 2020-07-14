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
                Assert.AreEqual( 40-i, item.Quality);
            }
            for (int i = 1; i <= 10; i++) {
                app.UpdateQuality();
                Assert.AreEqual(30-2*i, item.Quality);
            }
        }
        
        [Test]
        public void ConjuredItemsDegradeTwiceAsFast() {
            var item = new Item {Name = "Conjured foo", SellIn = 10, Quality = 40};
            IList<Item> Items = new List<Item> { item };
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 10; i++) {
                app.UpdateQuality();
                Assert.AreEqual(40-2*i, item.Quality);
            }
            for (int i = 1; i <= 5; i++) {
                app.UpdateQuality();
                Assert.AreEqual(20-4*i, item.Quality);
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
            Assert.AreEqual(-40, item.SellIn);
        }

        [Test]
        public void AgedBrieGetsBetter() {
            var item = new Item {Name = "Aged Brie", SellIn = 10, Quality = 5};
            IList<Item> Items = new List<Item> { item };
            GildedRose app = new GildedRose(Items);
            for (int i = 1; i <= 10; i++) {
                app.UpdateQuality();
                Assert.AreEqual(5+i, item.Quality );
            }
            for (int i = 1; i <= 10; i++) {
                app.UpdateQuality();
                Assert.AreEqual(15+2*i, item.Quality);
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
            Assert.AreEqual(50, item.Quality);
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
                Assert.AreEqual(0, item1.SellIn);
                Assert.AreEqual(-1, item2.SellIn);
                Assert.AreEqual(1, item3.SellIn);
                Assert.AreEqual(80, item1.Quality);
                Assert.AreEqual(80, item2.Quality);
                Assert.AreEqual(80, item3.Quality);
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
                Assert.AreEqual(20 + i, item.Quality);
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
                Assert.AreEqual(20 + 2*i, item.Quality);
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
                Assert.AreEqual(20+3*i, item.Quality);
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
                Assert.AreEqual(0, item.Quality);
            }
        }
    }
}
