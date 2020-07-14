using System;
using System.Collections.Generic;

namespace csharp {
    public class GildedRose {
        IList<Item> Items;

        public GildedRose(IList<Item> Items) {
            this.Items = Items;
        }

        public void UpdateQuality() {
            foreach (var item in Items) {
                switch (item.Name) {
                    case "Aged Brie": {
                        item.Quality++;
                        break;
                    }
                    case "Backstage passes to a TAFKAL80ETC concert": {
                        item.Quality += item.SellIn < 6 ? 3
                            : item.SellIn < 11 ? 2
                            : 1;
                        break;
                    }
                    case "Sulfuras, Hand of Ragnaros":
                        break;
                    default: {
                        item.Quality--;
                        break;
                    }
                }

                if (item.SellIn <= 0) {
                    switch (item.Name) {
                        case "Aged Brie": {
                            item.Quality++;
                            break;
                        }
                        case "Backstage passes to a TAFKAL80ETC concert": {
                            item.Quality = 0;
                            break;
                        }
                        case "Sulfuras, Hand of Ragnaros": {
                            break;
                        }
                        default: {
                            item.Quality--;
                            break;
                        }
                    }
                }

                if (item.Name != "Sulfuras, Hand of Ragnaros") {
                    item.Quality = Math.Max(Math.Min(item.Quality, 50), 0);
                    item.SellIn--;
                }
            }
        }
    }
}