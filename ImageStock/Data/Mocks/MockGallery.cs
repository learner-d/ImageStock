using ImageStock.Data.Interfaces;
using ImageStock.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageStock.Data.Mocks
{
    public class MockGallery: IGallery
    {
        public IReadOnlyList<Post> Posts
        {
            get
            {
                return new List<Post>()
                {
                    new Post("Краєвид колообігу", "", "../img/aestetic_1.jpg"),
                    new Post("Сонячна краса", "", "../img/aestetic_2.jpg"),
                    new Post("Квітуча врода", "", "../img/aestetic_3.jpg"),
                    new Post("Стиль життя", "", "../img/aestetic_4.jpg"),
                    new Post("Промінь надії", "", "../img/aestetic_5.jpg"),
                    new Post("Метушний момент", "", "../img/aestetic_6.jpg"),
                    new Post("Вірна мить", "", "../img/aestetic_7.jpg"),
                };
            }            
        }
    }
}
